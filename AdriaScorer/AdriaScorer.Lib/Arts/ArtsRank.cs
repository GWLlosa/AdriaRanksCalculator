using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Arts
{
    public abstract class ArtsRank : Rank
    {

        public ArtsRank(int JourEP, int JourTW, int KniEP, int KniTW, int MWork, int War, int Demo)
        {
            minimumRecordForRank = new ArtsParticipationRecord()
            {
                DemoParticipations = Demo,
                JourneymanListParticipations = JourEP,
                JourneymanListWins = JourTW,
                KnightsListParticipations = KniEP,
                KnightsListWins = KniTW,
                MasterworksMade = MWork,
                WarParticipations = War
            };

        }
        ArtsParticipationRecord minimumRecordForRank;
       public static List<ArtsRank> CalculateArtsRanks(ArtsParticipationRecord record, out string missingRequirements)
        {
            var rank = new KnightDoctor();
            var results = rank.GetHighestQualifiedRank(record, out missingRequirements);
            return results
                .OfType<ArtsRank>()
                .ToList();
        }

       
       protected override bool DoesRankMeetCriteria(ParticipationRecord record)
       {
           ArtsParticipationRecord artsRecord = (ArtsParticipationRecord)record.Photocopy();
           artsRecord.Deduct(this.minimumRecordForRank);

           return (artsRecord.DemoParticipations >= 0 &&
                    artsRecord.JourneymanListParticipations >= 0 &&
                    artsRecord.JourneymanListWins >= 0 &&
                    artsRecord.KnightsListParticipations >= 0 &&
                    artsRecord.KnightsListWins >= 0 &&
                    artsRecord.MasterworksMade >= 0 &&
                    artsRecord.WarParticipations >= 0);
       }

       protected override void ConsumeRecord(ParticipationRecord record)
       {
           record.Deduct(this.minimumRecordForRank);
       }

       protected override string ExplainMissingRequirements(ParticipationRecord record)
       {
           StringBuilder explanationBuilder = new StringBuilder("Requirements Missing For " + this.GetRankName() + ": ");
           ArtsParticipationRecord testRecord = (ArtsParticipationRecord)record.Photocopy();
           testRecord.Deduct(this.minimumRecordForRank);

           explanationBuilder.Append(GetFriendlyMissingMessage("demonstrations", testRecord.DemoParticipations));
           explanationBuilder.Append(GetFriendlyMissingMessage("war participations", testRecord.WarParticipations));
           explanationBuilder.Append(GetFriendlyMissingMessage("Journeyman's List Participations", testRecord.JourneymanListParticipations));
           explanationBuilder.Append(GetFriendlyMissingMessage("Journeyman's List Wins", testRecord.JourneymanListWins));
           explanationBuilder.Append(GetFriendlyMissingMessage("Knights's List Participations", testRecord.KnightsListParticipations));
           explanationBuilder.Append(GetFriendlyMissingMessage("Knights's List Wins", testRecord.KnightsListWins));
           explanationBuilder.Append(GetFriendlyMissingMessage("Masterworks Made", testRecord.MasterworksMade));
           return explanationBuilder.ToString();
       }
    }
    
}
