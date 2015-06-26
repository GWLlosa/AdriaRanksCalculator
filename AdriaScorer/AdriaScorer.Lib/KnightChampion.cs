using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class KnightChampion : Rank
    {
        public KnightChampion()
            :base(0,0,36,10,16,6,10,15)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new KnightBanneret();
        }

        public override string GetRankName()
        {
            return "Knight Champion";
        }

    }
}
