using System.Runtime.CompilerServices;

namespace PrivateNote.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IRsaAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IRsaAuthService rsaAuthService, ILogger<AuthController> logger)
    {
        _authService = rsaAuthService;
        _logger = logger;
    }

    private Task<TResult> TryRun2<TResult>(Func<Task<TResult>> action) => action();
    private async Task<TResult> TryRun<TResult>(Func<Task<TResult>> action,
        [CallerArgumentExpression("action")] string message = "") where TResult : class?
    {
        _logger.LogInformation("{0} is starting ...", message);
        Stopwatch stopWatch = new();
        stopWatch.Start();
        try
        {
            var result = await action();
            stopWatch.Stop();
            _logger.LogInformation("{0} finished in {1}ms", message, stopWatch.ElapsedMilliseconds);
            return result;
        }
        catch (Exception e)
        {
            stopWatch.Stop();
            _logger.LogError(e, "something went wrong while running {0} in {1}ms ", message,
                stopWatch.ElapsedMilliseconds);
            return null;
        }
    }

    [HttpPost]
    [AllowAnonymous]
    private async Task<IActionResult> SignUp(SignUpRequest request)
    {
        var result = await TryRun(() => _authService.RegisterAsync(request.UserName, request.Password));
        if (!result.Succeeded)
        {
            _logger.LogError("user registration was failed");
            return ValidationProblem();
        }
        _logger.LogInformation("user was registered successfully");
        return Ok();
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest request)
    {
        var token = await TryRun(async () =>
        {
            var (result ,jwtToken )= await _authService.AuthenticateAsync(request.UserName, request.Password);
            if (result.Succeeded) return jwtToken;
            _logger.LogError("user authentication was failed with error {0}",result.Errors);
            return null;
        });
        if (string.IsNullOrEmpty(token))
            return BadRequest("user authentication was failed");
        _logger.LogInformation("user was authenticated successfully. token = {0}", token);
        return Ok(new SignInResponse { Token = token });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RsaSignUp(RsaSignUpRequest request)
    {
        var result = await TryRun(() =>
            _authService.RsaRegisterAsync(request.UserName, request.PublicKey, request.Signature));
        if (result != IdentityResult.Success)
        {
            _logger.LogError("user registration was failed with errors : {0}", result.Errors);
            return BadRequest("user registration was failed");
        }

        _logger.LogInformation("user was registered successfully");
        return Ok();
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<RsaSignInResponse>> RsaSignIn(RsaSignInRequest request)
    {
        var token = await TryRun(async () =>
        {
            var (result ,encryptedToken )= await _authService.RsaAuthenticateAsync(request.UserName, request.PublicKey, request.Signature);
            if (result.Succeeded) return encryptedToken;
            _logger.LogError("user authentication was failed with error {0}",result.Errors);
            return null;
        });
        if (string.IsNullOrEmpty(token))
            return BadRequest("user authentication was failed");
        _logger.LogInformation("user was authenticated successfully. encryptedToken = {0}", token);
        return Ok(new RsaSignInResponse { EncryptedToken = token });
    }
    
    [HttpGet]
    public async Task<string> Hello()
    {
        var user = await TryRun(() => _authService.GetMyUserAsync());
        return $"Hello {(string.IsNullOrEmpty(user?.UserName) ? "World!" : user?.UserName)}";
    }

    [HttpGet]
    public async Task<IActionResult> HowAmI()
    {
        var user = await _authService.GetMyUserAsync();
        if (user is null)
        {
            _logger.LogError("your user not found.");
            return BadRequest("your user not found.");
        }
        else
        {
            _logger.LogInformation("your user = {0}", user.UserName);
            return Ok(new UserInfo() { Id = user.Id, UserName = user.UserName });
        }
    }
}