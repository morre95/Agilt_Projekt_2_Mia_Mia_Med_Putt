using System.Collections.Generic;
using Windows.Foundation;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes
{
    internal static class PawnPaths
    {
        /// <summary>
        /// The path of points that the Red pawn follows.
        /// </summary>
        public readonly static List<Point> Red = new List<Point>()
        {
            new Point(4,0),  new Point(4,1), new Point(4,2), new Point(4,3),  new Point(4,4),
            new Point(3,4),  new Point(2,4), new Point(1,4), new Point(0,4),  new Point(0,5),
            new Point(0,6),  new Point(1,6), new Point(2,6), new Point(3,6),  new Point(4,6),
            new Point(4,7),  new Point(4,8), new Point(4,9), new Point(4,10), new Point(5,10),
            new Point(6,10), new Point(6,9), new Point(6,8), new Point(6,7),  new Point(6,6),
            new Point(7,6),  new Point(8,6), new Point(9,6), new Point(10,6), new Point(10,5),
            new Point(10,4), new Point(9,4), new Point(8,4), new Point(7,4),  new Point(6,4),
            new Point(6,3),  new Point(6,2), new Point(6,1), new Point(6,0),  new Point(5,0), 
            // Mållinjen
            new Point(5,1),  new Point(5,2), new Point(5,3), new Point(5,4),  new Point(5,5)

        };

        /// <summary>
        /// The path of points that the Blue pawn follows.
        /// </summary>
        public readonly static List<Point> Blue = new List<Point>()
        {
            new Point(0,6),  new Point(1,6), new Point(2,6), new Point(3,6),  new Point(4,6),
            new Point(4,7),  new Point(4,8), new Point(4,9), new Point(4,10), new Point(5,10),
            new Point(6,10), new Point(6,9), new Point(6,8), new Point(6,7),  new Point(6,6),
            new Point(7,6),  new Point(8,6), new Point(9,6), new Point(10,6), new Point(10,5),
            new Point(10,4), new Point(9,4), new Point(8,4), new Point(7,4),  new Point(6,4),
            new Point(6,3),  new Point(6,2), new Point(6,1), new Point(6,0),  new Point(5,0),
            new Point(4,0),  new Point(4,1), new Point(4,2), new Point(4,3),  new Point(4,4),
            new Point(3,4),  new Point(2,4), new Point(1,4), new Point(0,4),  new Point(0,5), 
            // Målet är nära nu
            new Point(1,5),  new Point(2,5), new Point(3,5), new Point(4,5),  new Point(5,5)
        };

        /// <summary>
        /// The path of points that the Yellow pawn follows.
        /// </summary>
        public readonly static List<Point> Yellow = new List<Point>()
        {
            new Point(6,10), new Point(6,9), new Point(6,8), new Point(6,7),  new Point(6,6),
            new Point(7,6),  new Point(8,6), new Point(9,6), new Point(10,6), new Point(10,5),
            new Point(10,4), new Point(9,4), new Point(8,4), new Point(7,4),  new Point(6,4),
            new Point(6,3),  new Point(6,2), new Point(6,1), new Point(6,0),  new Point(5,0),
            new Point(4,0),  new Point(4,1), new Point(4,2), new Point(4,3),  new Point(4,4),
            new Point(3,4),  new Point(2,4), new Point(1,4), new Point(0,4),  new Point(0,5),
            new Point(0,6),  new Point(1,6), new Point(2,6), new Point(3,6),  new Point(4,6),
            new Point(4,7),  new Point(4,8), new Point(4,9), new Point(4,10), new Point(5,10),
            // Millinjen
            new Point(5,9),  new Point(5,8), new Point(5,7), new Point(5,6), new Point(5,5),
        };

        /// <summary>
        /// The path of points that the Green pawn follows.
        /// </summary>
        public readonly static List<Point> Green = new List<Point>()
        {
            new Point(10,4), new Point(9,4), new Point(8,4), new Point(7,4),  new Point(6,4),
            new Point(6,3),  new Point(6,2), new Point(6,1), new Point(6,0),  new Point(5,0),
            new Point(4,0),  new Point(4,1), new Point(4,2), new Point(4,3),  new Point(4,4),
            new Point(3,4),  new Point(2,4), new Point(1,4), new Point(0,4),  new Point(0,5),
            new Point(0,6),  new Point(1,6), new Point(2,6), new Point(3,6),  new Point(4,6),
            new Point(4,7),  new Point(4,8), new Point(4,9), new Point(4,10), new Point(5,10),
            new Point(6,10), new Point(6,9), new Point(6,8), new Point(6,7),  new Point(6,6),
            new Point(7,6),  new Point(8,6), new Point(9,6), new Point(10,6), new Point(10,5),
            // Sista striden
            new Point(9,5),  new Point(8,5), new Point(7,5), new Point(6,5), new Point(5,5),
        };

    }
}
