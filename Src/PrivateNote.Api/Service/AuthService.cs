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

    public async Task<bool> RegisterAsync(string userName, string password)
    {
        var result = await _userManager.CreateAsync(new RsaUser { UserName = userName }, password);
        if(result.Succeeded) 
            return true;
        _logger.LogError(result.ToString());    
        return false;
    }


    public async Task<string> AuthenticateAsync(string userName, string password)
    {
        var user = await GetUserAsync(userName);
        if (user is null) throw new InvalidDataException();
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
            throw new InvalidDataException();
        var roles = await _userManager.GetRolesAsync(user);
        return _tokenService.GenerateToken(user.Id.ToString(), user.UserName, roles);
    }

    public async Task<IUser?> GetMyUserAsync()
    {
        var userName = _claimService.GetUserName();
        _logger.LogInformation("finding user with userName: {0}", userName);
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            _logger.LogError("user with username = {0} not found! but it was in the token!", userName);
        else
            _logger.LogInformation("user found");
        return user;
    }
}