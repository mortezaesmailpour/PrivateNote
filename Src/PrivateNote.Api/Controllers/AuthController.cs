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

    private async Task<TResult> TryRun<TResult>(Func<Task<TResult>> action,
        [CallerArgumentExpression("action")] string message = "") where TResult : class
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

    private Task<IUser> GetCurrentUser() => TryRun(() => _authService.GetMyUserAsync());

    [HttpPost]
    [AllowAnonymous]
    public Task<IActionResult> SignUp(SignUpRequest request) => TryRun(() => signUp(request));

    [HttpPost]
    [AllowAnonymous]
    public Task<ActionResult<SignInResponse>> SignIn(SignInRequest request) => TryRun(() => signIn(request));

    [HttpPost]
    [AllowAnonymous]
    public Task<IActionResult> RsaSignUp(RsaSignUpRequest request) => TryRun(() => rsaSignUp(request));

    [HttpPost]
    [AllowAnonymous]
    public Task<ActionResult<RsaSignInResponse>> RsaSignIn(RsaSignInRequest request) =>
        TryRun(() => rsaSignIn(request));


    [HttpGet]
    public async Task<string> Hello()
    {
        var user = await GetCurrentUser();
        return $"Hello {(string.IsNullOrEmpty(user?.UserName) ? "World!" : user?.UserName)}";
    }


    private async Task<IActionResult> signUp(SignUpRequest request)
    {
        var result = await _authService.RegisterAsync(request.UserName, request.Password);
        if (!result)
        {
            _logger.LogError("user registeration was faild.");
            return ValidationProblem();
        }

        _logger.LogInformation("user was registered successfully.");
        return Ok();
    }

    private async Task<ActionResult<SignInResponse>> signIn(SignInRequest request)
    {
        var jwtToken = await _authService.AuthenticateAsync(request.UserName, request.Password);
        _logger.LogInformation("user was authenticated successfully. token = {0}", jwtToken);
        return Ok(new SignInResponse { Token = jwtToken });
        //     catch (InvalidDataException e)
        // {
        //     _logger.LogError(e, "Username or password in not correct!");
        //     return BadRequest("Username or Password in not Correct!");
        // }
    }

    private async Task<IActionResult> rsaSignUp(RsaSignUpRequest request)
    {
        _ = await _authService.RsaRegisterAsync(request.UserName, request.PublicKey, request.Signature);
        _logger.LogInformation("user was registered successfully.");
        return Ok();
    }

    private async Task<ActionResult<RsaSignInResponse>> rsaSignIn(RsaSignInRequest request)
    {
        var encryptedToken =
            await _authService.RsaAuthenticateAsync(request.UserName, request.PublicKey, request.Signature);
        _logger.LogInformation("user was authenticated successfully. encryptedToken = {0}", encryptedToken);
        return Ok(new RsaSignInResponse { EncryptedToken = encryptedToken });
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