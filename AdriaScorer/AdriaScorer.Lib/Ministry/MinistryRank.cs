using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Ministry
{
    public abstract class MinistryRank : Rank
    {
        public MinistryRank(int EP, int War, int Demo, int DI)
        {
            minimumRecordForRank = new MinistryParticipationRecord()
           {
               War = War,
               Demo = Demo,
               Participation = EP,
               DemoInitiation = DI
           };
        }
        MinistryParticipationRecord minimumRecordForRank;
        protected override bool DoesRankMeetCriteria(ParticipationRecord record)
        {
            MinistryParticipationRecord minRecord = (MinistryParticipationRecord)record.Photocopy();
            minRecord.Deduct(this.minimumRecordForRank);
            return (minRecord.Demo >= 0 &&
                    minRecord.DemoInitiation >= 0 &&
                    minRecord.War >= 0 &&
                    minRecord.Participation >= 0);
        }
        public static List<MinistryRank> CalculateMinistryRanks(MinistryParticipationRecord record, out string missingRequirements)
        {
            var rank = new KnightPremier();
            var results = rank.GetHighestQualifiedRank(record, out missingRequirements);
            return results
                .OfType<MinistryRank>()
                .ToList();
        }
        protected override void ConsumeRecord(ParticipationRecord record)
        {
            record.Deduct(this.minimumRecordForRank);
        }

        protected override string ExplainMissingRequirements(ParticipationRecord record)
        {
            StringBuilder explanationBuilder = new StringBuilder("Requirements Missing For " + this.GetRankName() + ": ");
            MinistryParticipationRecord testRecord = (MinistryParticipationRecord)record.Photocopy();
            testRecord.Deduct(this.minimumRecordForRank);
            explanationBuilder.Append(GetFriendlyMissingMessage("Ministry Participations", testRecord.Participation));
            explanationBuilder.Append(GetFriendlyMissingMessage("demonstration initiations", testRecord.DemoInitiation));
            explanationBuilder.Append(GetFriendlyMissingMessage("war participations", testRecord.War));
            explanationBuilder.Append(GetFriendlyMissingMessage("demonstrations", testRecord.Demo));
            return explanationBuilder.ToString();
        }
    }
}
