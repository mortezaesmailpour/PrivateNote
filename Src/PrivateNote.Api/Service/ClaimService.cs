namespace PrivateNote.Service;

public class ClaimService : IClaimService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var userNameClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        if (string.IsNullOrEmpty(userNameClaim?.Value))
            throw new KeyNotFoundException(ClaimTypes.Name + " is not in the Claims");
        return Guid.Parse(userNameClaim.Value);
    }

    public string GetUserName()
    {
        var userNameClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        if (string.IsNullOrEmpty(userNameClaim?.Value))
            throw new KeyNotFoundException(ClaimTypes.Name + " is not in the Claims");
        return userNameClaim.Value;
    }
}