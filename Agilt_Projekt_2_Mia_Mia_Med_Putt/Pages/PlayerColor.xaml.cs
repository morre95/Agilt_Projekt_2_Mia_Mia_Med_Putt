using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes;
using System.Diagnostics;
using Windows.UI.Text;
using System.Collections.Generic;
using Windows.UI.Text;
using System.Drawing;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Pages
{
    public class GameBoardParameters
    {
        public List<PawnColor> ColorsSelected { get; set; }

        public GameBoardParameters(params PawnColor[] colorSelected)
        {
            ColorsSelected = new List<PawnColor>(colorSelected);

        }
    }

        /// <summary>
        public class GameBoardParameters
    {

        private ComboBox[] colorComboBoxes;

        public PlayerColor()
        {
            this.InitializeComponent();
        }
        /// </summary>

        // Modify the StartButton_Click method inside the PlayerColor.xaml.cs file
        private void NumPlayersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int numPlayers = NumPlayersComboBox.SelectedIndex + 1;

            // Toggle the visibility of the PlayersStackPanel based on the selected number of players
            PlayersStackPanel.Visibility = (numPlayers <= 4) ? Visibility.Visible : Visibility.Collapsed;

            // Clear the existing color selection controls
            PlayersStackPanel.Children.Clear();

            // Initialize the colorComboBoxes array
            colorComboBoxes = new ComboBox[numPlayers];

            // Create and add new player controls based on the selected number of players
            for (int i = 0; i < numPlayers; i++)
            {
                // Create a horizontal StackPanel for each player
                StackPanel playerPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 5, 0, 5),
                };

                // Create a TextBlock for displaying the player number
                TextBlock playerTextBlock = new TextBlock()
                {
                    Text = $"Player {i + 1}",
                    Margin = new Thickness(5),
                    FontWeight = FontWeights.Bold,
                    VerticalAlignment = VerticalAlignment.Center,
                };

                // Create a ComboBox for color selection
                ComboBox colorComboBox = new ComboBox()
                {
                    PlaceholderText = $"Choose color",
                    Width = 100,
                    FontSize = 12,
                    ItemsSource = new List<string> { "Röd", "Grön", "Blå", "Gul" },
                    Margin = new Thickness(5),
                };

                // Add the ComboBox to the colorComboBoxes array
                colorComboBoxes[i] = colorComboBox;

                // Add the player's TextBlock and ComboBox to the player's StackPanel
                playerPanel.Children.Add(playerTextBlock);
                playerPanel.Children.Add(colorComboBox);

                // Add the player's StackPanel to the main PlayersStackPanel
                PlayersStackPanel.Children.Add(playerPanel);
            }
        }

        //Frame.Navigate(typeof(GameBoard), new PlayerSelected("Röd", "Grön", "Gulan", "Blåbäret", new KeyValuePair<PawnColor, bool>(color, true)));

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int numPlayers = NumPlayersComboBox.SelectedIndex + 1;

            if (numPlayers == 5)
            {
                // Handle case when 4 players are selected (e.g., show a message or navigate to the next page)
                Debug.WriteLine("You selected 4 players. Implement the desired behavior.");
                return;
            }

            PawnColor[] playerColors = new PawnColor[numPlayers];
            HashSet<string> uniqueColors = new HashSet<string>(StringComparer.OrdinalIgnoreCase); // Case-insensitive comparison

            for (int i = 0; i < numPlayers; i++)
            {
                string selectedColor = (string)colorComboBoxes[i].SelectedItem;

                if (!string.IsNullOrEmpty(selectedColor) && uniqueColors.Add(selectedColor))
                {
                    //playerColors[i] = selectedColor;
                    switch (selectedColor)
                    {
                        case "Röd":
                            playerColors[i] = PawnColor.Red;
                            break;
                        case "Grön":
                            playerColors[i] = PawnColor.Green;
                            break;
                        case "Gul":
                            playerColors[i] = PawnColor.Yellow;
                            break;
                        case "Blå":
                            playerColors[i] = PawnColor.Blue;
                            break;
                    }
                }
                else
                {
                    // Handle case where a color is not selected or a duplicate color is chosen
                    Debug.WriteLine($"Player {i + 1} color not selected or duplicate color.");
                    // Show an alert or message to the user
                    // You might want to replace this with your specific logic
                    ShowColorNotSelectedOrDuplicateAlert();
                    return;
                }
            }

            Debug.WriteLine($"Selected Players: {numPlayers}"); // Add this line for debugging

            // Navigate to the GameBoard page and pass the selected data
            //Frame.Navigate(typeof(GameBoard), new GameBoardParameters { NumPlayers = numPlayers, PlayerColors = playerColors });

            Frame.Navigate(typeof(GameBoard), new GameBoardParameters(playerColors));
        }


        private async void ShowColorNotSelectedOrDuplicateAlert()
        {
            var dialog = new Windows.UI.Popups.MessageDialog("Please select a unique color for each player.", "Color Not Selected or Duplicate");
            await dialog.ShowAsync();
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
