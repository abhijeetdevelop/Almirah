using System;
using System.Threading.Tasks;
using Almirah.ViewModels.Notes;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Authentication;

namespace Almirah.Views.Notes;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageVM vm)
    {
        try
        {
            InitializeComponent();
            BindingContext = vm;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw;
        }   
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MainPageVM vm)
        { 
           await vm.OnAppearing(); // Ensure LoadFiles is triggered here
        }
    }
}