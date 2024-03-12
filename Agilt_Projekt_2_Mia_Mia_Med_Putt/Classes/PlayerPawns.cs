using System;
using System.Collections.Generic;
using Windows.Foundation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes
{
    public enum PawnColor  //lagt till
    {
        Red,
        Blue,
        Yellow,
        Green
    }

    /// <summary>
    /// Manages a collection of pawns that belong to a single player.
    /// </summary>
    /// <example>
    /// This class is used by the MainPage class to keep track of the Pawns that belong to each player. 
    /// The MainPage class uses the IsMyPawnAt method to determine whether a given Point represents a 
    /// Pawn that belongs to the current player, and the GetPawnAt method can be used to retrieve a 
    /// specific Pawn when a user clicks on a square on the game board.
    /// </example>
    public class PlayerPawns
    {
        /// <summary>
        /// The name of the player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A list of pawns that belong to the player.
        /// </summary>
        private List<Pawn> playerRepository = new List<Pawn>();

        /// <summary>
        /// The count of the number of pawns for a specific player
        /// </summary>
        public int PawnCount { get { return playerRepository.Count; } }


        /// <summary>
        public bool IsSelectedPlayer { get; set; }
        /// </summary>

        public PawnColor Color { get; set; }

        ///
        public bool IsActive { get; set; } = true; // Default to true
        ///

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerPawns"/> class.
        /// </summary>
        /// <param name="playerName">The name of the player.</param>
        /// <param name="pawns">The pawns that belong to the player.</param>
        public PlayerPawns(string playerName, PawnColor color, params Pawn[] pawns)
        {
            ///IsSelectedPlayer = false;
            IsActive = false;
            Color = color;
            Name = playerName;
            foreach (var pawn in pawns) { playerRepository.Add(pawn); }
        }

        
        /// <summary>
        /// Adds a pawn to the collection.
        /// </summary>
        /// <param name="pawn">The pawn to add.</param>
        public void AddPawn(Pawn pawn)
        {
            playerRepository.Add(pawn);
        }

        /// <summary>
        /// Removes a pawn from the collection.
        /// </summary>
        /// <param name="pawn">The pawn to remove.</param>
        public void RemovePawn(Pawn pawn)
        {
            playerRepository.Remove(pawn);
        }

        /// <summary>
        /// Determines whether the player has a pawn at the specified location.
        /// </summary>
        /// <param name="location">The location to check.</param>
        /// <returns><c>true</c> if the player has a pawn at the specified location; otherwise, <c>false</c>.</returns>
        public bool IsMyPawnAt(Point location) => playerRepository.Any(p => p.IsAtPosition(location));

        public int CountPawnsAt(Point location) => playerRepository.Count(p => p.IsAtPosition(location));

        /// <summary>
        /// Gets the pawn at the specified location.
        /// </summary>
        /// <param name="location">The location to check.</param>
        /// <returns>The pawn at the specified location, if any; otherwise, <c>null</c>.</returns>
        public Pawn GetPawnAt(Point location) => playerRepository.FirstOrDefault(p => p.IsAtPosition(location));

        /// <summary>
        /// Gets the next pawn in the collection of the nest.
        /// </summary>
        /// <returns>The next pawn in the collection of the nest, if any; otherwise, <c>null</c>.</returns>
        public Pawn GetNextPawnInNest() => GetPawnsInNest().FirstOrDefault();

        /// <summary>
        /// Gets the next pawn in the collection of the ones in play.
        /// </summary>
        /// <returns>The next pawn in the collection of the ones in play, if any; otherwise, <c>null</c>.</returns>
        public Pawn GetNextPawnInPlay() => GetPawnsInPlay().FirstOrDefault();

        /// <summary>
        /// Determines whether the player has any pawn on the board.
        /// </summary>
        /// <returns><c>true</c> if the player has a pawn on the board; otherwise, <c>false</c>.</returns>
        public bool HasPawnOnBoard() => playerRepository.Any(p => p.IsInPlay);

        /// <summary>
        /// Get a collection of Pawn that is in play on the board.
        /// </summary>
        /// <returns>A collection of Pawn that is in play.</returns>
        public IEnumerable<Pawn> GetPawnsInPlay() => playerRepository.Where(p => p.IsInPlay);

        /// <summary>
        /// Gets a collection of Pawn that are not in play on the board.
        /// </summary>
        /// <returns>Gets a collection of Pawn that are not in play.</returns>
        public IEnumerable<Pawn> GetPawnsInNest() => playerRepository.Where(p => !p.IsInPlay);

        /// <summary>
        /// Gets the location of the pawn should be moved to for a specific dice roll
        /// </summary>
        /// <param name="pawn">The pawn</param>
        /// <param name="diceRoll">The dice roll</param>
        /// <returns>The location</returns>
        public Point GetNextLocationFromRoll(Pawn pawn, int diceRoll)
        {
            int pathIndex = pawn.PawnPath.FindIndex(p => p.Equals(pawn.Location));
            if (pathIndex + diceRoll >= pawn.PawnPath.Count)
            {
                return pawn.PawnPath[pawn.PawnPath.Count - Math.Abs(pawn.PawnPath.Count - pathIndex - diceRoll) - 2];
            }

            return pawn.PawnPath[pathIndex + diceRoll];
        }

        

    }
}
