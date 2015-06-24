using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class KnightBanneret : Rank
    {
        protected override bool DoesRankMeetCriteria(CombatParticipationRecord record)
        {
            bool areSimpleReqsMet = DemonstrationParticipationsRequired <= record.DemonstrationParticipations &&
                            WarParticipationsRequired <= record.WarParticipations;

            bool areMinimumParticipationsArmored = record.KnightsListArmoredParticipations >= this.KnightsListArmoredParticipations;
            bool areMinimumWinsArmored = record.KnightsListArmoredWins >= this.KnightsListArmoredWins;
            bool areKnightParticipationReqsMet = (record.KnightsListArmoredParticipations + record.KightsListParticipations) >= (this.KnightsListArmoredParticipations + this.KnightsListParticipationsRequired);
            bool areKnightWinsReqsMet = (record.KnightsListArmoredWins + record.KnightsListWins) >= (this.KnightsListArmoredWins + this.KnightsListWinsRequired);
            return (areSimpleReqsMet && areKnightParticipationReqsMet && areKnightWinsReqsMet && areMinimumParticipationsArmored && areMinimumWinsArmored);
        }

        protected override void ConsumeRecord(CombatParticipationRecord record)
        {
            int totalNumberOfKnightsParticipationsRequired = this.KnightsListArmoredParticipations + this.KnightsListParticipationsRequired;
            for (int i = 0; i < totalNumberOfKnightsParticipationsRequired; i++)
            {
                if (record.KightsListParticipations > 0)
                    record.KightsListParticipations--;
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
            return new KnightBachelor();
        }

        public override string GetRankName()
        {
            return "Knight Banneret";
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
            get { return 5; }
        }

        protected override int KnightsListParticipationsRequired
        {
            get { return 10; }
        }

        protected override int KnightsListWinsRequired
        {
            get { return 2; }
        }

        protected override int WarParticipationsRequired
        {
            get { return 5; }
        }

        protected override int KnightsListArmoredParticipations
        {
            get { return 8; }
        }

        protected override int KnightsListArmoredWins
        {
            get { return 3; }
        }
    }
}
