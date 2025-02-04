using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Almirah.Models;

namespace Almirah.Services.Interfaces;

public interface IDatabaseService
{
    Task<List<Note>> GetNotesAsync();
    Task<Note> GetNoteAsync(int id);
    Task<int> SaveNoteAsync(Note note);
    Task<int> DeleteNoteAsync(int id);
}