using PrivateNote.Api.Data.Model;
using PrivateNote.Api.Services.Contract;

namespace PrivateNote.Api.Services;

public class AuthService : IAuthService
{
    protected readonly IUserManager UserManager;
    private readonly IClaimService _claimService;
    protected readonly ITokenService TokenService;
    protected readonly ILoggerAdapter<AuthService> _logger;

    protected AuthService(IUserManager userManager, IClaimService claimService, ITokenService tokenService,
        ILoggerAdapter<AuthService> logger)
    {
        UserManager = userManager;
        _claimService = claimService;
        TokenService = tokenService;
        _logger = logger;
    }


    public async Task<IUser?> GetUserAsync(string userName)
    {
        _logger.LogInformation("finding user with userName: {0}", userName);
        var user = await UserManager.FindByNameAsync(userName);
        _logger.LogInformation(user is null ? "user not found" : "user found");
        return user;
    }

    public Task<IdentityResult> RegisterAsync(string userName, string password) => UserManager.CreateAsync(new RsaUser { UserName = userName }, password);

    public async Task<string?> AuthenticateAsync(string userName, string password)
    {
        var user = await GetUserAsync(userName);
        if (user is null)
        {
            _logger.LogError("user not found" );
            return null;
        }
        var isPasswordValid = await UserManager.CheckPasswordAsync(user, password);
        if (!isPasswordValid)
        {
            _logger.LogError( "password isn't correct");
            return null;
        }
        var roles = await UserManager.GetRolesAsync(user);
        var token = TokenService.GenerateToken(user.Id.ToString(), user.UserName!, roles);
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
        var user = await UserManager.FindByNameAsync(userName);
        if (user is null)
            _logger.LogError("user with username = {0} not found! but it was in the token!", userName);
        _logger.LogInformation("user was find {0}",user?.UserName);
        return user;
    }
}