using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Archery
{
    public abstract class ArcheryRank : Rank
    {
        public ArcheryRank(int BowmEP, int BowmTW, int HuntEP, int HuntTW, int War, int Demo)
        {
            minimumRecordForRank = new ArcheryParticipationRecord()
            {
                War = War,
                HuntsmanWins = HuntTW,
                HuntsmanParticipations = HuntEP,
                Demo = Demo,
                BowmanWins = BowmTW,
                BowmanParticipations = BowmEP
            };
        }
        public static List<ArcheryRank> CalculateArcheryRanks(ArcheryParticipationRecord record, out string missingRequirements)
        {
            var rank = new KnightWarden();
            var results = rank.GetHighestQualifiedRank(record, out missingRequirements);
            return results
                .OfType<ArcheryRank>()
                .ToList();
        }
        ArcheryParticipationRecord minimumRecordForRank;
        protected override bool DoesRankMeetCriteria(ParticipationRecord record)
        {
            ArcheryParticipationRecord archeryRecord = (ArcheryParticipationRecord)record.Photocopy();
            archeryRecord.Deduct(this.minimumRecordForRank);

            return (archeryRecord.BowmanParticipations >= 0 &&
                    archeryRecord.BowmanWins >= 0 &&
                    archeryRecord.Demo >= 0 &&
                    archeryRecord.HuntsmanParticipations >= 0 &&
                    archeryRecord.HuntsmanWins >= 0 &&
                    archeryRecord.War >= 0);
        }

        protected override void ConsumeRecord(ParticipationRecord record)
        {
            record.Deduct(this.minimumRecordForRank);
        }

        protected override string ExplainMissingRequirements(ParticipationRecord record)
        {
            StringBuilder explanationBuilder = new StringBuilder("Requirements Missing For " + this.GetRankName() + ": ");
            ArcheryParticipationRecord testRecord = (ArcheryParticipationRecord)record.Photocopy();
            testRecord.Deduct(this.minimumRecordForRank);

            explanationBuilder.Append(GetFriendlyMissingMessage("Bowman's List Participations", testRecord.BowmanParticipations));
            explanationBuilder.Append(GetFriendlyMissingMessage("Bowman's List Wins", testRecord.BowmanWins));
            explanationBuilder.Append(GetFriendlyMissingMessage("Huntsman's List Participations", testRecord.HuntsmanParticipations));
            explanationBuilder.Append(GetFriendlyMissingMessage("Huntsman's List Wins", testRecord.HuntsmanWins));
            explanationBuilder.Append(GetFriendlyMissingMessage("war participations", testRecord.War));
            explanationBuilder.Append(GetFriendlyMissingMessage("demonstrations", testRecord.Demo));
            return explanationBuilder.ToString();
            
        }
    }
}
