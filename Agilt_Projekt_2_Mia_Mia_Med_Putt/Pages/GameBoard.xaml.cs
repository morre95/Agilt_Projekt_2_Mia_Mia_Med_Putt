﻿using Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
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

                foreach(PlayerPawns p in playerPawns)
                {
                    p.IsActive = false;
                }

                PlayerPawns player = playerPawns[currentIndex];
                player.IsActive = true;
                return player;
            }
        }

        /// <summary>
        /// Creates timer from roll-animation
        /// </summary>
        private DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Randomizer-variable for the dice roll
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Intedger representing the final dice roll
        /// </summary>
        private int finalDiceRollResult;

        /// <summary>
        /// Ractangle ogject that is going to be removed from the grid when not hovering over it anymore.
        /// </summary>
        private Rectangle RectangleToRemove { get; set; }

        /// <summary>
        /// Page constructor. It initializes the page, setup players and draw them on to the game board.
        /// </summary>
        public GameBoard()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Sets color for the user selected players
        /// </summary>
        /// <param name="color"></param>
        private void SetPlayerColor(PawnColor color)
        {
            foreach (PlayerPawns player in playerPawns)
            {
                if (player.Color == color)
                {

                    player.IsSelectedPlayer = true;
                }
            }
        }

        /// <summary>
        /// Runs on nanigating to this page. And sets the color for the selected players.
        /// </summary>
        /// <param name="e">NavigationEventArgs event object</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is GameBoardParameters gameBoardParameters)
            {
                SetUpPlayers();
                foreach (PawnColor pawnColor in gameBoardParameters.ColorsSelected)
                {
                    Debug.WriteLine($"PawnColor player: {pawnColor}");
                    SetPlayerColor(pawnColor);
                }
                DrawPlayers();

                timer.Interval = TimeSpan.FromMilliseconds(60);

                timer.Tick += Timer_Tick;

                Uri imageUri = new Uri($"ms-appx:///Assets/Board/Dice/D{random.Next(1, 7)}.png");
                DicePic.Source = new BitmapImage(imageUri);

                DicePic.PointerReleased += DicePic_PointerReleased;

                AddStatusTextToTop($"{currentPlayer.Name} spelares tur", 8);
            }   
        }

        /// <summary>
        /// Set up the grid system
        /// </summary>
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
                }
            }
        }

        /// <summary>
        /// Sets up the players
        /// </summary>
        private void SetUpPlayers()
        {
            AddPawnsToPlayer();

            playerPawns.Add(redPlayer);
            playerPawns.Add(bluePlayer);
            playerPawns.Add(yellowPlayer);
            playerPawns.Add(greenPlayer);
        }

        /// <summary>
        /// Adds the pawns to the players
        /// </summary>
        private void AddPawnsToPlayer()
        {
            AddRedPawns();
            AddBluePawns();
            AddYellowPawns();
            AddGreenPawns();
        }

        /// <summary>
        /// Adds the green pawns to the greenPlayer variable
        /// </summary>
        private void AddGreenPawns()
        {
            Pawn green1 = new Pawn("Green Pawn 1", PawnPaths.Green, new Point(8, 1));
            Pawn green2 = new Pawn("Green Pawn 2", PawnPaths.Green, new Point(9, 1));
            Pawn green3 = new Pawn("Green Pawn 3", PawnPaths.Green, new Point(8, 2));
            Pawn green4 = new Pawn("Green Pawn 4", PawnPaths.Green, new Point(9, 2));

            greenPlayer = new PlayerPawns("Grön", PawnColor.Green, green1, green2, green3, green4);
        }

        /// <summary>
        /// Adds the yellow pawns to the yellowPlayer variable
        /// </summary>
        private void AddYellowPawns()
        {
            Pawn yellow1 = new Pawn("Yellow Pawn 1", PawnPaths.Yellow, new Point(8, 8));
            Pawn yellow2 = new Pawn("Yellow Pawn 2", PawnPaths.Yellow, new Point(9, 8));
            Pawn yellow3 = new Pawn("Yellow Pawn 3", PawnPaths.Yellow, new Point(8, 9));
            Pawn yellow4 = new Pawn("Yellow Pawn 4", PawnPaths.Yellow, new Point(9, 9));

            yellowPlayer = new PlayerPawns("Gul", PawnColor.Yellow, yellow1, yellow2, yellow3, yellow4);
        }

        /// <summary>
        /// Adds the blue pawns to the bluePlayer variable
        /// </summary>
        private void AddBluePawns()
        {
            Pawn blue1 = new Pawn("Blue Pawn 1", PawnPaths.Blue, new Point(2, 8));
            Pawn blue2 = new Pawn("Blue Pawn 2", PawnPaths.Blue, new Point(1, 8));
            Pawn blue3 = new Pawn("Blue Pawn 3", PawnPaths.Blue, new Point(2, 9));
            Pawn blue4 = new Pawn("Blue Pawn 4", PawnPaths.Blue, new Point(1, 9));
            bluePlayer = new PlayerPawns("Blå", PawnColor.Blue, blue1, blue2, blue3, blue4);
        }

        /// <summary>
        /// Adds the ren pawns to the renPlayer variable
        /// </summary>
        private void AddRedPawns()
        {
            Pawn red1 = new Pawn("Red Pawn 1", PawnPaths.Red, new Point(2, 1));
            Pawn red2 = new Pawn("Red Pawn 2", PawnPaths.Red, new Point(1, 1));
            Pawn red3 = new Pawn("Red Pawn 3", PawnPaths.Red, new Point(2, 2));
            Pawn red4 = new Pawn("Red Pawn 4", PawnPaths.Red, new Point(1, 2));

            redPlayer = new PlayerPawns("Röd", PawnColor.Red, red1, red2, red3, red4);
        }

        /// <summary>
        /// Draws the board
        /// </summary>
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

            DrawTurnIndicator(currentPlayer);
        }

        /// <summary>
        /// Draws the turn indicator for active player
        /// </summary>
        /// <param name="player">PlayerPawns object</param>
        private void DrawTurnIndicator(PlayerPawns player)
        {

            if (player.IsActive)
            {
                Image img = new Image();
                BitmapImage bitmapImage = new BitmapImage();

                Uri uri = new Uri($"ms-appx:///Assets/Board/PurpleEllipse.png");
                if (player.Color == PawnColor.Green)
                {
                    uri = new Uri($"ms-appx:///Assets/Board/GreenEllipse.png");
                    Canvas.SetTop(img, 429);
                    Canvas.SetLeft(img, 5);
                }

                if (player.Color == PawnColor.Red)
                {
                    uri = new Uri($"ms-appx:///Assets/Board/RedEllipse.png");
                    Canvas.SetTop(img, 8);
                    Canvas.SetLeft(img, 5);
                }

                if (player.Color == PawnColor.Yellow)
                {
                    uri = new Uri($"ms-appx:///Assets/Board/YellowEllipse.png");
                    Canvas.SetTop(img, 428);
                    Canvas.SetLeft(img, 428);
                }

                if (player.Color == PawnColor.Blue)
                {
                    uri = new Uri($"ms-appx:///Assets/Board/BlueEllipse.png");
                    Canvas.SetTop(img, 8);
                    Canvas.SetLeft(img, 428);
                }

                bitmapImage.UriSource = uri;
                img.Source = bitmapImage;

                GridCanvas.Children.Add(img);
            }

            //playerStatusBlock.Text = $"{player.Name} spelares tur";
            //AddStatusTextToTop($"{player.Name} spelares tur", 8);
        }

        /// <summary>
        /// Draws the pawns if they exist
        /// </summary>
        /// <param name="currentDimensions">Size object</param>
        /// <param name="gridLocation">Point object that representats the the gridlocation</param>
        private void DrawIfPawnExists(Size currentDimensions, Point gridLocation)
        {
            foreach (PlayerPawns player in playerPawns)
            {
                if (player.IsMyPawnAt(gridLocation))
                {
                    Pawn pawn = player.GetPawnAt(gridLocation);
                    if (!pawn.InAnimation) DrawPawn(gridLocation, currentDimensions, player);
                }
            }
        }

        /// <summary>
        /// Draws the pawn
        /// </summary>
        /// <param name="gridLocation">Grid location to draw the pawn</param>
        /// <param name="currentDimensions">Dimentions of the image</param>
        /// <param name="player">The player object to which the pawn belongs</param>
        private void DrawPawn(Point gridLocation, Size currentDimensions, PlayerPawns player)
        {
            Image img = new Image();
            BitmapImage bitmapImage = new BitmapImage();

            string pawnColor = player.Color.ToString();
            int pawnCount = player.CountPawnsAt(gridLocation);
            Uri uri = new Uri($"ms-appx:///Assets/Board/Pawns/{pawnColor}-{pawnCount}.png");

            bitmapImage.UriSource = uri;
            img.Source = bitmapImage;

            img.Width = currentDimensions.Width;
            img.Height = currentDimensions.Height;

            img.Name = $"{gridLocation.X}:{gridLocation.Y}";

            img.PointerEntered += HoverOverPawnEnter;
            img.PointerPressed += PressedOnPawn;

            Canvas.SetTop(img, (gridLocation.X * currentDimensions.Width));
            Canvas.SetLeft(img, (gridLocation.Y * currentDimensions.Height));
            GridCanvas.Children.Add(img);
        }


        /// <summary>
        /// Executes when hover over pawn is entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HoverOverPawnExit(object sender, PointerRoutedEventArgs e)
        {
            PlayerPawns player = currentPlayer;
            if (sender is Image image)
            {
                int[] point = image.Name.Split(':').Select(Int32.Parse).ToArray();
                Point pos = new Point(point[0], point[1]);
                Pawn pawn = player.GetPawnAt(pos);
                if (pawn != null)
                {
                    Debug.WriteLine($"{player.Name} Exited pawn {player.IsSelectedPlayer}: {pawn.Name}");
                    GridCanvas.Children.Remove(RectangleToRemove);
                }
            }

            if (sender is Rectangle rectangle)
            {
                GridCanvas.Children.Remove(rectangle);
            }
        }

        /// <summary>
        /// Executes when pressed over pawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PressedOnPawn(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Image image )
            {
                PlayerPawns player = currentPlayer;
                int diceRoll = finalDiceRollResult;

                int[] coordinate = image.Name.Split(':').Select(Int32.Parse).ToArray();
                Pawn pawn = player.GetPawnAt(new Point(coordinate[0], coordinate[1]));
                if (pawn != null)
                {
                    int pawnsInField = player.GetPawnsInPlay().Count();
                    if (pawnsInField > 0)
                    {
                        await MovePawnNumberOfSteps(diceRoll, pawn, player);
                    }

                    Debug.WriteLine("Ja OnPointerPressed(object sender, PointerRoutedEventArgs e) körs ");

                    

                    AddOnePawnButton.IsEnabled = false;
                    AddTwoPawnsButton.IsEnabled = false;
                    AddOnePawnMoveSixStepButton.IsEnabled = false;

                    if (diceRoll != 6)
                    {
                        NextPlayer();
                    }


                }      
            }  
        }

        /// <summary>
        /// Executes when hover over pawn enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HoverOverPawnEnter(object sender, PointerRoutedEventArgs e)
        {
            PlayerPawns player = currentPlayer;
            if (sender is Image image && player.IsSelectedPlayer)
            {
                int[] point = image.Name.Split(':').Select(Int32.Parse).ToArray();
                Pawn pawn = player.GetPawnAt(new Point(point[0], point[1]));
                HighlightDiceRollPosition(player, image, pawn);

            }
        }

        /// <summary>
        /// Shows the highlighted place a pawn can move to
        /// </summary>
        /// <param name="player">Plasyer object that the pawn belongs to</param>
        /// <param name="image">The image that was hovered over</param>
        /// <param name="pawn">The pawn to move</param>
        private void HighlightDiceRollPosition(PlayerPawns player, Image image, Pawn pawn)
        {
            if (pawn != null)
            {
                Debug.WriteLine($"{player.Name}'s pawn {pawn.Name} is entered");
                Rectangle rectangle = new Rectangle();
                //rectangle.Stroke = new SolidColorBrush(Colors.Black);

                Color color = Colors.Black;
                color.A = 55;
                rectangle.Fill = new SolidColorBrush(color);

                rectangle.StrokeThickness = 1;
                rectangle.Width = squareSide;
                rectangle.Height = squareSide;

                Point pos = player.GetNextLocationFromRoll(pawn, finalDiceRollResult);

                if (pawn.Location == pos)
                {
                    rectangle.PointerExited += HoverOverPawnExit;
                    image.PointerExited -= HoverOverPawnExit;
                }
                else
                {
                    image.PointerExited += HoverOverPawnExit;
                }


                Canvas.SetTop(rectangle, (pos.X * squareSide));
                Canvas.SetLeft(rectangle, (pos.Y * squareSide));

                GridCanvas.Children.Add(rectangle);
                RectangleToRemove = rectangle;
            }
        }

        /// <summary>
        /// Runs the AI players pawn to the location according to the dice roll result
        /// </summary>
        /// <param name="diceRoll"></param>
        /// <returns></returns>
        private async Task RunAiPlayerAsync(int diceRoll)
        {
            PlayerPawns player = currentPlayer;

            AddStatusTextToTop($"{player.Name} spelares tur", 8);

            player = DeleteIfNoPawnsLeft(player);

            if (IsAnyPlayersLeft()) return;

            Pawn pawn = player.GetNextPawnInPlay();

            int pawnsInNest = player.GetPawnsInNest().Count();

            Debug.WriteLine($"{player.Name} slog {diceRoll}");


            if (
                (pawnsInNest >= 1 && diceRoll == 1) ||
                (pawnsInNest == 1 && diceRoll == 6)
                )
            {
                pawn = player.GetNextPawnInNest();

                if (diceRoll == 6)
                {
                    Debug.WriteLine($"{player.Name} rullade 6 och får slå igen");
                    for (int i = 0; i < 6; i++)
                    {
                        await GoToNextPosition(pawn, player);
                    }
                    await PushPawns(player, pawn);
                }
                else
                {
                    await GoToNextPosition(pawn, player);
                    await PushPawns(player, pawn);
                    NextPlayer();
                }

                return;
            }
            else if (pawnsInNest >= 2 && diceRoll == 6)
            {
                pawn = player.GetNextPawnInNest();
                await GoToNextPosition(pawn, player);

                pawn = player.GetNextPawnInNest();
                await GoToNextPosition(pawn, player);

                await PushPawns(player, pawn);

                Debug.WriteLine($"{player.Name} rullade 6 och har plockat ut två spelare och får nu slå igen");

                return;
            }
            else if (player.GetPawnsInPlay().Count() == 0)
            {
                Debug.WriteLine($"{player.Name} har ingen spelare på plan");
                NextPlayer();
                Debug.WriteLine($"Nu är det {currentPlayer.Name} tur att spela");
                return;
            }

            // Denna foreach loop kollar om pjäsen kan knuffa ut en annan pjäs
            foreach (Pawn pawnInPlay in player.GetPawnsInPlay())
            {
                Point currentLocation = pawnInPlay.Location;
                for (int i = 0; i < diceRoll; i++)
                {
                    Point nextPosition = pawnInPlay.LookAhead(1);
                    if (nextPosition != new Point())
                    {
                        if (player.IsMyPawnAt(nextPosition))
                        {
                            pawnInPlay.ChangeLocation(currentLocation);
                            break;
                        }
                        pawnInPlay.NextPosition();
                    }
                }

                if (CanPawnPush(player, pawnInPlay))
                {
                    pawn = pawnInPlay;
                    pawnInPlay.ChangeLocation(currentLocation);
                    break;
                }

                pawnInPlay.ChangeLocation(currentLocation);
            }

            // Move the pawn the steps the dice shows and sleep 100 ms.
            await MovePawnNumberOfSteps(diceRoll, pawn, player);

            NextPlayer();
        }

        private PlayerPawns DeleteIfNoPawnsLeft(PlayerPawns player)
        {
            if (player.PawnCount <= 0)
            {
                playerPawns.Remove(player);
                player = currentPlayer;
            }

            return player;
        }

        private bool IsAnyPlayersLeft()
        {
            return playerPawns.Count <= 0;
        }

        /// <summary>
        /// Checks if a pawn can go to the next position
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="player"></param>
        /// <returns>True if there is no pawn with the same color on next position and false if there is</returns>
        private bool CanThisPawnGoToNextPosition(Pawn pawn, PlayerPawns player)
        {
            Point nextPosition = pawn.LookAhead(1);
            if (nextPosition != new Point())
            {
                if (player.IsMyPawnAt(nextPosition))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Selects the next ploayer in turn
        /// </summary>
        private void NextPlayer()
        {
            currentIndex++;

            DrawPlayers();
        }

        /// <summary>
        /// Checks if a pawn can push another pawn
        /// </summary>
        /// <param name="player"></param>
        /// <param name="pawn"></param>
        /// <returns>True if a pawn can push another pawn and false otherwise</returns>
        private bool CanPawnPush(PlayerPawns player, Pawn pawn)
        {
            foreach (PlayerPawns otherPlayer in playerPawns.Where(x => !player.Equals(x)))
            {
                if (otherPlayer.HasPawnOnBoard())
                {
                    foreach (Pawn otherPawn in otherPlayer.GetPawnsInPlay())
                    {
                        if (pawn.CanPawnPush(otherPawn.Location)) return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Pushes a pawn to its nest position
        /// </summary>
        /// <param name="player">Player object that reprecent the pawn</param>
        /// <param name="pawn">Pawn thacan push</param>
        private async Task PushPawns(PlayerPawns player, Pawn pawn)
        {
            foreach (PlayerPawns otherPlayer in playerPawns.Where(x => !player.Equals(x)))
            {
                if (otherPlayer.HasPawnOnBoard())
                {
                    bool isEvilSoundPlayed = false;
                    //Debug.WriteLine("Spelare '" + playerPawn.Name + "' har spelare på plan");
                    foreach (Pawn pawnToPush in otherPlayer.GetPawnsInPlay())
                    {
                        if (pawn.CanPawnPush(pawnToPush.Location))
                        {
                            Debug.WriteLine("Payer: " + otherPlayer.Name + " blev tillbaka knuffad av " + player.Name);
                            if (!isEvilSoundPlayed)
                            {
                                AddStatusTextToTop($"{player.Name} har knuffats tillbaka {otherPlayer.Name}", 5);
                                isEvilSoundPlayed = true;
                                await PlaySoundFile("evil-scream.wav");
                                await Task.Delay(3000);
                            }

                            pawnToPush.ChangeLocation(pawnToPush.NestLocation);
                            DrawPlayers();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Moves the pawn one position
        /// </summary>
        /// <param name="pawn">The pawn to move</param>
        /// <param name="player">The player object the hase the pawn</param>
        /// <param name="backwards">The direction, false for forward and true for backwards movments</param>
        private async Task GoToNextPosition(Pawn pawn, PlayerPawns player, bool backwards = false)
        {
            pawn.InAnimation = true;
            DrawPlayers();

            Image image = new Image();
            BitmapImage bitmapImage = new BitmapImage();

            string pawnColor = player.Color.ToString();
            Uri uri = new Uri($"ms-appx:///Assets/Board/Pawns/{pawnColor}-1.png");

            bitmapImage.UriSource = uri;
            image.Source = bitmapImage;

            image.Width = squareSide;
            image.Height = squareSide;


            Point from = pawn.Location;

            double fromX = from.X * squareSide;
            double fromY = from.Y * squareSide;

            if (!backwards) pawn.NextPosition();
            else pawn.BackPosition();


            Point to = pawn.Location;

            double toX = to.X * squareSide;
            double toY = to.Y * squareSide;

            Debug.WriteLine($"{player.Name} kommer att flyta pawn {pawn.Name} från {from} till {to}");

            GridCanvas.Children.Add(image);

            Canvas.SetTop(image, fromX);
            Canvas.SetLeft(image, fromY);

            GoToNextAnimation(fromX, fromY, toX, toY, image);

            pawn.InAnimation = false;

            await Task.Delay(150);

            DrawPlayers();

            await PlaySoundFile("move.wav");  
        }

        /// <summary>
        /// Plays a sound file
        /// </summary>
        /// <param name="fileName">The file name</param>
        private async Task PlaySoundFile(string fileName)
        {
            var mediaPlayer = new MediaPlayer();

            StorageFile file = await Package.Current.InstalledLocation.GetFileAsync(fileName);
            mediaPlayer.AutoPlay = true;
            mediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
            mediaPlayer.Play();
        }

        /// <summary>
        /// Dice click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DicePic_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Button_Click(sender, e);
        }


        // TODO: När en spelare har slagit så att den får upp val. Så går inte spelet automatiskt vidare när valet är gjort
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await RunGame();
        }

        /// <summary>
        /// Runs the game
        /// </summary>
        private async Task RunGame()
        {
            DicePic.PointerReleased -= DicePic_PointerReleased;
            RollButton.IsEnabled = false;

            if (currentPlayer.IsSelectedPlayer)
            {
                RunManualPlayerAsync(await RollDice());
                await AutoRunAiPlayerAsync();
            }
            else
            {
                await AutoRunAiPlayerAsync();
            }

            //AddStatusTextToTop($"{currentPlayer.Name} spelares tur", 8);

            RollButton.IsEnabled = true;
            DicePic.PointerReleased += DicePic_PointerReleased;
        }

        /// <summary>
        /// Atoruns the AI player
        /// </summary>
        private async Task AutoRunAiPlayerAsync()
        {
            while (!currentPlayer.IsSelectedPlayer)
            {
                await RunAiPlayerAsync(await RollDice());

                if (playerPawns.All(x => x.PawnCount <= 0)) break;
            } 
            
        }

        /// <summary>
        /// Runs the manmual players
        /// </summary>
        /// <param name="diceRoll">The dice roll</param>
        private void RunManualPlayerAsync(int diceRoll)
        {
            PlayerPawns player = currentPlayer;

            AddStatusTextToTop($"{player.Name} spelares tur", 8);

            player = DeleteIfNoPawnsLeft(player);

            if (IsAnyPlayersLeft()) return;


            int pawnsInPlay = player.GetPawnsInPlay().Count();
            if (diceRoll == 1)
            {
                int pawnsInNest = player.GetPawnsInNest().Count();
                if (pawnsInNest > 0)
                {
                    AddOnePawnButton.IsEnabled = true;
                }      
            }
            else if (diceRoll == 6)
            {
                int pawnsInNest = player.GetPawnsInNest().Count();
                if (pawnsInNest == 1)
                {
                    AddOnePawnMoveSixStepButton.IsEnabled = true;
                }
                else if (pawnsInNest > 1)
                {
                    AddTwoPawnsButton.IsEnabled = true;
                    AddOnePawnMoveSixStepButton.IsEnabled = true;
                }

                RollButton.IsEnabled = false;
                DicePic.PointerReleased -= DicePic_PointerReleased;
            } 
            else if (pawnsInPlay <= 0)
            {
                NextPlayer();
            }
            
        }

        /// <summary>
        /// Add one pawn to the board
        /// </summary>
        /// <param name="player">The player object</param>
        private async Task<Pawn> AddOnePawnAsync(PlayerPawns player)
        {
            Pawn pawn = player.GetNextPawnInNest();
            //await GoToNextPosition(pawn, player);
            await MovePawnNumberOfSteps(1, pawn, player);

            return pawn;
        }

        /// <summary>
        /// Rolls the dice
        /// </summary>
        /// <returns>The rolling resault</returns>
        private async Task<int> RollDice()
        {
            random.Next(1, 7);

            DiceRollAnimation.Begin();
            timer.Start();
            await PlaySoundFile("dice-throw.wav");
            await Task.Delay(1000);
            return finalDiceRollResult;
        }

        /// <summary>
        /// Timer function for the dice animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, object e)
        {
            int tempResult = random.Next(1, 7);
            while (tempResult == finalDiceRollResult)
            {
                tempResult = random.Next(1, 7);
            }

            Uri imageUri = new Uri($"ms-appx:///Assets/Board/Dice/D{tempResult}.png");
            DicePic.Source = new BitmapImage(imageUri);

            if (!DiceRollAnimation.GetCurrentState().Equals(ClockState.Active))
            {
                timer.Stop();
                DicePic.Source = new BitmapImage(new Uri($"ms-appx:///Assets/Board/Dice/D{finalDiceRollResult}.png"));
            }
        }

        /// <summary>
        /// Dice animation complete event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiceRollAnimation_Completed(object sender, object e)
        {
            //RollButton.IsEnabled=true;
        }

        /// <summary>
        /// Open ingame navigation event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InGameMenu));
        }

        /// <summary>
        /// Open ingame menu escape button klicked event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                Frame.Navigate(typeof(InGameMenu));
            }
        }

        /// <summary>
        /// Go topp next position animation
        /// </summary>
        /// <param name="fromX">X position to start from</param>
        /// <param name="fromY">Y position to start from</param>
        /// <param name="toX">X position to move to</param>
        /// <param name="toY">Y position to move to</param>
        /// <param name="image">The image to move</param>
        public void GoToNextAnimation(double fromX, double fromY, double toX, double toY, Image image)
        {
            Storyboard moveStoryboard = new Storyboard();
            DoubleAnimation xAnimation = InitAnimation(fromX, toX);

            QuadraticEase ease = new QuadraticEase();
            ease.EasingMode = EasingMode.EaseInOut;
            xAnimation.EasingFunction = ease;

            DoubleAnimation yAnimation = InitAnimation(fromY, toY);

            yAnimation.EasingFunction = ease;

            AddAnimationToBoard(image, moveStoryboard, xAnimation, yAnimation);
        }

        /// <summary>
        /// Adds the animation to move a pawn
        /// </summary>
        /// <param name="image">Pawn image</param>
        /// <param name="moveStoryboard">Storyboard object</param>
        /// <param name="xAnimation">X animation object</param>
        /// <param name="yAnimation">Y animastion object</param>
        private static void AddAnimationToBoard(Image image, Storyboard moveStoryboard, DoubleAnimation xAnimation, DoubleAnimation yAnimation)
        {
            Storyboard.SetTarget(xAnimation, image);
            Storyboard.SetTargetProperty(xAnimation, "(Canvas.Top)");

            Storyboard.SetTarget(yAnimation, image);
            Storyboard.SetTargetProperty(yAnimation, "(Canvas.Left)");

            moveStoryboard.Children.Add(xAnimation);
            moveStoryboard.Children.Add(yAnimation);

            moveStoryboard.Begin();
        }

        /// <summary>
        /// Initializes a double animation
        /// </summary>
        /// <param name="from">From coordinats</param>
        /// <param name="to">To coordinats</param>
        /// <returns></returns>
        private static DoubleAnimation InitAnimation(double from, double to)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = from;
            animation.To = to;
            animation.Duration = TimeSpan.FromMilliseconds(150);
            return animation;
        }

        /// <summary>
        /// Adds status texts
        /// </summary>
        /// <param name="text">The test to add</param>
        /// <param name="secondsBeforDelete">The time the status text is shown</param>
        private void AddStatusTextToTop(string text, int secondsBeforDelete)
        {
            TextBlock textBlockToAdd = new TextBlock
            {
                Text = text,
                Margin = new Thickness(2, 2, 2, 5),
                Padding = new Thickness(3),
                Foreground = new SolidColorBrush(Colors.Black),
                TextWrapping = TextWrapping.Wrap,
            };

            StatusStackPanel.Children.Insert(0, textBlockToAdd);


            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(secondsBeforDelete)
            };

            dispatcherTimer.Tick += (object sender, object e) =>
            {
                StatusStackPanel.Children.Remove(textBlockToAdd);
                dispatcherTimer.Stop();
            };

            dispatcherTimer.Start();
        }

        /// <summary>
        /// Adds a pawn to the board from the nest button click event handeler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddOnePawnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                PlayerPawns player = currentPlayer;
                Pawn pawn = await AddOnePawnAsync(player);

                await PushPawns(player, pawn);

                NextPlayer();
                
                button.IsEnabled = false;

                //await RunGame();

                //RollButton.IsEnabled = true;
                //DicePic.PointerReleased += DicePic_PointerReleased;
            }
        }

        /// <summary>
        /// Adds two pawns to the board from the nest button click event handeler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddTwoPawnsClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                PlayerPawns player = currentPlayer;
                await AddOnePawnAsync(player);
                Pawn pawn = await AddOnePawnAsync(player);

                await PushPawns(player, pawn);

                //NextPlayer();

                button.IsEnabled = false;
                AddOnePawnMoveSixStepButton.IsEnabled = false;

                //RollButton.IsEnabled = true;
                //DicePic.PointerReleased += DicePic_PointerReleased;
            }
        }

        /// <summary>
        /// Adds one pawn to the board from the nest and moves it 6 steps button click event handeler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddOnePawnMoveSixStepClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                PlayerPawns player = currentPlayer;
                Pawn pawn = await AddOnePawnAsync(player);

                await MovePawnNumberOfSteps(5, pawn, player);

                //NextPlayer();

                button.IsEnabled = false;
                AddTwoPawnsButton.IsEnabled = false;

                //RollButton.IsEnabled = true;
                //DicePic.PointerReleased += DicePic_PointerReleased;
            }
        }

        /// <summary>
        /// Move a pawn a certen amounts of steps
        /// </summary>
        /// <param name="steps">The steps to move the pawn</param>
        /// <param name="pawn">The pawn to move</param>
        /// <param name="player">The player object the pawn belongs to</param>
        private async Task MovePawnNumberOfSteps(int steps, Pawn pawn, PlayerPawns player)
        {
            for (int i = 0; i < steps; i++)
            {
                if (!CanThisPawnGoToNextPosition(pawn, player))
                {
                    await GoToNextPosition(pawn, player);
                    break;
                }
                await GoToNextPosition(pawn, player);

                if (pawn.IsAtEnd())
                {
                    if (i < steps - 1)
                    {
                        for (int j = 0; j < steps - (i + 1); j++)
                        {
                            await GoToNextPosition(pawn, player, true);
                        }

                        DrawPlayers();
                        break;
                    }

                    AddStatusTextToTop($"En av {player.Name} spelares pjäser har gott i mål", 10);

                    await PlaySoundFile("tada-fanfare.mp3");
                    await Task.Delay(2000);

                    player.RemovePawn(pawn);
                    DrawPlayers();

                    break;
                }
            }

            await PushPawns(player, pawn);
        }

        /// <summary>
        /// Hover over add two pawns button, enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HoverAddTwoPawnsButtonEnter(object sender, PointerRoutedEventArgs e)
        {
            PlayerPawns player = currentPlayer;
            Pawn pawn = player.GetNextPawnInNest();
            if (pawn != null)
            {
                int oldDiceRoll = finalDiceRollResult;
                finalDiceRollResult = 1;

                HighlightDiceRollPosition(player, new Image(), pawn);

                finalDiceRollResult = oldDiceRoll;
            }
        }

        /// <summary>
        /// Hover over add pawn button, enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HoverAddPawnButtonEnter(object sender, PointerRoutedEventArgs e)
        {
            PlayerPawns player = currentPlayer;
            Pawn pawn = player.GetNextPawnInNest();
            if (pawn != null)
            {
                HighlightDiceRollPosition(player, new Image(), pawn);
            }
        }

        /// <summary>
        /// Hover over add pawn button, leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HoverAddPawnButtonExit(object sender, PointerRoutedEventArgs e)
        {
            PlayerPawns player = currentPlayer;
            Pawn pawn = player.GetNextPawnInNest();
            if (pawn != null) 
            {
                GridCanvas.Children.Remove(RectangleToRemove);
            }
            
        }
    }
}
