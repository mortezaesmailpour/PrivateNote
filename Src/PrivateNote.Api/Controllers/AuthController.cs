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
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        var result = await TryRun(() => _authService.RegisterAsync(request.UserName, request.Password));
        if (!result.Succeeded)
            return ValidationProblem();
        return Ok();
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest request)
    {
        var jwtToken = await TryRun(() => _authService.AuthenticateAsync(request.UserName, request.Password));
        if (string.IsNullOrEmpty(jwtToken))
            return BadRequest("user authentication was failed");
        return Ok(new SignInResponse { Token = jwtToken });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RsaSignUp(RsaSignUpRequest request)
    {
        var result = await TryRun(() =>
            _authService.RsaRegisterAsync(request.UserName, request.PublicKey, request.Signature));
        if (result != IdentityResult.Success)
            return BadRequest("user registration was failed");
        return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<RsaSignInResponse>> RsaSignIn(RsaSignInRequest request)
    {
        var encryptedJwtToken = await TryRun( () => _authService.RsaAuthenticateAsync(request.UserName, request.PublicKey, request.Signature));
        if (string.IsNullOrEmpty(encryptedJwtToken))
            return BadRequest("user authentication was failed");
        return Ok(new RsaSignInResponse { EncryptedToken = encryptedJwtToken });
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
        var user = await TryRun(() => _authService.GetMyUserAsync());
        if (user is null)
            return BadRequest("your user not found.");
        return Ok(new UserInfo() { Id = user.Id, UserName = user.UserName });
    }
}