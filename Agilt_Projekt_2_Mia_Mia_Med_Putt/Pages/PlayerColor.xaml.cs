using Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayerColor : Page
    {
        public PlayerColor()
        {
            this.InitializeComponent();
        }
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        ComboBoxItem selectedColorItem = (ComboBoxItem)ColorComboBox.SelectedItem;
        if (selectedColorItem != null)
        {
            string selectedColor = selectedColorItem.Name.ToString();
            PawnColor color;

            switch (selectedColor)
            {
                case "Red":
                    color = PawnColor.Red;
                    break;
                case "Blue":
                    color = PawnColor.Blue;
                    break;
                case "Yellow":
                    color = PawnColor.Yellow;
                    break;
                case "Green":
                    color = PawnColor.Green;
                    break;
                default:
                    color = PawnColor.Red;
                    break;
            }

            Frame.Navigate(typeof(GameBoard), color);
        }
    }


    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.GoBack();
    }
}
