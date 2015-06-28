using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Arts
{
    class KnightRobe : ArtsRank
    {
        public KnightRobe()
            :base(0,0,10,1,1,1,3)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new Master();
        }

        public override string GetRankName()
        {
            return "Knight Robe";
        }
    }
}
