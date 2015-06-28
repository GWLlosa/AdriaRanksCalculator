using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Combat
{
    public class Sergeant : CombatRank
    {
        public Sergeant()
            :base(5,1,0,0,0,0,0,2)
        {

        }
        public override Rank GetPreviousRank()
        {
            return new Guardsman();
        }

        public override string GetRankName()
        {
            return "Sergeant";
        }

    }
}
