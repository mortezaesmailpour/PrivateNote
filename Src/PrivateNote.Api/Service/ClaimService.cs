﻿namespace PrivateNote.Service;

public class ClaimService : IClaimService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILoggerAdapter<ClaimService> _logger;

    public ClaimService(IHttpContextAccessor httpContextAccessor, ILoggerAdapter<ClaimService> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public Guid? GetUserId()
    {
        var userNameClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userNameClaim?.Value))
        {
            _logger.LogError( "{0} is not in the Claims",ClaimTypes.Name);
            return null;
        }
        //return Guid.Parse(userNameClaim.Value);
        
        Guid userId;
        Guid.TryParse(userNameClaim.Value,out userId);
        return userId;
    }

    public string? GetUserName()
    {
        var userNameClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        if (string.IsNullOrEmpty(userNameClaim?.Value))
            throw new KeyNotFoundException(ClaimTypes.Name + " is not in the Claims");
        return userNameClaim.Value;
    }
}