namespace PrivateNote.Service;

public class AuthService : IAuthService
{
    protected readonly IUserManager _userManager;
    protected readonly IClaimService _claimService;
    protected readonly ITokenService _tokenService;
    protected readonly ILoggerAdapter<AuthService> _logger;

    public AuthService(IUserManager userManager, IClaimService claimService, ITokenService tokenService,
        ILoggerAdapter<AuthService> logger)
    {
        _userManager = userManager;
        _claimService = claimService;
        _tokenService = tokenService;
        _logger = logger;
    }


    public async Task<IUser?> GetUserAsync(string userName)
    {
        _logger.LogInformation("finding user with userName: {0}", userName);
        var user = await _userManager.FindByNameAsync(userName);
        _logger.LogInformation(user is null ? "user not found" : "user found");
        return user;
    }

    public Task<IdentityResult> RegisterAsync(string userName, string password) => _userManager.CreateAsync(new RsaUser { UserName = userName }, password);

    public async Task<string?> AuthenticateAsync(string userName, string password)
    {
        var user = await GetUserAsync(userName);
        if (user is null)
        {
            _logger.LogError("user not found" );
            return null;
        }
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            _logger.LogError( "password isn't correct");
            return null;
        }
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user.Id.ToString(), user.UserName!, roles);
        return token;
    }

    public async Task<IUser?> GetMyUserAsync()
    {
        var userName = _claimService.GetUserName();
        if (string.IsNullOrEmpty(userName))
        {
            _logger.LogInformation("username is not  in the claims");
            return null;
        }
        _logger.LogInformation("finding user with userName: {0}", userName);
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            _logger.LogError("user with username = {0} not found! but it was in the token!", userName);
        _logger.LogInformation("user was find {0}",user?.UserName);
        return user;
    }
}