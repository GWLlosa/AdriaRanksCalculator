using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Ministry
{
    class KnightMinister : MinistryRank
    {
        public KnightMinister()
            :base(10,1,0,3)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new Chamberlain();
        }

        public override string GetRankName()
        {
            return "Knight Minister";
        }
    }
}
