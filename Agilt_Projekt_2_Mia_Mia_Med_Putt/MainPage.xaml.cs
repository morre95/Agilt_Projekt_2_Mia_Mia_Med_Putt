using Agilt_Projekt_2_Mia_Mia_Med_Putt.Pages;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt
{
    /// <summary>
    /// Page for the mainmenu
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            // _ = behövs för att VS inte ska ge en grön orm
            _ = Timer();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PlayerColor));
        }

        private void InstructionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InstructionPage));
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        private async void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            await ExitDialog.ShowAsync();
        }

        private void ExitDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Application.Current.Exit();
        }

        private void ExitDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //Stängs per automatik om man väljer "NEJ", kod överflödig men metod nödvändig
        }


        private async Task Timer()
        {
            await Task.Delay(5000);
        }
    }
}
