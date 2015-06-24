using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class CombatParticipationRecord
    {
        public int SergeantsListParticipations
        { get; set; }
        public int SergeantsListWins
        { get; set; }
        public int DemonstrationParticipations
        { get; set; }
        public int KightsListParticipations
        { get; set; }
        public int KnightsListWins
        { get; set; }
        public int WarParticipations
        { get; set; }


        public int KnightsListArmoredWins { get; set; }

        public int KnightsListArmoredParticipations { get; set; }
    }
}
