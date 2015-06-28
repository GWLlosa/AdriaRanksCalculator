using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class Participant
    {
        public string Name { get; set; }
        public List<Rank> QualifiedRanks { get; set; }
        public string Chapter { get; set; }
        public string MissingRequirementsForNextRank { get; set; }
        public DateTime ExpiresOn { get; set; }
        public static Participant GetParticipantForId(int id)
        {
            try
            {
                string combatantName = WebReader.GetCombatantName(id);
                string chapter = WebReader.GetCombatantChapter(id);
                DateTime expirationDate = WebReader.GetExpirationDate(id);
                var record = WebReader.GetRecord(id);
                string reqs = "";
                var ranks = Rank.CalculateRanks(record, out reqs);
                return new Participant()
                {
                    Name = combatantName,
                    Id = id,
                    QualifiedRanks = ranks,
                    ExpiresOn = expirationDate,
                    Chapter = chapter,
                    MissingRequirementsForNextRank = reqs
                };
            }
            catch (Exception)
            { return null; }
        }
        public static void DumpListToCSVFile(string fileName, List<Participant> fighters)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            List<string> fighterData = fighters
                .Where(fight => fight != null)
                .Select(fight => fight.ToString()).ToList();
            fighterData.Insert(0, "Name,URL,Chapter,Highest Potentially Qualified (But Not Necessarily Awarded) Combat Rank,Missing Requirements");
            File.WriteAllLines(fileName, fighterData);
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(
                '"'+this.Name+"\",\""
                +this.Url+"\",\""
                + this.Chapter + "\",\""
                + this.QualifiedRanks.Last().GetRankName() + "\",\""
                +this.MissingRequirementsForNextRank + "\""
                );
            
            return result.ToString();
        }

        public int Id { get; set; }

        public string Url
        {
            get
            {
                return "http://adrianempire.org/members/rolls.php?id=" + this.Id;
            }
        }
    }
}
