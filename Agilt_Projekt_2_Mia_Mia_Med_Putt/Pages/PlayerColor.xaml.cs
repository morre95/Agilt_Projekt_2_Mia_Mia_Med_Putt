using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes;
using System.Diagnostics;
using System.Collections.Generic;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Pages
{
    public class PlayerSelected
    {
        public Dictionary<PawnColor, bool> ColorSelected { get; set; }
        public string RedName { get; set; }
        public string GreenName { get; set; }
        public string YellowName { get; set; }
        public string BlueName { get; set; }

        public PlayerSelected(string redName, string greenName, string yellowName, string blueName, params KeyValuePair<PawnColor, bool>[] colorSelected)
        {
            ColorSelected = new Dictionary<PawnColor, bool>(colorSelected);
            RedName = redName;
            GreenName = greenName;
            YellowName = yellowName;
            BlueName = blueName;
        }
    }

    public sealed partial class PlayerColor : Page
    {
        public PlayerColor()
        {
            this.InitializeComponent();
        }

        // Modify the StartButton_Click method inside the PlayerColor.xaml.cs file
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedColorItem = (ComboBoxItem)ColorComboBox.SelectedItem;
            if (selectedColorItem != null)
            {
                string selectedColor = selectedColorItem.Content.ToString();
                PawnColor color;

                switch (selectedColor)
                {
                    case "Röd":
                        color = PawnColor.Red;
                        break;
                    case "Blå":
                        color = PawnColor.Blue;
                        break;
                    case "Gul":
                        color = PawnColor.Yellow;
                        break;
                    case "Grön":
                        color = PawnColor.Green;
                        break;
                    default:
                        color = PawnColor.Red;
                        break;
                }

                Debug.WriteLine(color.ToString());
                Frame.Navigate(typeof(GameBoard), color);

                //Frame.Navigate(typeof(GameBoard), new PlayerSelected("Röd", "Grön", "Gulan", "Blåbäret", new KeyValuePair<PawnColor, bool>(color, true)));
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        /*private void ChooseColor_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected color from ComboBox
            ComboBoxItem selectedColorItem = (ComboBoxItem)ColorComboBox.SelectedItem;
            if (selectedColorItem != null)
            {
                string selectedColor = selectedColorItem.Content.ToString();

                // Map color names to actual color values
                PawnColor color;
                switch (selectedColor)
                {
                    case "Gul":
                        color = PawnColor.Yellow;
                        break;
                    case "Grön":
                        color = PawnColor.Green;
                        break;
                    case "Röd":
                        color = PawnColor.Red;
                        break;
                    case "Blå":
                        color = PawnColor.Blue;
                        break;
                    default:
                        color = PawnColor.Red; // Default color
                        break;
                }

                // Set the player color in MainPage
                if (Window.Current.Content is Frame rootFrame)
                {
                    if (rootFrame.Content is MainPage mainPage)
                    {
                        mainPage.SetPlayerColor(Colors.Blue);
                    }
                }




                Frame.Navigate(typeof(GameBoard), color);
                // Navigate back to the main page
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged_1()
        {

        }*/
    }
}
