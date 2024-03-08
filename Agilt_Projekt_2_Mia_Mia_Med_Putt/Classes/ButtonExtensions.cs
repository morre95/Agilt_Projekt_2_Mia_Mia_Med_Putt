using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes
{
    public static class ButtonExtensions
    {
        public static bool GetChangeCursorOnHover(DependencyObject obj) => (bool)obj.GetValue(ChangeCursorOnHoverProperty);

        public static void SetChangeCursorOnHover(DependencyObject obj, bool value) => obj.SetValue(ChangeCursorOnHoverProperty, value);

        public static readonly DependencyProperty ChangeCursorOnHoverProperty =
            DependencyProperty.RegisterAttached(
                "ChangeCursorOnHover",
                typeof(bool),
                typeof(ButtonExtensions),
                new PropertyMetadata(false, OnChangeCursorOnHoverChanged));

        private static void OnChangeCursorOnHoverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Button button)
            {
                if ((bool)e.NewValue)
                {
                    button.PointerEntered += Button_PointerEntered;
                    button.PointerExited += Button_PointerExited;
                }
                else
                {
                    button.PointerEntered -= Button_PointerEntered;
                    button.PointerExited -= Button_PointerExited;
                }
            }

            if (d is Image buttonImage)
            {
                if ((bool)e.NewValue)
                {
                    buttonImage.PointerEntered += Button_PointerEntered;
                    buttonImage.PointerExited += Button_PointerExited;
                }
                else
                {
                    buttonImage.PointerEntered -= Button_PointerEntered;
                    buttonImage.PointerExited -= Button_PointerExited;
                }                   
            }

        }

        private static void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Hand, 0);
        }

        private static void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 0);
        }
    }
}
