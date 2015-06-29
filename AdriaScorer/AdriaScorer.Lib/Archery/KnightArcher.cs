using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Archery
{
    class KnightArcher : ArcheryRank
    {
        public KnightArcher()
            :base(0,0,10,1,1,3)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new Marksman();
        }

        public override string GetRankName()
        {
            return "Knight Archer";
        }
    }
}
