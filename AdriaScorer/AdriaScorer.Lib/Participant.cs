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
        public static Participant GetParticipantForId(int id)
        {
            try
            {
                string combatantName = WebReader.GetCombatantName(id);
                var ranks = Rank.CalculateRanks(WebReader.GetRecord(id));
                return new Participant()
                {
                    Name = combatantName,
                    Id = id,
                    QualifiedRanks = ranks
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
            fighterData.Insert(0,"Name,URL");
            File.WriteAllLines(fileName, fighterData);
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder('"'+this.Name+"\",\""+this.Url+"\"");
            foreach (var item in QualifiedRanks)
            {
                result.Append(",");
                result.Append('"'+item.GetRankName()+'"');
            }
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
