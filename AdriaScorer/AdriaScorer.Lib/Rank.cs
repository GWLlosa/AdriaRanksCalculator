using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public abstract class Rank
    {
        public static List<Rank> CalculateRanks(int sergEP, int SergTW, int KniEP, int KniTW, int KniArmEP, int KniArmTW, int War, int Demo)
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
            return CalculateRanks(record, out outs);
        }
        public static List<Rank> CalculateRanks(CombatParticipationRecord record, out string missingRequirements)
        {
            var rank = new KnightChampion();
            var results = rank.GetHighestQualifiedRank(record,out missingRequirements);
            return results;
        }
        public Rank(int sergEP, int SergTW, int KniEP, int KniTW, int KniArmEP, int KniArmTW, int War, int Demo )
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
        public abstract Rank GetPreviousRank();
        public abstract string GetRankName();
        CombatParticipationRecord minimumRecordForRank;
        public List<Rank> GetHighestQualifiedRank(CombatParticipationRecord record, out string missingRequirements)
        {
            missingRequirements = "Satisfied";
            List<Rank> qualifiedRanks;
            Rank previousRank = GetPreviousRank();
            if (previousRank == null)
                qualifiedRanks = new List<Rank>();
            else
            {
                qualifiedRanks = previousRank.GetHighestQualifiedRank(record, out missingRequirements);
            }

            if (DoesRankMeetCriteria(record) && (qualifiedRanks.Contains(previousRank) || previousRank == null))
            {
                ConsumeRecord(record);
                qualifiedRanks.Add(this);
                missingRequirements = "Satisfied";
            } 
            else
            {
                if (missingRequirements == "Satisfied")
                    missingRequirements  = this.ExplainMissingRequirements(record);
            }
            
            return qualifiedRanks;
        }

        protected virtual void ConsumeRecord(CombatParticipationRecord record)
        {
            record.Deduct(this.minimumRecordForRank);
           
        }

        protected virtual bool DoesRankMeetCriteria(CombatParticipationRecord record)
        {
            CombatParticipationRecord testRecord = record.Photocopy();
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
        
        

        protected virtual string ExplainMissingRequirements(CombatParticipationRecord record)
        {
            StringBuilder explanationBuilder = new StringBuilder("Requirements Missing For " + this.GetRankName()+": ");
            CombatParticipationRecord testRecord = record.Photocopy();
            testRecord.Deduct(this.minimumRecordForRank);
            if (testRecord.DemonstrationParticipations < 0)
            {
                explanationBuilder.Append("Missing " + testRecord.DemonstrationParticipations * -1 + " demonstrations.  ");
            }
            if (testRecord.WarParticipations < 0)
            {
                explanationBuilder.Append("Missing " + testRecord.WarParticipations * -1 + " war participations.  ");
            }
            if (testRecord.SergeantsListParticipations < 0)
            {
                explanationBuilder.Append("Missing " + testRecord.SergeantsListParticipations * -1 + " Sergeant's List Participations.  ");
            }
            if (testRecord.SergeantsListWins < 0)
            {
                explanationBuilder.Append("Missing " + testRecord.SergeantsListWins * -1 + " Sergeant's List Wins.  ");
            }
            if (testRecord.KnightsListParticipations < 0)
            {
                explanationBuilder.Append("Missing " + testRecord.KnightsListParticipations * -1 + " Knight's List Participations.  ");
            }
            if (testRecord.KnightsListWins < 0)
            {
                explanationBuilder.Append("Missing " + testRecord.KnightsListWins * -1 + " Knight's List Wins.  ");
            }
            if (testRecord.KnightsListArmoredParticipations < 0)
            {
                explanationBuilder.Append("Missing " + testRecord.KnightsListArmoredParticipations * -1 + " Knight's List (Armored) Participations.  ");
            }
            if (testRecord.KnightsListArmoredWins < 0)
            {
                explanationBuilder.Append("Missing " + testRecord.KnightsListArmoredWins * -1 + " Knight's List (Armored) Wins.  ");
            }
            
           
            return explanationBuilder.ToString();
        }
    }
}
