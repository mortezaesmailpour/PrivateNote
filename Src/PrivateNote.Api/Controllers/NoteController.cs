using PrivateNote.Api.Contracts.Requests;
using PrivateNote.Api.Contracts.Responses;
using PrivateNote.Api.Data;
using PrivateNote.Api.Data.Model;
using PrivateNote.Api.Services.Contract;

namespace PrivateNote.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly PrivateNoteDbContext _context;
    private readonly INoteService _noteService;


    public NoteController(INoteService noteService, PrivateNoteDbContext context)
    {
        _noteService = noteService;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RsaNote>>> GetNotes()
    {
        var notes = await _noteService.GetMyNotesAsync();
        return Ok(notes);
    }
    
    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<RsaNote>>> GetAllNotes()
    {
        var notes = await _noteService.GetAllNotes();
        return Ok(notes);
    }
    
    [HttpPost]
    public async Task<ActionResult<CreateNoteResponse>> PostNote(CreateNoteRequest request)
    {
        var result = await _noteService.CreateNoteAsync(request);
        if (result is null)
            return BadRequest();
        return Ok(new CreateNoteResponse { NoteId = result.Id, LastModificationTime = result.LastModificationTime });
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<RsaNote>> GetNote(Guid id)
    {
        var note = await _noteService.GetNoteAsync(id);
        if (note is null)
            return NotFound();
        return Ok(note);
    }

    [HttpPut]
    public async Task<ActionResult<UpdateNoteResponse>> PutNote(UpdateNoteRequest request)
    {
        var result = await _noteService.UpdateNoteAsync(request);
        if (result is null)
            return BadRequest();
        return Ok(new UpdateNoteResponse { NoteId = result.Id, LastModificationTime = result.LastModificationTime });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(Guid id)
    {

        var result = await _noteService.DeleteNoteAsync(id);
        if (!result)
            return Problem();
        return NoContent();
    }
}
