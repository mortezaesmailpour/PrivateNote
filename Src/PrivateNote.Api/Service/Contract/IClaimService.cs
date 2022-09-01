namespace PrivateNote.Service.Contract;

public interface IClaimService
{
    Guid? GetUserId();
    string? GetUserName();
}