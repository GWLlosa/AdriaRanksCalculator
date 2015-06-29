using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Archery
{
    class KnightWarden : ArcheryRank
    {
        public KnightWarden()
            :base(0,0,36,10,10,15)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new KnightForester();
        }

        public override string GetRankName()
        {
            return "Knight Warden";
        }
    }
}
