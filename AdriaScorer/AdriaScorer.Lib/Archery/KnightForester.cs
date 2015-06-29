using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Archery
{
    class KnightForester : ArcheryRank
    {
        public KnightForester()
            :base(0,0,18,5,5,5)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new KnightArcher();
        }

        public override string GetRankName()
        {
            return "Knight Forester";
        }
    }
}
