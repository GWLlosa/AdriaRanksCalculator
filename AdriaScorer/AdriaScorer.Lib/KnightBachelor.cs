using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class KnightBachelor : Rank
    {

        public override Rank GetPreviousRank()
        {
            return new Sergeant();
        }

        public override string GetRankName()
        {
            return "Knight Bachelor";
        }

        protected override int SergeantsParticipationsRequired
        {
            get { return 0; }
        }

        protected override int SergeantsListWinsRequired
        {
            get { return 0; }
        }

        protected override int DemonstrationParticipationsRequired
        {
            get { return 3; }
        }

        protected override int KnightsListParticipationsRequired
        {
            get { return 10; }
        }

        protected override int KnightsListWinsRequired
        {
            get { return 1; }
        }

        protected override int WarParticipationsRequired
        {
            get { return 1; }
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
