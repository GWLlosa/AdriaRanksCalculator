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
                KightsListParticipations = KniEP,
                KnightsListWins = KniTW,
                SergeantsListParticipations = sergEP,
                SergeantsListWins = SergTW,
                KnightsListArmoredParticipations = KniArmEP,
                KnightsListArmoredWins = KniArmTW,
                WarParticipations = War
            };
            return CalculateRanks(record);
        }
        public static List<Rank> CalculateRanks(CombatParticipationRecord record)
        {
            var rank = new KnightChampion();
            var results = rank.GetHighestQualifiedRank(record);
            return results;
        }
        public abstract Rank GetPreviousRank();
        public abstract string GetRankName();
        public List<Rank> GetHighestQualifiedRank(CombatParticipationRecord record)
        {
            List<Rank> qualifiedRanks;
            Rank previousRank = GetPreviousRank();
            if (previousRank == null)
                qualifiedRanks = new List<Rank>();
            else
            {
                qualifiedRanks = previousRank.GetHighestQualifiedRank(record);
            }

            if (DoesRankMeetCriteria(record) && (qualifiedRanks.Contains(previousRank) || previousRank == null))
            {
                ConsumeRecord(record);
                qualifiedRanks.Add(this);
            }
            return qualifiedRanks;
        }

        protected virtual void ConsumeRecord(CombatParticipationRecord record)
        {
            record.SergeantsListParticipations -= this.SergeantsParticipationsRequired;
            record.SergeantsListWins -= this.SergeantsListWinsRequired;
            record.KightsListParticipations -= this.KnightsListParticipationsRequired;
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
                            KnightsListParticipationsRequired <= record.KightsListParticipations &&
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
    }
}
