using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Archery
{
    class Bowman : ArcheryRank
    {
        public Bowman()
            :base(3,0,0,0,0,0)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new YeomanArcher();
        }

        public override string GetRankName()
        {
            return "Bowman";
        }
    }
}
