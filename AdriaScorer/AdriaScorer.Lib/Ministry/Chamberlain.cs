using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Ministry
{
    class Chamberlain : MinistryRank
    {
        public Chamberlain()
            :base(5,0,1,1)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new Rector();
        }

        public override string GetRankName()
        {
            return "Chamberlain";
        }
    }
}
