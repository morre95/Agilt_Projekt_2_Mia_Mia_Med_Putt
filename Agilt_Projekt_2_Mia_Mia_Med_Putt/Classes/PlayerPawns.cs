using System;
using System.Collections.Generic;
using Windows.Foundation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilt_Projekt_2_Mia_Mia_Med_Putt.Classes
{
    internal class PlayerPawns
    {

        public string Name { get; set; }
        private List<Pawn> playerRepository = new List<Pawn>();

        public int PawnCount { get { return playerRepository.Count; } }


        public PlayerPawns(string playerName, params Pawn[] pawns)
        {
            Name = playerName;
            foreach (var pawn in pawns) { playerRepository.Add(pawn); }
        }

        public void AddPawn(Pawn pawn)
        {
            playerRepository.Add(pawn);
        }


        public void RemovePawn(Pawn pawn)
        {
            playerRepository.Remove(pawn);
        }

        public bool IsMyPawnAt(Point location) => playerRepository.Any(p => p.IsAtPosition(location));

        public Pawn GetPawnAt(Point location) => playerRepository.FirstOrDefault(p => p.IsAtPosition(location));

        public Pawn NextPawnInNest() => GetPawnsInNest().FirstOrDefault();

        public Pawn NextPawnInPlay() => GetPawnsInPlay().FirstOrDefault();

        public bool HasPawnOnBoard() => playerRepository.Any(p => p.IsInPlay);

        public IEnumerable<Pawn> GetPawnsInPlay() => playerRepository.Where(p => p.IsInPlay);

        public IEnumerable<Pawn> GetPawnsInNest() => playerRepository.Where(p => !p.IsInPlay);

        public Point GetNextLocationFromRoll(Pawn pawn, int diceRoll)
        {
            // TODO: Räkna ut hur den ska studsa tillbaka vid för högt tärningskast vid slutet
            // exempel:
            /*int lastIndex = pawn.LastSixLocations.FindIndex(p => p.Equals(pawn.Location));
            List<Point> remainingSteps = pawn.LastSixLocations.Skip(lastIndex + 1).ToList();
            if (pawn.IsWithinSixStepsFromEnd() && remainingSteps.Count > diceRoll) 
            {
                int adjustedIndex = diceRoll % remainingSteps.Count - 1;
                return remainingSteps[adjustedIndex];
            }*/

            int pathIndex = pawn.PawnPath.FindIndex(p => p.Equals(pawn.Location));
            return pawn.PawnPath[pathIndex + diceRoll];
        }

    }
}
