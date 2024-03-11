using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using System.Threading.Tasks;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Pages
{
    /// <summary>
    /// In-game menu that allows the player to access the rules and exit the game during gameplay
    /// </summary>
    public sealed partial class InGameMenu : Page
    {
        public InGameMenu()
        {
            this.InitializeComponent();
            upToDownAnimation.Begin();
        }

        // Function for opening the in-game menu through pushing the Esc-key on keyboard
        private async void Page_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                await BackToGame();
            }

        }

        // Function for opening the in-game menu through clicking on hamburger image
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            upToDownAnimation.Begin();
        }

        // Button to go back to game, hide the menu
        private async void BackToGameButton_Click(object sender, RoutedEventArgs e)
        {
            await BackToGame();
        }

        // Navigates back to the gameboard with animation
        private async Task BackToGame()
        {
            moveUpDialogAnimation.Begin();
            await Task.Delay(700);
            Frame.Navigate(typeof(GameBoard));
        }



        // Button to open up the instructions/rules of the game
        private void RulesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InstructionPage));
        }

        // Button to exit the game with a pop-up window to ask if the user is sure they want to exit
        private async void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog confirmDialog = new ContentDialog
            {
                Title = "Avsluta Mia med Putt",
                Content = "Är du säker att du vill avsluta spelet?",
                PrimaryButtonText = "Ja",
                CloseButtonText = "Tillbaka"

            };

            ContentDialogResult result = await confirmDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Application.Current.Exit();
            }
            
        }
    }
}
