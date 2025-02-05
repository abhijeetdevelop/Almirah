using System;
using System.Threading.Tasks;
using Almirah.Models;
using Almirah.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace Almirah.ViewModels.Notes;

public class ViewVM : BindableObject
{
    private readonly IDatabaseService _databaseService;
    
    private bool _isNoteNew;
    public bool IsNoteNew
    {
        get => _isNoteNew;
        set
        {
            _isNoteNew = value;
            OnPropertyChanged();
        }
    }

    private Note _selectedNote;
    public Note SelectedNote
    {
        get => _selectedNote;
        set
        {
            _selectedNote = value;
            
            IsNoteNew = _selectedNote?.Id == 0;
            OnPropertyChanged();
        }
    }

    public IAsyncRelayCommand SaveNoteCommand { get; }
    public IAsyncRelayCommand CancelCommand { get; }

    public ViewVM(IDatabaseService _databaseService)
    {
        this._databaseService = _databaseService;

        SaveNoteCommand = new AsyncRelayCommand(SaveNoteAsync);
        CancelCommand = new AsyncRelayCommand(CancelAsync);
    }

    private async Task CancelAsync()
    {
        //await DisplayPromptAsync("Cancel", "Are you sure you want to cancel the note?", "OK");
        await Shell.Current.GoToAsync("///MainPage");
    }

    private async Task SaveNoteAsync()
    {
        if (_selectedNote == null)
            throw new ArgumentNullException(nameof(_selectedNote), "Note cannot be null.");

        await _databaseService.SaveNoteAsync(_selectedNote);
        await Shell.Current.GoToAsync("///MainPage");
    }
}