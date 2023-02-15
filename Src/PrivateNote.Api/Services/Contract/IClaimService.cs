namespace PrivateNote.Api.Services.Contract;

public interface IClaimService
{
    Guid? GetUserId();
    string? GetUserName();
}