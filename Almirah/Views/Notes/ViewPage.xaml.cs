using Almirah.Models;
using Almirah.ViewModels.Notes;
using Microsoft.Maui.Controls;

namespace Almirah.Views.Notes;

[QueryProperty(nameof(Note), "note")]
public partial class ViewPage : ContentPage
{
    private Note _note;

    public Note Note
    {
        get => _note;
        set
        {
            _note = value;

            // Set the SelectedNote in the ViewModel to the passed Note
            if (BindingContext is ViewVM vm)
            {
                vm.SelectedNote = _note ?? new Note();
            }
        }
    }

    public ViewPage(ViewVM viewVM)
    {
        InitializeComponent();
        BindingContext = viewVM;
    }
}