using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Arts
{
    class Journeyman : ArtsRank
    {
        public Journeyman()
            :base(3,0,0,0,0,0,0)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new Apprentice();
        }

        public override string GetRankName()
        {
            return "Journeyman";
        }
    }
}
