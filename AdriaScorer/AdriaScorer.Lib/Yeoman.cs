using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class Yeoman : Rank
    {
        public override Rank GetPreviousRank()
        {
            return null;
        }

        public override string GetRankName()
        {
            return "Yeoman";
        }
        public Yeoman()
            :base(0,0,0,0,0,0,0,0)
        {

        }
    }
}
