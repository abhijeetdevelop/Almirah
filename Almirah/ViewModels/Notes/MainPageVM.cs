using System.Collections.ObjectModel;
using Almirah.Models;
using Almirah.Services.Interfaces;
using Almirah.Views.Notes;
using CommunityToolkit.Mvvm.Input;

namespace Almirah.ViewModels.Notes;

public class MainPageVM : BindableObject
{
    private readonly IDatabaseService _databaseService;
    private readonly IAuthService _authService;

    public ObservableCollection<Note> Notes { get; } = new();


    private Note _selectedNote;

    public Note SelectedNote
    {
        get => _selectedNote;
        set
        {
            _selectedNote = value;
            OnPropertyChanged();

            if (_selectedNote != null) OpenNoteAsync(_selectedNote);
        }
    }

    private ObservableCollection<Note> _filteredNotes;

    public ObservableCollection<Note> FilteredNotes
    {
        get => _filteredNotes;
        set
        {
            _filteredNotes = value;
            OnPropertyChanged();
        }
    }

    private string _searchText;

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            SearchNotes();
        }
    }

    public IAsyncRelayCommand LoadNotesCommand { get; }
    public IAsyncRelayCommand AddNoteCommand { get; }
    public IAsyncRelayCommand DeleteNoteCommand { get; }

    public MainPageVM(IDatabaseService databaseService, IAuthService authService)
    {
        _databaseService = databaseService;
        _authService = authService;

        LoadNotesCommand = new AsyncRelayCommand(LoadNotesAsync);
        DeleteNoteCommand = new AsyncRelayCommand<Note>(DeleteNoteAsync);
        AddNoteCommand = new AsyncRelayCommand(AddNoteAsync);
    }

    private void SearchNotes()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            // If no search text, show all notes
            FilteredNotes = new ObservableCollection<Note>(Notes);
        }
        else
        {
            // Filter notes based on the search text
            var filtered = Notes.Where(n => n.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                                            || n.Content.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
            FilteredNotes = new ObservableCollection<Note>(filtered);
        }
    }

    private async Task OpenNoteAsync(Note selectedNote)
    {
        if (selectedNote == null) return; // Add null check for safety

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
        foreach (var note in notes) Notes.Add(note);

        FilteredNotes = new ObservableCollection<Note>(notes);
    }

    private async Task DeleteNoteAsync(Note note)
    {
        await _databaseService.DeleteNoteAsync(note);
        await LoadNotesAsync();
    }

    public async Task OnAppearing()
    {
        await LoadNotesAsync();
    }
}