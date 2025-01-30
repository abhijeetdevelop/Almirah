using Almirah.ViewModels;

namespace Almirah.Views
{
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
    }
}