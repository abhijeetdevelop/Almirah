using SQLite;
using Almirah.Models;
using Almirah.Services.Interfaces;

public class DatabaseService : IDatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService()
    {
    }

    public DatabaseService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Note>().Wait();
    }

    public Task<List<Note>> GetNotesAsync()
    {
        return _database.Table<Note>().ToListAsync();
    }

    public Task<Note> GetNoteAsync(int id)
    {
        return _database.Table<Note>().Where(n => n.Id == id).FirstOrDefaultAsync();
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