using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class CombatParticipationRecord
    {
        public int SergeantsListParticipations
        { get; set; }
        public int SergeantsListWins
        { get; set; }
        public int DemonstrationParticipations
        { get; set; }
        public int KnightsListParticipations
        { get; set; }
        public int KnightsListWins
        { get; set; }
        public int WarParticipations
        { get; set; }


        public int KnightsListArmoredWins { get; set; }

        public int KnightsListArmoredParticipations { get; set; }

        internal void Deduct(CombatParticipationRecord combatParticipationRecord)
        {
            
            //Demo points are isolated.
            DemonstrationParticipations -= combatParticipationRecord.DemonstrationParticipations;
            //War points are isolated.
            WarParticipations -= combatParticipationRecord.WarParticipations;
            
            //Sergeant's List Participations.
            for (int i = combatParticipationRecord.SergeantsListParticipations; i >0; i--)
            {
                //Need sergeant's list? Start with sergeant's list.
                if (SergeantsListParticipations > 0)
                    SergeantsListParticipations--;
                //All out of sergeant's list?  You can use knight's list.
                else if (KnightsListParticipations > 0)
                    KnightsListParticipations--;
                //doh.  This is going to fail.
                else
                    SergeantsListParticipations--;
            }

            //Sergeant's List Wins.
            for (int i = combatParticipationRecord.SergeantsListWins; i > 0; i--)
            {
                if (SergeantsListWins > 0)
                    SergeantsListWins--;
                else if (KnightsListWins > 0)
                    KnightsListWins--;
                else if (KnightsListArmoredWins > 0)
                    KnightsListArmoredWins--;
                
            }

            //Knight's List Participations.
            KnightsListParticipations -= combatParticipationRecord.KnightsListParticipations;

            //Knight's List Wins.
            KnightsListWins -= combatParticipationRecord.KnightsListWins;

            //Knight's List Armored Participation
            KnightsListArmoredParticipations -= combatParticipationRecord.KnightsListArmoredParticipations;

            //Knight's List Armored Wins
            KnightsListArmoredWins -= combatParticipationRecord.KnightsListArmoredWins;

            

        }

        internal CombatParticipationRecord Photocopy()
        {
            return new CombatParticipationRecord()
            {
                DemonstrationParticipations = this.DemonstrationParticipations,
                KnightsListArmoredParticipations = this.KnightsListArmoredParticipations,
                KnightsListArmoredWins = this.KnightsListArmoredWins,
                KnightsListParticipations = this.KnightsListParticipations,
                KnightsListWins = this.KnightsListWins,
                SergeantsListParticipations = this.SergeantsListParticipations,
                SergeantsListWins = this.SergeantsListWins,
                WarParticipations = this.WarParticipations
            };
        }
    }
}
