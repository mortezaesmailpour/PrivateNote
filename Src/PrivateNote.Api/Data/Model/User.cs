using PrivateNote.Api.Services.Contract;

namespace PrivateNote.Api.Data.Model;
public class User : IdentityUser<Guid>, IUser { }