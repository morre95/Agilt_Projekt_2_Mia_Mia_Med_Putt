using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Pages
{
    /// <summary>
    /// Page with "About"-content
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
