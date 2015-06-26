using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class Guardsman : Rank
    {
        public Guardsman()
            :base(3,0,0,0,0,0,0,0)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new Yeoman();
        }

        public override string GetRankName()
        {
            return "Guardsman";
        }

    }
}
