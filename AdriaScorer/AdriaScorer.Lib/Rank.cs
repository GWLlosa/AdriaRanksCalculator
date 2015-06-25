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
        public abstract Rank GetPreviousRank();
        public abstract string GetRankName();

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
            record.SergeantsListParticipations -= this.SergeantsParticipationsRequired;
            record.SergeantsListWins -= this.SergeantsListWinsRequired;
            record.KnightsListParticipations -= this.KnightsListParticipationsRequired;
            record.KnightsListWins -= this.KnightsListWinsRequired;
            record.WarParticipations -= this.WarParticipationsRequired;
            record.KnightsListArmoredParticipations -= this.KnightsListArmoredParticipations;
            record.KnightsListArmoredWins -= this.KnightsListArmoredWins;
            record.DemonstrationParticipations -= this.DemonstrationParticipationsRequired;
        }

        protected virtual bool DoesRankMeetCriteria(CombatParticipationRecord record)
        {
            return SergeantsParticipationsRequired <= record.SergeantsListParticipations &&
                            SergeantsListWinsRequired <= record.SergeantsListWins &&
                            DemonstrationParticipationsRequired <= record.DemonstrationParticipations &&
                            KnightsListParticipationsRequired <= record.KnightsListParticipations &&
                            KnightsListWinsRequired <= record.KnightsListWins &&
                            WarParticipationsRequired <= record.WarParticipations &&
                            KnightsListArmoredParticipations <= record.KnightsListArmoredParticipations &&
                            KnightsListArmoredWins <= record.KnightsListArmoredWins;
        }
        
        protected abstract int SergeantsParticipationsRequired{get;}
        protected abstract int SergeantsListWinsRequired{get;}
        protected abstract int DemonstrationParticipationsRequired{get;}
        protected abstract int KnightsListParticipationsRequired{get;}
        protected abstract int KnightsListWinsRequired{get;}
        protected abstract int WarParticipationsRequired{get;}
        protected abstract int KnightsListArmoredParticipations { get; }
        protected abstract int KnightsListArmoredWins { get; }

        protected virtual string ExplainMissingRequirements(CombatParticipationRecord record)
        {
            StringBuilder explanationBuilder = new StringBuilder("Requirements Missing For " + this.GetRankName()+": ");
            if (SergeantsParticipationsRequired > record.SergeantsListParticipations)
                explanationBuilder.Append(record.SergeantsListParticipations + " of " + SergeantsParticipationsRequired + " Sergeant's List Participations Required.  ");

            if (SergeantsListWinsRequired > record.SergeantsListWins)
                explanationBuilder.Append(record.SergeantsListWins + " of " + SergeantsListWinsRequired + " Sergeant's List Wins Required.  ");
            
            if (DemonstrationParticipationsRequired > record.DemonstrationParticipations)
                explanationBuilder.Append(record.DemonstrationParticipations + " of " + DemonstrationParticipationsRequired + "  Demonstration Participations Required.  ");
            
            if (KnightsListParticipationsRequired > record.KnightsListParticipations)
                explanationBuilder.Append(record.KnightsListParticipations + " of " + KnightsListParticipationsRequired + "  Knights List Participations Required.  ");
            
            if (KnightsListWinsRequired > record.KnightsListWins)
                explanationBuilder.Append(record.KnightsListWins + " of " + KnightsListWinsRequired + "  Knights List Wins Required.  ");
          
            if (WarParticipationsRequired > record.WarParticipations)
                explanationBuilder.Append(record.WarParticipations + " of " + WarParticipationsRequired + "  War Participations Required.  ");
           
            if (KnightsListArmoredParticipations > record.KnightsListArmoredParticipations)
                explanationBuilder.Append(record.KnightsListParticipations + " of " + KnightsListArmoredParticipations + "  Knights List (Armored) Participations Required.  ");
           
            if (KnightsListArmoredWins > record.KnightsListArmoredWins)
                explanationBuilder.Append(record.KnightsListArmoredWins + " of " + KnightsListArmoredWins + "  Knights List (Armored) Wins Required.  ");
           
            return explanationBuilder.ToString();
        }
    }
}
