using System;
using System.Collections.Generic;
using Windows.Foundation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes
{

    /// <summary>
    /// Represents a pawn on the game board.
    /// <seealso cref="PlayerPawns"/>
    /// </summary>
    internal class Pawn
    {

        /// <summary>
        /// The current location of the pawn.
        /// </summary>
        public Point Location { get; private set; }

        /// <summary>
        /// True if pawn is on the board, false otherwise.
        /// </summary>
        public bool IsInPlay { get { return Location != NestLocation; } }

        /// <summary>
        /// The final location of the pawn.
        /// </summary>
        public Point EndLocation { get; private set; }

        /// <summary>
        /// The location where the pawn was initially placed on the board.
        /// </summary>
        public Point NestLocation { get; private set; }

        /// <summary>
        /// A list of the previous six locations of the pawn has to the it's goal.
        /// </summary>
        public List<Point> LastSixLocations { get; private set; }

        /// <summary>
        /// The name of the pawn.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The path of points that the pawn follows.
        /// </summary>
        public List<Point> PawnPath { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pawn"/> class.
        /// </summary>
        /// <param name="name">The name of the pawn.</param>
        /// <param name="pawnPath">The path of points that the pawn follows.</param>
        /// <param name="nestLocation">The location where the pawn was initially placed.</param>
        public Pawn(string name, List<Point> pawnPath, Point nestLocation)
        {
            Location = nestLocation;
            NestLocation = nestLocation;
            EndLocation = pawnPath.Last();
            LastSixLocations = pawnPath.TakeLast(6).ToList();
            Name = name;
            PawnPath = pawnPath;
        }

        /// <summary>
        /// Moves the pawn to the next point in its path.
        /// </summary>
        public void NextPosition()
        {
            int i = PawnPath.FindIndex(p => p.Equals(Location));
            Location = PawnPath[i + 1];
        }

        /// <summary>
        /// Changes the location of the pawn.
        /// </summary>
        /// <param name="newLocation">The new location of the pawn.</param>
        public void ChangeLocation(Point newLocation)
        {
            Location = newLocation;
        }

        /// <summary>
        /// Returns a bool value indicating whether the pawn is at its final location.
        /// </summary>
        /// <returns><c>true</c> if the pawn is at its final location; otherwise, <c>false</c>.</returns>
        public bool IsAtEnd() => Location.Equals(EndLocation);

        /// <summary>
        /// Returns a bool value indicating whether the pawn is at the specified location.
        /// </summary>
        /// <param name="location">The location to check.</param>
        /// <returns><c>true</c> if the pawn is at the specified location; otherwise, <c>false</c>.</returns>
        public bool IsAtPosition(Point location) => Location.Equals(location);

        /// <summary>
        /// Returns a bool value indicating whether the pawn can push the specified opponent to its nest.
        /// </summary>
        /// <param name="opponent">The opponent to check for collision with.</param>
        /// <returns><c>true</c> if the pawn collides with the specified opponent; otherwise, <c>false</c>.</returns>
        public bool CanPawnPush(Point opponent) => Location.Equals(opponent);

        /// <summary>
        /// Returns a value indicating whether the pawn is within six steps of its final location.
        /// </summary>
        /// <returns><c>true</c> if the pawn is within six steps of its final location; otherwise, <c>false</c>.</returns>
        public bool IsWithinSixStepsFromEnd() => LastSixLocations.Any(l => Location.Equals(l));
    }
}
