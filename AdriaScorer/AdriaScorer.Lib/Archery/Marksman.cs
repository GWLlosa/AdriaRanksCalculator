using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Archery
{
    class Marksman : ArcheryRank
    {
        public Marksman()
            :base(5,1,0,0,0,2)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new Bowman();
        }

        public override string GetRankName()
        {
            return "Marksman";
        }
    }
}
