using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Arts
{
    class KnightMaster : ArtsRank
    {
        public KnightMaster()
            :base(0,0,18,5,2,3,5)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new KnightRobe();
        }

        public override string GetRankName()
        {
            return "Knight Master";
        }
    }
}
