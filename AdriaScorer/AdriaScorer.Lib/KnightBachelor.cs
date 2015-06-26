using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class KnightBachelor : Rank
    {
        public KnightBachelor()
            :base(0,0,10,1,0,0,1,3)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new Sergeant();
        }

        public override string GetRankName()
        {
            return "Knight Bachelor";
        }

    }
}
