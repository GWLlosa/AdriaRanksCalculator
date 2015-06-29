using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Ministry
{
    class KnightCivil : MinistryRank
    {
        public KnightCivil()
            :base(18,1,0,3)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new KnightMinister();
        }

        public override string GetRankName()
        {
            return "Knight Civil";
        }
    }
}
