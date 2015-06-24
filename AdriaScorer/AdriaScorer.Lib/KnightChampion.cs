using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class KnightChampion : Rank
    {
        protected override bool DoesRankMeetCriteria(CombatParticipationRecord record)
        {
            bool areSimpleReqsMet = DemonstrationParticipationsRequired <= record.DemonstrationParticipations &&
                            WarParticipationsRequired <= record.WarParticipations;

            bool areMinimumParticipationsArmored = record.KnightsListArmoredParticipations >= this.KnightsListArmoredParticipations;
            bool areMinimumWinsArmored = record.KnightsListArmoredWins >= this.KnightsListArmoredWins;
            bool areKnightParticipationReqsMet = (record.KnightsListArmoredParticipations + record.KnightsListParticipations) >= (this.KnightsListArmoredParticipations + this.KnightsListParticipationsRequired);
            bool areKnightWinsReqsMet = (record.KnightsListArmoredWins + record.KnightsListWins) >= (this.KnightsListArmoredWins + this.KnightsListWinsRequired);
            return (areSimpleReqsMet && areKnightParticipationReqsMet && areKnightWinsReqsMet && areMinimumParticipationsArmored && areMinimumWinsArmored);
        }
        protected override string ExplainMissingRequirements(CombatParticipationRecord record)
        {
            StringBuilder explanationBuilder = new StringBuilder("Requirements Missing For:" + this.GetRankName() + ": ");

            if (DemonstrationParticipationsRequired > record.DemonstrationParticipations)
                explanationBuilder.Append(record.DemonstrationParticipations + " of " + DemonstrationParticipationsRequired + "  Demonstration Participations Required.  ");
            if (WarParticipationsRequired > record.WarParticipations)
                explanationBuilder.Append(record.WarParticipations + " of " + WarParticipationsRequired + "  War Participations Required.  ");

            if (KnightsListArmoredParticipations > record.KnightsListArmoredParticipations)
                explanationBuilder.Append(record.KnightsListParticipations + " of " + KnightsListArmoredParticipations + "  Knights List (Armored) Participations Required.  ");
            if (KnightsListArmoredWins > record.KnightsListArmoredWins)
                explanationBuilder.Append(record.KnightsListArmoredWins + " of " + KnightsListArmoredWins + "  Knights List (Armored) Wins Required.  ");


            if ((record.KnightsListArmoredParticipations + record.KnightsListParticipations) < (this.KnightsListArmoredParticipations + this.KnightsListParticipationsRequired))
                explanationBuilder.Append((record.KnightsListArmoredParticipations + record.KnightsListParticipations) + " of " + (this.KnightsListArmoredParticipations + this.KnightsListParticipationsRequired) + "  Knights List Participations Required.  ");

            if ((record.KnightsListArmoredWins + record.KnightsListWins) > (this.KnightsListArmoredWins + this.KnightsListWinsRequired))
                explanationBuilder.Append((record.KnightsListArmoredWins + record.KnightsListWins) + " of " + (this.KnightsListArmoredWins + this.KnightsListWinsRequired) + "  Knights List Wins Required.  ");



            return explanationBuilder.ToString();
        }

        protected override void ConsumeRecord(CombatParticipationRecord record)
        {
            int totalNumberOfKnightsParticipationsRequired = this.KnightsListArmoredParticipations + this.KnightsListParticipationsRequired;
            for (int i = 0; i < totalNumberOfKnightsParticipationsRequired; i++)
            {
                if (record.KnightsListParticipations > 0)
                    record.KnightsListParticipations--;
                else
                    record.KnightsListArmoredParticipations--;
            }
            int totalNumberOfKnightsWinsRequired = this.KnightsListWinsRequired + this.KnightsListArmoredWins;
            for (int i = 0; i < totalNumberOfKnightsWinsRequired; i++)
            {
                if (record.KnightsListWins > 0)
                    record.KnightsListWins--;
                else
                    record.KnightsListArmoredWins--;
            }
            record.WarParticipations -= this.WarParticipationsRequired;
            record.DemonstrationParticipations -= this.DemonstrationParticipationsRequired;
        }

        public override Rank GetPreviousRank()
        {
            return new KnightBanneret();
        }

        public override string GetRankName()
        {
            return "Knight Champion";
        }

        protected override int SergeantsParticipationsRequired
        {
            get { return 0; }
        }

        protected override int SergeantsListWinsRequired
        {
            get { return 0; }
        }

        protected override int DemonstrationParticipationsRequired
        {
            get { return 15; }
        }

        protected override int KnightsListParticipationsRequired
        {
            get { return 20; }
        }

        protected override int KnightsListWinsRequired
        {
            get { return 4; }
        }

        protected override int WarParticipationsRequired
        {
            get { return 10; }
        }

        protected override int KnightsListArmoredParticipations
        {
            get { return 16; }
        }

        protected override int KnightsListArmoredWins
        {
            get { return 6; }
        }
    }
}
