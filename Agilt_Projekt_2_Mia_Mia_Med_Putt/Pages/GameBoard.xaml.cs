using Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes;
using Newtonsoft.Json;
using System;
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
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
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

            //_ = CheckSaveGame();

        }

        public GameBoard(PawnColor color)
        {
            SetUpPlayers();

            foreach (PlayerPawns player in playerPawns)
            {
                if (player.Color == color)
                {
                    player.IsSelectedPlayer = true;
                }
            }

            DrawPlayers();
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

            //green1.Location = new Point(6, 5);
            greenPlayer = new PlayerPawns("Green Player", PawnColor.Green, green1, green2, green3, green4);
        }

        private void AddYellowPawns()
        {
            Pawn yellow1 = new Pawn("Yellow Pawn 1", PawnPaths.Yellow, new Point(8, 8));
            Pawn yellow2 = new Pawn("Yellow Pawn 2", PawnPaths.Yellow, new Point(9, 8));
            Pawn yellow3 = new Pawn("Yellow Pawn 3", PawnPaths.Yellow, new Point(8, 9));
            Pawn yellow4 = new Pawn("Yellow Pawn 4", PawnPaths.Yellow, new Point(9, 9));

            //yellow1.Location = new Point(5,6);
            yellowPlayer = new PlayerPawns("Yellow Player", PawnColor.Yellow, yellow1, yellow2, yellow3, yellow4);
        }

        private void AddBluePawns()
        {
            Pawn blue1 = new Pawn("Blue Pawn 1", PawnPaths.Blue, new Point(2, 8));
            Pawn blue2 = new Pawn("Blue Pawn 2", PawnPaths.Blue, new Point(1, 8));
            Pawn blue3 = new Pawn("Blue Pawn 3", PawnPaths.Blue, new Point(2, 9));
            Pawn blue4 = new Pawn("Blue Pawn 4", PawnPaths.Blue, new Point(1, 9));

            //blue1.Location = new Point(4, 5);
            bluePlayer = new PlayerPawns("Blue Player", PawnColor.Blue, blue1, blue2, blue3, blue4);
        }

        private void AddRedPawns()
        {
            Pawn red1 = new Pawn("Red Pawn 1", PawnPaths.Red, new Point(2, 1));
            Pawn red2 = new Pawn("Red Pawn 2", PawnPaths.Red, new Point(1, 1));
            Pawn red3 = new Pawn("Red Pawn 3", PawnPaths.Red, new Point(2, 2));
            Pawn red4 = new Pawn("Red Pawn 4", PawnPaths.Red, new Point(1, 2));

            //red1.Location = new Point(5,4);
            redPlayer = new PlayerPawns("Red Player", PawnColor.Red, red1, red2, red3, red4);
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

            foreach (PlayerPawns player in playerPawns)
            {
                if (player.IsSelectedPlayer)
                {
                    Debug.WriteLine("Japp jag är bäst");
                    Image img = new Image();
                    BitmapImage bitmapImage = new BitmapImage();

                    Uri uri = new Uri($"ms-appx:///Assets/Board/ArrowRight.png");
                    if (player.Color == PawnColor.Green)
                    {
                        Canvas.SetTop(img, (9 * squareSide));
                        Canvas.SetLeft(img, (0 * squareSide));
                    }

                    if (player.Color == PawnColor.Red)
                    {
                        Canvas.SetTop(img, (2 * squareSide));
                        Canvas.SetLeft(img, (0 * squareSide));
                    }

                    if (player.Color == PawnColor.Yellow)
                    {
                        uri = new Uri($"ms-appx:///Assets/Board/ArrowLeft.png");
                        Canvas.SetTop(img, (9 * squareSide));
                        Canvas.SetLeft(img, (10.5 * squareSide));
                    }
                    if (player.Color == PawnColor.Blue)
                    {
                        uri = new Uri($"ms-appx:///Assets/Board/ArrowLeft.png");
                        Canvas.SetTop(img, (2 * squareSide));
                        Canvas.SetLeft(img, (10.5 * squareSide));
                    }

                    bitmapImage.UriSource = uri;
                    img.Source = bitmapImage;

                    img.Width = 32;
                    img.Height = 15;

                    GridCanvas.Children.Add(img);
                }
            }
        }

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

            Canvas.SetTop(img, (gridLocation.X * currentDimensions.Width));
            Canvas.SetLeft(img, (gridLocation.Y * currentDimensions.Height));
            GridCanvas.Children.Add(img);
        }

        private async Task RunGameAsync()
        {
            PlayerPawns player = currentPlayer;
            Pawn pawn = player.NextPawnInPlay();

            int diceRoll = RollDice();
            await PlaySoundFile("dice-throw.wav");

            // TBD: Ganska jobbigt att behöva vänta 1 sek för detta
            await Task.Delay(1000);

            int pawnsInNest = player.GetPawnsInNest().Count();

            Debug.WriteLine($"{player.Name} slog {diceRoll}");

            if (
                // If the dice shows 1, bring one pawn to the gameboard if there is one.
                (pawnsInNest >= 1 && diceRoll == 1) ||
                // If the dice shows 6 and there is one pawn in the nest, bring one pawn to the gameboard.
                (pawnsInNest == 1 && diceRoll == 6)
                )
            {
                pawn = player.NextPawnInNest();
                await GoToNextPosition(pawn, player);

                await PushPawns(player, pawn);

                // If dice is = 1, go to next player
                if (diceRoll == 6) Debug.WriteLine($"{player.Name} rullade 6 och får slå igen");
                else currentIndex++;

                return;
            }
            // If the dice shows 6, bring two pawns to the gameboard if there are two or more pawns in the nest.
            else if (pawnsInNest >= 2 && diceRoll == 6)
            {
                pawn = player.NextPawnInNest();
                await GoToNextPosition(pawn, player);

                pawn = player.NextPawnInNest();
                await GoToNextPosition(pawn, player);

                await PushPawns(player, pawn);

                Debug.WriteLine($"{player.Name} rullade 6 och har plockat ut två spelare och får nu slå igen");
                //currentIndex++;

                return;
            }
            else if (player.GetPawnsInPlay().Count() == 0)
            {
                Debug.WriteLine($"{player.Name} har ingen spelare på plan");
                currentIndex++;
                Debug.WriteLine($"Nu är det {currentPlayer.Name} tur att spela");
                return;
            }

            // Move the pawn the steps the dice shows and sleep 100 ms.
            for (int i = 0; i < diceRoll; i++)
            {
                Point nextPosition = pawn.LookAhead(1);
                if (nextPosition != new Point())
                {
                    //Pawn nextPawn = player.GetPawnAt(nextPosition);
                    if (player.IsMyPawnAt(nextPosition))
                    {
                        Debug.WriteLine($"{player.Name} har {player.CountPawnsAt(nextPosition)} pjäser på denna plats och får inte gå om sin egen pjäs");
                        await GoToNextPosition(pawn, player);
                        break;
                    }
                }

                await GoToNextPosition(pawn, player);

                await Task.Delay(100);

                if (pawn.IsAtEnd())
                {
                    if (i < diceRoll - 1)
                    {
                        Debug.WriteLine($"{player.Name} slog {diceRoll} och ska studsa tillbaka {diceRoll - (i + 1)}");

                        //pawn.ChangeLocation(pawn.PawnPath[(pawn.PawnPath.Count - 1) - (diceRoll - (i + 1))]);

                        for (int j = 0; j < diceRoll - (i + 1); j++)
                        {
                            await GoToNextPosition(pawn, player, true);
                        }

                        DrawPlayers();
                        break;
                    }

                    Debug.WriteLine($"{player.Name} raderar pjäsen {pawn.Name}");

                    player.RemovePawn(pawn);
                    DrawPlayers();

                    break;
                }
            }

            // Kolla om det finns någon spelare som kan knuffa
            await PushPawns(player, pawn);

            currentIndex++;
        }

        private async Task PushPawns(PlayerPawns player, Pawn pawn)
        {
            foreach (PlayerPawns playerPawn in playerPawns.Where(x => !player.Equals(x)))
            {
                if (playerPawn.HasPawnOnBoard())
                {
                    bool isEvilSoundPlayed = false;
                    //Debug.WriteLine("Spelare '" + playerPawn.Name + "' har spelare på plan");
                    foreach (Pawn pawnToPush in playerPawn.GetPawnsInPlay())
                    {
                        if (pawn.CanPawnPush(pawnToPush.Location))
                        {
                            Debug.WriteLine("Payer: " + playerPawn.Name + " blev tillbaka knuffad av " + player.Name);
                            if (!isEvilSoundPlayed)
                            {
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

            if (!backwards)  pawn.NextPosition();
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

            await Task.Delay(100);

            DrawPlayers();

            await PlaySoundFile("move.wav");

            
        }

        private async Task PlaySoundFile(string fileName)
        {
            var mediaPlayer = new MediaPlayer();

            StorageFile file = await Package.Current.InstalledLocation.GetFileAsync(fileName);
            mediaPlayer.AutoPlay = true;
            mediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
            mediaPlayer.Play();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await RunGameAsync();
        }

        private int RollDice()
        {
            Random random = new Random();
            return random.Next(1,7);
        }

        private async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            //await FileHelper.SaveGameAsync(playerPawns, currentIndex);
            Frame.Navigate(typeof(InGameMenu));
        }

        private void Page_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                Frame.Navigate(typeof(InGameMenu));
            }
        }

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

        private static DoubleAnimation InitAnimation(double from, double to)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = from;
            animation.To = to;
            animation.Duration = TimeSpan.FromMilliseconds(100);
            return animation;
        }


        /*private async Task CheckSaveGame()
        {
            //await FileHelper.DeleteSavedGameAsync();
            Debug.WriteLine("Kör jag ens det här");
            Debug.WriteLine(await FileHelper.SaveGameExistsAsync());

            if (await FileHelper.SaveGameExistsAsync())
            {
                Debug.WriteLine("Det här körs");
                SavedGame game = await FileHelper.GetSavedGameAsync();
                
                currentIndex = game.currentIndex;
                playerPawns = game.playerPawns;
                Debug.WriteLine(game.currentIndex);
                Debug.WriteLine(game.playerPawns[game.currentIndex] == currentPlayer);

                Debug.WriteLine("Men inte det här va????");
                await FileHelper.DeleteSavedGameAsync();
                
                DrawPlayers();
            }
            else
            {
                Debug.WriteLine("Japp det blev en else");
                SetUpPlayers();
                DrawPlayers();
            }

            //DrawPlayers();
        }*/
    }

    /*public class FileHelper
    {
        private static string fileName = "savedGame.json";

        public static async Task SaveGameAsync(List<PlayerPawns> playerPawns, int currentIndex)
        {
            SavedGame game = new SavedGame
            {
                playerPawns = playerPawns,
                currentIndex = currentIndex
            };

            var jsonData = JsonConvert.SerializeObject(game);
            Debug.WriteLine(jsonData);

            await SaveFileAsync(fileName, game);
        }

        public static async Task<SavedGame> GetSavedGameAsync()
        {
            return await ReadFileAsync<SavedGame>(fileName);
        }

        public static async Task<bool> SaveGameExistsAsync()
        {
            return await FileExistsAsync(fileName);
        }

        public static async Task DeleteSavedGameAsync()
        {
            await DeleteFileAsync(fileName);
        }

        public static async Task SaveFileAsync(string fileName, object data)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var file = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            var jsonData = JsonConvert.SerializeObject(data);
            await FileIO.WriteTextAsync(file, jsonData);
        }

        public static async Task<T> ReadFileAsync<T>(string fileName)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var file = await localFolder.GetFileAsync(fileName);
            var jsonData = await FileIO.ReadTextAsync(file);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public static async Task<bool> FileExistsAsync(string fileName)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var item = await localFolder.TryGetItemAsync(fileName);
            return item != null;
        }

        public static async Task DeleteFileAsync(string fileName)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var file = await localFolder.GetFileAsync(fileName);
            if (file != null)
            {
                await file.DeleteAsync();
            }
        }
    }

    public class SavedGame
    {
        public List<PlayerPawns> playerPawns = new List<PlayerPawns>();

        public int currentIndex = 0;

    }*/

}
