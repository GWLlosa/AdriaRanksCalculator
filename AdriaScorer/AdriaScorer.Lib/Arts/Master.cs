using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Arts
{
    class Master : ArtsRank
    {
        public Master()
            :base(5,1,0,0,0,0,2)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new Journeyman();
        }

        public override string GetRankName()
        {
            return "Master";
        }
    }
}
