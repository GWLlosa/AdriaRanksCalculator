using AdriaScorer.Lib.Archery;
using AdriaScorer.Lib.Arts;
using AdriaScorer.Lib.Combat;
using AdriaScorer.Lib.Ministry;
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
        public string Chapter { get; set; }
        public DateTime ExpiresOn { get; set; }

        public List<CombatRank> QualifiedCombatRanks { get; set; }
        public string MissingRequirementsForNextCombatRank { get; set; }

        public List<ArtsRank> QualifiedArtisanRanks { get; set; }
        public string MissingRequirementsForNextArtisanRank { get; set; }

        public List<ArcheryRank> QualifiedArcheryRanks { get; set; }
        public string MissingRequirementsForNextArcheryRank { get; set; }

        public List<MinistryRank> QualifiedMinistryRanks { get; set; }
        public string MissingRequirementsForNextMinistryRank { get; set; }
        
        public static Participant GetParticipantForId(int id)
        {
            try
            {
                string combatantName = WebReader.GetParticipantName(id);
                string chapter = WebReader.GetParticipantChapter(id);
                DateTime expirationDate = WebReader.GetExpirationDate(id);

                List<CombatRank> combatRanks;
                string combatReqs = "";

                try
                {
                    var combatrecord = WebReader.GetCombatRecord(id);
                    combatRanks = CombatRank.CalculateCombatRanks(combatrecord, out combatReqs);
                }
                catch (Exception)
                {
                    //Assume everyone's at least a Yeoman.
                    combatRanks = new List<CombatRank>();
                    combatRanks.Add(new Yeoman());
                    combatReqs = "No Combat Data";
                }


                List<ArtsRank> artisanRanks;
                string artsReqs = "";
                try
                {
                    var artsRecord = WebReader.GetArtsRecord(id);
                    artisanRanks = ArtsRank.CalculateArtsRanks(artsRecord, out artsReqs);
                }
                catch (Exception)
                {
                    //Assume everyone's at least an Apprentice.
                    artisanRanks = new List<ArtsRank>();
                    artisanRanks.Add(new Apprentice());
                    artsReqs = "No Artisan Data";
                }

                List<ArcheryRank> archeryRanks;
                string archeryReqs = "";
                try
                {
                    var archeryRecord = WebReader.GetArcheryRecord(id);
                    archeryRanks = ArcheryRank.CalculateArcheryRanks(archeryRecord, out archeryReqs);
                }
                catch (Exception)
                {
                    //Assume everyone's at least a Yeoman Archer
                    archeryRanks = new List<ArcheryRank>();
                    archeryRanks.Add(new YeomanArcher());
                    archeryReqs = "No Archery Data";
                }

                List<MinistryRank> ministryRanks;
                string ministryReqs = "";
                try
                {
                    var ministryRecord = WebReader.GetMinistryRecord(id);
                    ministryRanks = MinistryRank.CalculateMinistryRanks(ministryRecord, out ministryReqs);
                }
                catch (Exception)
                {
                    //Assume everyone's at least a Clarke.
                    ministryRanks = new List<MinistryRank>();
                    ministryRanks.Add(new Clarke());
                    ministryReqs = "No Ministry Data";
                }

                return new Participant()
                {
                    Name = combatantName,
                    Id = id,
                    QualifiedCombatRanks = combatRanks,
                    QualifiedArtisanRanks = artisanRanks,
                    QualifiedArcheryRanks = archeryRanks,
                    QualifiedMinistryRanks = ministryRanks,
                    ExpiresOn = expirationDate,
                    Chapter = chapter,
                    MissingRequirementsForNextCombatRank = combatReqs,
                    MissingRequirementsForNextArtisanRank = artsReqs,
                    MissingRequirementsForNextArcheryRank = archeryReqs,
                    MissingRequirementsForNextMinistryRank = ministryReqs
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
            fighterData.Insert(0, "Name,URL,Chapter,Highest Possible Combat Rank,Missing Combat Requirements,Highest Possible Artisan Rank,Missing Artisan Requirements, Highest Possible Archery Rank, Missing Archery Requirements,Highest Possible Ministry Rank, Missing Ministry Requirements");
            File.WriteAllLines(fileName, fighterData);
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(
                '"'+this.Name.Replace("\"","'")+"\",\""
                +this.Url+"\",\""
                + this.Chapter + "\",\""
                + this.QualifiedCombatRanks.Last().GetRankName() + "\",\""
                + this.MissingRequirementsForNextCombatRank + "\",\""
                + this.QualifiedArtisanRanks.Last().GetRankName() + "\",\""
                + this.MissingRequirementsForNextArtisanRank + "\",\""
                + this.QualifiedArcheryRanks.Last().GetRankName() + "\",\""
                + this.MissingRequirementsForNextArcheryRank + "\",\""
                + this.QualifiedMinistryRanks.Last().GetRankName() + "\",\""
                + this.MissingRequirementsForNextMinistryRank + "\""
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
