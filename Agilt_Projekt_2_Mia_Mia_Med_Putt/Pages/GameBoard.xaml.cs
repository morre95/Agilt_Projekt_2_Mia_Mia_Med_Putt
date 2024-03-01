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
    /// The Page with the game board
    /// </summary>
    public sealed partial class GameBoard : Page
    {
        /// <summary>
        /// The pawns of the red player.
        /// </summary>
        private PlayerPawns redPlayer;

        /// <summary>
        /// The pawns of the blue player.
        /// </summary>
        private PlayerPawns bluePlayer;

        /// <summary>
        /// The pawns of the yellow player.
        /// </summary>
        private PlayerPawns yellowPlayer;

        /// <summary>
        /// The pawns of the green player.
        /// </summary>
        private PlayerPawns greenPlayer;

        /// <summary>
        /// The actual size (width and height) of each square on the grid.
        /// </summary>
        private int squareSide = 60;

        /// <summary>
        /// Gets the height of a square on the grid.
        /// </summary>
        private int SquareHeight { get { return squareSide; } }

        /// <summary>
        /// Gets the width of a square on the grid.
        /// </summary>
        private int SquareWidth { get { return squareSide; } }

        /// <summary>
        /// The grid representing the game board.
        /// </summary>
        private Grid grid;

        /// <summary>
        /// List of all players (a list of PlayerPawns class).
        /// </summary>
        private List<PlayerPawns> playerPawns = new List<PlayerPawns>();

        /// <summary>
        /// Index of the current player.
        /// </summary>
        private int currentIndex = 0;

        /// <summary>
        /// Gets the current player whose turn it is.
        /// </summary>
        private PlayerPawns currentPlayer
        {
            get
            {
                if (currentIndex + 1 > playerPawns.Count)
                    currentIndex = 0;
                return playerPawns[currentIndex];
            }
        }

        /// <summary>
        /// Page constructor. It initializes the page, setup players and draw them on to the game board.
        /// </summary>
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

            // if the player has no pawns in play, bring one up so the variable pawn is not null.
            if (player.GetPawnsInPlay().Count() == 0)
            {
                pawn = player.NextPawnInNest();
            }

            int diceRoll = RollDice();

            int pawnsInNest = player.GetPawnsInNest().Count();

            
            if (
                // If the dice shows 1, bring one pawn to the gameboard if there is one.
                (pawnsInNest >= 1 && diceRoll == 1) ||
                // If the dice shows 6 and there is one pawn in the nest, bring one pawn to the gameboard.
                (pawnsInNest == 1 && diceRoll == 6)
                )
            {
                pawn = player.NextPawnInNest();
                pawn.NextPosition();
                DrawPlayers();
                return;
            }
            // If the dice shows 6, bring two pawns to the gameboard if there are two or more pawns in the nest.
            else if (pawnsInNest >= 2 && diceRoll == 6)
            {
                pawn = player.NextPawnInNest();
                pawn.NextPosition();
                DrawPlayers();

                pawn = player.NextPawnInNest();
                pawn.NextPosition();
                DrawPlayers();
                return;
            }

            // Move the pawn the steps the dice shows and sleep 100 ms.
            for (int i = 0; i < diceRoll; i++)
            {
                pawn.NextPosition();
                DrawPlayers();
                await Task.Delay(100);
            }
            

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

        private void RollDiceButton_Click(object sender, RoutedEventArgs e)
        {
            int result = RollDice();
            Debug.WriteLine($"Resultat: {result}");
        }

        
        private int RollDice()
        {
            Random random = new Random();
            return random.Next(1,7);
        }
    }
}
