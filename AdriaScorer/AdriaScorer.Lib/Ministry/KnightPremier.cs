using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Ministry
{
    class KnightPremier : MinistryRank
    {
        public KnightPremier()
            :base(36,10,0,15)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new KnightCivil();
        }

        public override string GetRankName()
        {
            return "Knight Premier";
        }
    }
}
