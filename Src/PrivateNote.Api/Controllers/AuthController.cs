using PrivateNote.Api.Contracts.Data;
using PrivateNote.Api.Contracts.Requests;
using PrivateNote.Api.Contracts.Responses;
using PrivateNote.Api.Helpers;
using PrivateNote.Api.Services.Contract;

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
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        var result = await _authService.RegisterAsync(request.UserName, request.Password);
        return result is { Succeeded: true } ? Ok() : Problem(result?.ToString());
    }

    [HttpPost]
    public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest request)
    {
        var jwtToken = await  _authService.AuthenticateAsync(request.UserName, request.Password);
        if (string.IsNullOrEmpty(jwtToken))
            return BadRequest("user authentication was failed");
        return Ok(new SignInResponse { Token = jwtToken });
    }

    [HttpPost]
    public async Task<IActionResult> RsaSignUp(RsaSignUpRequest request)
    {
        var result = await _authService.RsaRegisterAsync(request.UserName, request.PublicKey, request.Signature);
        if (result != IdentityResult.Success)
            return BadRequest(result?.Errors);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<RsaSignInResponse>> RsaSignIn(RsaSignInRequest request)
    {
        var encryptedJwtToken = await _authService.RsaAuthenticateAsync(request.UserName, request.PublicKey, request.Signature);
        if (string.IsNullOrEmpty(encryptedJwtToken))
            return BadRequest("user authentication was failed");
        return Ok(new RsaSignInResponse { EncryptedToken = encryptedJwtToken });
    }

    [HttpGet]
    public async Task<string> Hello()
    {
        //var user = await RunHelper.TryRunWithTimeLog(() => _authService.GetMyUserAsync(), _logger);
        var user = await _authService.GetMyUserAsync();
        return $"Hello {(string.IsNullOrEmpty(user?.UserName) ? "World!" : user?.UserName)}";
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> HowAmI()
    {
        var user = await RunHelper.TryRunWithTimeLog(() => _authService.GetMyUserAsync(), _logger);
        if (user is null)
            return BadRequest("your user not found.");
        return Ok(new UserDto{Id = user.Id,UserName = user.UserName});
    }
}