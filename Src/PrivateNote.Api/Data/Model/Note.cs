using PrivateNote.Api.Data.Entity;

namespace PrivateNote.Api.Data.Model;
public class Note : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
}
