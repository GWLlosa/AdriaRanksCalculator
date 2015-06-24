using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class Sergeant : Rank
    {
        public override Rank GetPreviousRank()
        {
            return new Guardsman();
        }

        public override string GetRankName()
        {
            return "Sergeant";
        }

        protected override int SergeantsParticipationsRequired
        {
            get { return 5; }
        }

        protected override int SergeantsListWinsRequired
        {
            get { return 1; }
        }

        protected override int DemonstrationParticipationsRequired
        {
            get { return 2; }
        }

        protected override int KnightsListParticipationsRequired
        {
            get { return 0; }
        }

        protected override int KnightsListWinsRequired
        {
            get { return 0; }
        }

        protected override int WarParticipationsRequired
        {
            get { return 0; }
        }

        protected override int KnightsListArmoredParticipations
        {
            get { return 0; }
        }

        protected override int KnightsListArmoredWins
        {
            get { return 0; }
        }
    }
}
