using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Combat
{
    public abstract class CombatRank : Rank
    {
        public static List<CombatRank> CalculateRanks(int sergEP, int SergTW, int KniEP, int KniTW, int KniArmEP, int KniArmTW, int War, int Demo)
        {
            CombatParticipationRecord record = new CombatParticipationRecord()
            {
                DemonstrationParticipations = Demo,
                KnightsListParticipations = KniEP,
                KnightsListWins = KniTW,
                SergeantsListParticipations = sergEP,
                SergeantsListWins = SergTW,
                KnightsListArmoredParticipations = KniArmEP,
                KnightsListArmoredWins = KniArmTW,
                WarParticipations = War
            };
            string outs = "";
            return CalculateCombatRanks(record, out outs);
        }
        public static List<CombatRank> CalculateCombatRanks(CombatParticipationRecord record, out string missingRequirements)
        {
            var rank = new KnightChampion();
            var results = rank.GetHighestQualifiedRank(record,out missingRequirements);
            return results
                .OfType<CombatRank>()
                .ToList();
        }
        public CombatRank(int sergEP, int SergTW, int KniEP, int KniTW, int KniArmEP, int KniArmTW, int War, int Demo )
        {
            minimumRecordForRank = new CombatParticipationRecord()
            {
                DemonstrationParticipations = Demo,
                KnightsListParticipations = KniEP,
                KnightsListWins = KniTW,
                SergeantsListParticipations = sergEP,
                SergeantsListWins = SergTW,
                KnightsListArmoredParticipations = KniArmEP,
                KnightsListArmoredWins = KniArmTW,
                WarParticipations = War
            };
        }
        CombatParticipationRecord minimumRecordForRank;
        

        protected override void ConsumeRecord(ParticipationRecord record)
        {
            record.Deduct(this.minimumRecordForRank);
           
        }

        protected override bool DoesRankMeetCriteria(ParticipationRecord record)
        {
            CombatParticipationRecord testRecord = (CombatParticipationRecord)record.Photocopy();
            testRecord.Deduct(this.minimumRecordForRank);

            return (testRecord.DemonstrationParticipations >= 0 &&
                    testRecord.WarParticipations >= 0 &&
                    testRecord.SergeantsListParticipations >= 0 &&
                    testRecord.SergeantsListWins >= 0 &&
                    testRecord.KnightsListParticipations >= 0 &&
                    testRecord.KnightsListWins >= 0 &&
                    testRecord.KnightsListArmoredParticipations >= 0 &&
                    testRecord.KnightsListArmoredWins >= 0);
            
        }
        
        

        protected override string ExplainMissingRequirements(ParticipationRecord record)
        {
            StringBuilder explanationBuilder = new StringBuilder("Requirements Missing For " + this.GetRankName()+": ");
            CombatParticipationRecord testRecord = (CombatParticipationRecord)record.Photocopy();
            testRecord.Deduct(this.minimumRecordForRank);
            explanationBuilder.Append(GetFriendlyMissingMessage("demonstrations", testRecord.DemonstrationParticipations));
            explanationBuilder.Append(GetFriendlyMissingMessage("war participations", testRecord.WarParticipations));
            explanationBuilder.Append(GetFriendlyMissingMessage("Sergeant's List Participations", testRecord.SergeantsListParticipations));
            explanationBuilder.Append(GetFriendlyMissingMessage("Sergeant's List Wins", testRecord.SergeantsListWins));
            explanationBuilder.Append(GetFriendlyMissingMessage("Knight's List Participations", testRecord.KnightsListParticipations));
            explanationBuilder.Append(GetFriendlyMissingMessage("Knight's List Wins", testRecord.KnightsListWins));
            explanationBuilder.Append(GetFriendlyMissingMessage("Knight's List (Armored) Participations", testRecord.KnightsListArmoredParticipations));
            explanationBuilder.Append(GetFriendlyMissingMessage("Knight's List (Armored) Wins", testRecord.KnightsListArmoredWins));
            return explanationBuilder.ToString();
        }
    }
}
