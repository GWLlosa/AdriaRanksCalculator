using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdriaScorer.Lib.Arts
{
    class KnightDoctor : ArtsRank
    {
        public KnightDoctor()
            :base(0,0,36,10,4,5,15)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new KnightMaster();
        }

        public override string GetRankName()
        {
            return "Knight Doctor";
        }

       
    }
}
