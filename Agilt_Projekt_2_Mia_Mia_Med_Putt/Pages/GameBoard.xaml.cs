using Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameBoard : Page
    {
        private PlayerPawns redPlayer;
        private PlayerPawns bluePlayer;
        private PlayerPawns yellowPlayer;
        private PlayerPawns greenPlayer;

        private int squareSide = 60;

        private int SquareHeight { get { return squareSide; } }
        private int SquareWidth { get { return squareSide; } }

        private Grid grid;

        private List<PlayerPawns> playerPawns = new List<PlayerPawns>();

        private int currentIndex = 0;

        private PlayerPawns currentPlayer
        {
            get
            {
                if (currentIndex + 1 > playerPawns.Count) currentIndex = 0;
                return playerPawns[currentIndex];
            }
        }

        public GameBoard()
        {
            InitializeComponent();

            SetUpPlayers();

            DrawPlayers();
        }

        private void PageSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = e.NewSize.Width;
            double height = e.NewSize.Height;

            Debug.WriteLine($"width: {width}, height: {height}");

            //squareSide = Math.Min(Scale(width, 500, 2560, 20, 120), Scale(height, 319, 1360, 20, 120));

            //DrawPlayers();
        }

        private new int Scale(double value, int min, int max, int minScale, int maxScale)
        {
            int scaled = Convert.ToInt32(minScale + (double)(value - min) / (max - min) * (maxScale - minScale));
            return scaled;
        }


        private void SetUpGrid()
        {
            GridCanvas.Children.Clear();

            grid = new Grid(11, 11);
            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    Size size = new Size(SquareWidth, SquareHeight);
                    grid.SetValue(x, y, size);

                    DrawRectangle(x, y, size);
                }
            }
        }

        private void DrawRectangle(int x, int y, Size size)
        {
            Rectangle rectangle = new Rectangle()
            {
                Width = size.Width,
                Height = size.Height
            };

            Canvas.SetTop(rectangle, x * size.Width);
            Canvas.SetLeft(rectangle, y * size.Height);
            GridCanvas.Children.Add(rectangle);
        }

        private void SetUpPlayers()
        {
            AddPawnsToPlayer();

            playerPawns.Add(redPlayer);
            playerPawns.Add(bluePlayer);
            playerPawns.Add(yellowPlayer);
            playerPawns.Add(greenPlayer);
        }

        private void AddPawnsToPlayer()
        {
            AddRedPawns();
            AddBluePawns();
            AddYellowPawns();
            AddGreenPawns();
        }

        private void AddGreenPawns()
        {
            Pawn green1 = new Pawn("Green Pawn 1", PawnPaths.Green, new Point(8, 1));
            Pawn green2 = new Pawn("Green Pawn 2", PawnPaths.Green, new Point(9, 1));
            Pawn green3 = new Pawn("Green Pawn 3", PawnPaths.Green, new Point(8, 2));
            Pawn green4 = new Pawn("Green Pawn 4", PawnPaths.Green, new Point(9, 2));
            greenPlayer = new PlayerPawns("Green Player", green1, green2, green3, green4);
        }

        private void AddYellowPawns()
        {
            Pawn yellow1 = new Pawn("Yellow Pawn 1", PawnPaths.Yellow, new Point(8, 8));
            Pawn yellow2 = new Pawn("Yellow Pawn 2", PawnPaths.Yellow, new Point(9, 8));
            Pawn yellow3 = new Pawn("Yellow Pawn 3", PawnPaths.Yellow, new Point(8, 9));
            Pawn yellow4 = new Pawn("Yellow Pawn 4", PawnPaths.Yellow, new Point(9, 9));
            yellowPlayer = new PlayerPawns("Yellow Player", yellow1, yellow2, yellow3, yellow4);
        }

        private void AddBluePawns()
        {
            Pawn blue1 = new Pawn("Blue Pawn 1", PawnPaths.Blue, new Point(2, 8));
            Pawn blue2 = new Pawn("Blue Pawn 2", PawnPaths.Blue, new Point(1, 8));
            Pawn blue3 = new Pawn("Blue Pawn 3", PawnPaths.Blue, new Point(2, 9));
            Pawn blue4 = new Pawn("Blue Pawn 4", PawnPaths.Blue, new Point(1, 9));
            bluePlayer = new PlayerPawns("Blue Player", blue1, blue2, blue3, blue4);
        }

        private void AddRedPawns()
        {
            Pawn red1 = new Pawn("Red Pawn 1", PawnPaths.Red, new Point(2, 1));
            Pawn red2 = new Pawn("Red Pawn 2", PawnPaths.Red, new Point(1, 1));
            Pawn red3 = new Pawn("Red Pawn 3", PawnPaths.Red, new Point(2, 2));
            Pawn red4 = new Pawn("Red Pawn 4", PawnPaths.Red, new Point(1, 2));
            redPlayer = new PlayerPawns("Red Player", red1, red2, red3, red4);
        }

        private void DrawPlayers()
        {
            SetUpGrid();
            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    Size currentDimensions = grid.GetValue(x, y);
                    Point gridLocation = new Point(x, y);
                    DrawIfPawnExists(currentDimensions, gridLocation);
                }
            }
        }

        private void DrawIfPawnExists(Size currentDimensions, Point gridLocation)
        {
            if (redPlayer.IsMyPawnAt(gridLocation))
            {
                DrawPawn(gridLocation, currentDimensions, "Red");
            }

            if (bluePlayer.IsMyPawnAt(gridLocation))
            {
                DrawPawn(gridLocation, currentDimensions, "Blue");
            }

            if (yellowPlayer.IsMyPawnAt(gridLocation))
            {
                DrawPawn(gridLocation, currentDimensions, "Yellow");
            }

            if (greenPlayer.IsMyPawnAt(gridLocation))
            {
                DrawPawn(gridLocation, currentDimensions, "Green");
            }
        }

        private void DrawPawn(Point gridLocation, Size currentDimensions, string pawnColor)
        {
            Image img = new Image();
            BitmapImage bitmapImage = new BitmapImage();

            Uri uri = new Uri($"ms-appx:///Assets/Board/Pawns/{pawnColor}.png");

            bitmapImage.UriSource = uri;
            img.Source = bitmapImage;

            img.Width = currentDimensions.Width;
            img.Height = currentDimensions.Height;

            Canvas.SetTop(img, (gridLocation.X * currentDimensions.Width));
            Canvas.SetLeft(img, (gridLocation.Y * currentDimensions.Height));
            GridCanvas.Children.Add(img);
        }


        private async Task RunGameAsync()
        {
            PlayerPawns player = currentPlayer;
            Pawn pawn = player.NextPawnInPlay();

            if (player.GetPawnsInPlay().Count() == 0)
            {
                pawn = player.NextPawnInNest();
            }

            pawn.NextPosition();
            DrawPlayers();

            if (pawn.IsAtEnd())
            {
                // TBD: Detta är bara för att få ett meddelande när en pjäs går i mål
                ContentDialog noWifiDialog = new ContentDialog
                {
                    Title = $"{player.Name} Vann!!!!",
                    Content = $"Är det {player.Name} som är bäste eller?",
                    CloseButtonText = "Ja det tycker jag"
                };

                ContentDialogResult result = await noWifiDialog.ShowAsync();
            }

            currentIndex++;
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await RunGameAsync();
        }
    }
}
