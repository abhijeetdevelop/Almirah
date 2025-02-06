using Almirah.Services.Interfaces;
using Almirah.ViewModels.Notes;

namespace Almirah.Views.Notes;

public partial class MainPage : ContentPage
{
    private readonly IAuthService _authService;

    public MainPage(MainPageVM vm, IAuthService authService)
    {
        try
        {
            InitializeComponent();
            _authService = authService;

            _ = AuthenticateAndInitialize();
            BindingContext = vm;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw;
        }   
    }
    
    private async Task AuthenticateAndInitialize()
    {
        bool isAuthenticated = await _authService.AuthenticateAsync();
        if (!isAuthenticated)
        {            
            Environment.Exit(0);
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