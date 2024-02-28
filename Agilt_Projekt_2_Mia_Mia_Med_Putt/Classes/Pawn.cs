using System;
using System.Collections.Generic;
using Windows.Foundation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes
{
    internal class Pawn
    {

        public Point Location { get; private set; }

        public bool IsInPlay { get { return Location != NestLocation; } }

        public Point EndLocation { get; private set; }
        public Point NestLocation { get; private set; }

        public List<Point> LastSixLocations { get; private set; }
        public string Name { get; set; }
        public List<Point> PawnPath { get; private set; }

        public Pawn(string name, List<Point> pawnPath, Point nestLocation)
        {
            Location = nestLocation;
            NestLocation = nestLocation;
            EndLocation = pawnPath.Last();
            LastSixLocations = pawnPath.TakeLast(6).ToList();
            Name = name;
            PawnPath = pawnPath;
        }

        public void NextPosition()
        {
            int i = PawnPath.FindIndex(p => p.Equals(Location));
            Location = PawnPath[i + 1];
        }

        public void ChangeLocation(Point newLocation)
        {
            Location = newLocation;
        }

        public bool IsAtEnd() => Location.Equals(EndLocation);

        public bool IsAtPosition(Point location) => Location.Equals(location);

        public bool CanPawnPush(Point opponent) => Location.Equals(opponent);

        public bool IsWithinSixStepsFromEnd() => LastSixLocations.Any(l => Location.Equals(l));
    }
}
