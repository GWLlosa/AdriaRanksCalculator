using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public abstract class Rank
    {
        public abstract Rank GetPreviousRank();
        public abstract string GetRankName();
        public List<Rank> GetHighestQualifiedRank(ParticipationRecord record, out string missingRequirements)
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
                    missingRequirements = this.ExplainMissingRequirements(record);
            }

            return qualifiedRanks;
        }

        protected abstract bool DoesRankMeetCriteria(ParticipationRecord record);
        protected abstract void ConsumeRecord(ParticipationRecord record);
        protected abstract string ExplainMissingRequirements(ParticipationRecord record);
        protected string GetFriendlyMissingMessage(string friendlyFieldName, int fieldValue)
        {
            if (fieldValue < 0)
            {
                return "Missing " + fieldValue*-1 + " " +friendlyFieldName + ".  ";
            }
            return "";
        }
    }
}
