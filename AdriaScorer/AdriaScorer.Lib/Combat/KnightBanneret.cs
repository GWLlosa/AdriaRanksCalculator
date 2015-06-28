using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Combat
{
    public class KnightBanneret : CombatRank
    {
        public KnightBanneret()
            :base(0,0,18,5,8,3,5,5)
        {

        }

        public override Rank GetPreviousRank()
        {
            return new KnightBachelor();
        }

        public override string GetRankName()
        {
            return "Knight Banneret";
        }

    }
}
