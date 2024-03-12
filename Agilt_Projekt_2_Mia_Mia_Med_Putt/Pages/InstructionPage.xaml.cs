using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Pages
{
    /// <summary>
    /// Page with instructions for the gameplay
    /// </summary>
    public sealed partial class InstructionPage : Page
    {
        public InstructionPage()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
                Frame.GoBack();
        }
    }
}
