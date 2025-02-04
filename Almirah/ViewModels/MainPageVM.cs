using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Almirah.Models;
using Almirah.Services.Interfaces;
using Almirah.Views.Notes;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Almirah.ViewModels;

public class MainPageVM : BindableObject
{
    private readonly IDatabaseService _databaseService;
    public ObservableCollection<Note> Notes { get; } = new();

    private Note _selectedNote;
    public Note SelectedNote
    {
        get => _selectedNote;
        set
        {
            _selectedNote = value;
            OnPropertyChanged();
            
            if (_selectedNote != null)
            {
                OpenNoteAsync(_selectedNote);
            }
        }
    }
    public IAsyncRelayCommand LoadNotesCommand { get; }
    public IAsyncRelayCommand AddNoteCommand { get; }
    public IAsyncRelayCommand<Note> DeleteNoteCommand { get; }


    public MainPageVM(IDatabaseService _databaseService)
    {
        this._databaseService = _databaseService;
        LoadNotesCommand = new AsyncRelayCommand(LoadNotesAsync);
        DeleteNoteCommand = new AsyncRelayCommand<Note>(DeleteNoteAsync);
        AddNoteCommand = new AsyncRelayCommand(AddNoteAsync);
    }
    
    private async Task OpenNoteAsync(Note selectedNote)
    {
        // Navigate to ViewPage with the selected note
        await Shell.Current.GoToAsync($"//{nameof(ViewPage)}", new Dictionary<string, object>
        {
            ["note"] = selectedNote
        });
    }

    private async Task AddNoteAsync()
    {
        // Navigate to the NoteDetailPage with a new note
        var newNote = new Note();
        await Shell.Current.GoToAsync($"//{nameof(ViewPage)}", new Dictionary<string, object>
        {
            ["note"] = newNote
        });
    }

    private async Task LoadNotesAsync()
    {
        var notes = await _databaseService.GetNotesAsync();
        Notes.Clear();
        foreach (var note in notes)
        {
            Notes.Add(note);
        }
    }

    private async Task DeleteNoteAsync(Note note)
    {
        await _databaseService.DeleteNoteAsync(note.Id);
        await LoadNotesAsync();
    }

    public async Task OnAppearing()
    {
        await LoadNotesAsync(); // Ensure notes are loaded when the page appears
    }
}