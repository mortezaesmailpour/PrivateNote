namespace PrivateNote.Api.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private const string StartMessage = "{0} is starting ... with request: {1}";
    private const string ErrorMessage = "something went wrong while running {0} with request: {1}";
    private const string FinishedMessage = "{0} with request: {1} finished in {2}ms";

    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<string> Hello()
    {
        _logger.LogInformation("{0} is starting ... ", nameof(Hello));
        var stopWatch = Stopwatch.StartNew();
        try
        {
            var user = await _authService.GetMyUserAsync();
            if (user is not null)
                return "Hello " + user.UserName;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "something went wrong while running {0} ", nameof(Hello));
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("{0} finished in {1}ms", nameof(Hello), stopWatch.ElapsedMilliseconds);
        }
        return "Hello World!";
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        _logger.LogInformation(StartMessage, nameof(SignUp), request);
        var stopWatch = Stopwatch.StartNew();
        try
        {
            _ = await _authService.RegisterAsync(request.UserName, request.Password);
            _logger.LogInformation("user was registered successfully.");
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, ErrorMessage, nameof(SignUp), request);
            throw;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation(FinishedMessage, nameof(SignUp), request, stopWatch.ElapsedMilliseconds);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest request)
    {
        _logger.LogInformation(StartMessage, nameof(SignIn), request);
        var stopWatch = Stopwatch.StartNew();
        try
        {
            var jwtToken = await _authService.AuthenticateAsync(request.UserName, request.Password);
            _logger.LogInformation("user was authenticated successfully. token = {0}", jwtToken);
            return Ok(new SignInResponse { Token = jwtToken });
        }
        catch (InvalidDataException e)
        {
            _logger.LogError(e, "Username or password in not correct!");
            return BadRequest("Username or Password in not Correct!");
        }
        catch (Exception e)
        {
            _logger.LogError(e, ErrorMessage, nameof(SignIn), request);
            throw;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation(FinishedMessage, nameof(SignIn), request, stopWatch.ElapsedMilliseconds);
        }
    }

    [HttpGet]
    public async Task<IActionResult> HowAmI()
    {
        _logger.LogInformation("{0} is starting ... ", nameof(HowAmI));
        var stopWatch = Stopwatch.StartNew();
        try
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
                return Ok(new UserInfo() { UserName = user.UserName });
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "something went wrong while running {0} ", nameof(HowAmI));
            throw;
        }
        finally
        {
            stopWatch.Stop();
            _logger.LogInformation("{0} finished in {1}ms", nameof(HowAmI), stopWatch.ElapsedMilliseconds);
        }
    }

}