using System;
using Almirah.ViewModels;
using Microsoft.Maui.Controls;

namespace Almirah.Views;

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