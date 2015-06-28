using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Combat
{
    public class Yeoman : CombatRank
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
