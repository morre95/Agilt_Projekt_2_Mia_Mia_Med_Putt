using Agilt_Projekt_2_Mia_Mia_Med_Putt.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt
{
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
            // Handle Start button click
            // Add your logic here
            Frame.Navigate(typeof(GameBoard));
        }

        private void InstructionButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle Instructioner button click
            // Add your logic here
            Frame.Navigate(typeof(InstructionPage));
        }

        private void OmButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle Om button click
            // Add your logic here
        }

        private async void AvslutaButton_Click(object sender, RoutedEventArgs e)
        {
            await ExitDialog.ShowAsync();
        }

        private void ExitDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Application.Current.Exit();
        }

        private void ExitDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            //Stängs per automatik om man väljer "NEJ", kod överflödig
        }


        private async Task Timer()
        {
            await Task.Delay(5000);
        }
    }
}
