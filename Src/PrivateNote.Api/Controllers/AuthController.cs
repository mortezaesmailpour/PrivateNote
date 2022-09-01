using System.Runtime.CompilerServices;
using PrivateNote.Api.Helpers;

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

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        var result =
            await RunHelper.TryRunWithTimeLog(() => _authService.RegisterAsync(request.UserName, request.Password),
                _logger);
        if (!result.Succeeded)
            return ValidationProblem();
        return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest request)
    {
        var jwtToken =
            await RunHelper.TryRunWithTimeLog(() => _authService.AuthenticateAsync(request.UserName, request.Password),
                _logger);
        if (string.IsNullOrEmpty(jwtToken))
            return BadRequest("user authentication was failed");
        return Ok(new SignInResponse { Token = jwtToken });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RsaSignUp(RsaSignUpRequest request)
    {
        var result = await RunHelper.TryRunWithTimeLog(() =>
            _authService.RsaRegisterAsync(request.UserName, request.PublicKey, request.Signature), _logger);
        if (result != IdentityResult.Success)
            return BadRequest("user registration was failed");
        return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<RsaSignInResponse>> RsaSignIn(RsaSignInRequest request)
    {
        var encryptedJwtToken = await RunHelper.TryRunWithTimeLog(
            () => _authService.RsaAuthenticateAsync(request.UserName, request.PublicKey, request.Signature), _logger);
        if (string.IsNullOrEmpty(encryptedJwtToken))
            return BadRequest("user authentication was failed");
        return Ok(new RsaSignInResponse { EncryptedToken = encryptedJwtToken });
    }

    [HttpGet]
    public async Task<string> Hello()
    {
        var user = await RunHelper.TryRunWithTimeLog(() => _authService.GetMyUserAsync(), _logger);
        return $"Hello {(string.IsNullOrEmpty(user?.UserName) ? "World!" : user?.UserName)}";
    }

    [HttpGet]
    public async Task<IActionResult> HowAmI()
    {
        var user = await RunHelper.TryRunWithTimeLog(() => _authService.GetMyUserAsync(), _logger);
        if (user is null)
            return BadRequest("your user not found.");
        return Ok(new UserInfo() { Id = user.Id, UserName = user.UserName });
    }
}