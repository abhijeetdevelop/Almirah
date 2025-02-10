using SQLite;
using Almirah.Models;
using Almirah.Services.Interfaces;

namespace Almirah.Services.Notes;

public class DatabaseService : IDatabaseService
{
    private readonly SQLiteAsyncConnection _database;
    private readonly IEncryptionService _encryptionService;

    public DatabaseService(string dbPath, IEncryptionService encryptionService)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _encryptionService = encryptionService;
        _database.CreateTableAsync<Note>().Wait();
    }

    public async Task<List<Note>> GetNotesAsync()
    {
        var notes = await _database.Table<Note>().ToListAsync();
        foreach (var note in notes)
        {
            note.InitializeEncryptionService(_encryptionService);
        }
        return notes;
    }

    public Task<int> SaveNoteAsync(Note note)
    {
        if (note.Id != 0)
        {
            note.UpdatedAt = DateTime.UtcNow;
            return _database.UpdateAsync(note);
        }
        else
        {
            return _database.InsertAsync(note);
        }
    }

    public Task<int> DeleteNoteAsync(Note note)
    {
        return _database.DeleteAsync(note);
    }
}
