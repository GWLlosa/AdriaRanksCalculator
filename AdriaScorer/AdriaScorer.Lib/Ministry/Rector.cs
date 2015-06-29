using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Ministry
{
    class Rector : MinistryRank
    {
        public Rector()
            :base(3,0,1,0)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new Clarke();
        }

        public override string GetRankName()
        {
            return "Rector";
        }
    }
}
