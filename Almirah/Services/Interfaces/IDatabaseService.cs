using Almirah.Models;

namespace Almirah.Services.Interfaces;

public interface IDatabaseService
{
    Task<List<Note>> GetNotesAsync();
    Task<int> SaveNoteAsync(Note note);
    Task<int> DeleteNoteAsync(Note note);
}