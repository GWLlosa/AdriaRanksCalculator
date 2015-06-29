using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Ministry
{
    class Clarke : MinistryRank
    {
        public Clarke()
            :base(0,0,0,0)
        {

        }
        public override Rank GetPreviousRank()
        {
            return null;
        }

        public override string GetRankName()
        {
            return "Clarke";
        }
    }
}
