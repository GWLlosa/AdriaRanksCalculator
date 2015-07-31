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
using System.Xml.Linq;

namespace AdriaScorer.Lib
{
    public class Participant
    {
        #region Public Properties
        public string Name { get; set; }
        public string Chapter { get; set; }
        public DateTime ExpiresOn { get; set; }
        public int Id { get; set; }
        public string Url
        {
            get
            {
                return "http://adrianempire.org/members/rolls.php?id=" + this.Id;
            }
        }
        public DateTime UpdatedDate { get; set; }
        public List<CombatRank> QualifiedCombatRanks { get; set; }
        public string MissingRequirementsForNextCombatRank { get; set; }
        public List<ArtsRank> QualifiedArtisanRanks { get; set; }
        public string MissingRequirementsForNextArtisanRank { get; set; }
        public List<ArcheryRank> QualifiedArcheryRanks { get; set; }
        public string MissingRequirementsForNextArcheryRank { get; set; }
        public List<MinistryRank> QualifiedMinistryRanks { get; set; }
        public string MissingRequirementsForNextMinistryRank { get; set; }
        #endregion
        public static Participant GetParticipantForId(int id)
        {
            try
            {
                string combatantName = WebReader.GetParticipantName(id);
                string chapter = WebReader.GetParticipantChapter(id);
                DateTime expirationDate = WebReader.GetExpirationDate(id);
                DateTime lastUpdateDate = WebReader.GetLastUpdatedDate(id);

                List<CombatRank> combatRanks;
                string combatReqs;
                ExtractCombatRanks(id, out combatRanks, out combatReqs);


                List<ArtsRank> artisanRanks;
                string artsReqs;
                ExtractArtisanRanks(id, out artisanRanks, out artsReqs);

                List<ArcheryRank> archeryRanks;
                string archeryReqs;
                ExtractArcheryRanks(id, out archeryRanks, out archeryReqs);

                List<MinistryRank> ministryRanks;
                string ministryReqs;
                ExtractMinistryRanks(id, out ministryRanks, out ministryReqs);

                return new Participant()
                {
                    Name = combatantName,
                    Id = id,
                    QualifiedCombatRanks = combatRanks,
                    QualifiedArtisanRanks = artisanRanks,
                    QualifiedArcheryRanks = archeryRanks,
                    QualifiedMinistryRanks = ministryRanks,
                    ExpiresOn = expirationDate,
                    UpdatedDate = lastUpdateDate,
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
        #region Rank Extraction
        private static void ExtractMinistryRanks(int id, out List<MinistryRank> ministryRanks, out string ministryReqs)
        {

            ministryReqs = "";
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
        }

        private static void ExtractArcheryRanks(int id, out List<ArcheryRank> archeryRanks, out string archeryReqs)
        {

            archeryReqs = "";
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
        }

        private static void ExtractArtisanRanks(int id, out List<ArtsRank> artisanRanks, out string artsReqs)
        {

            artsReqs = "";
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
        }

        private static void ExtractCombatRanks(int id, out List<CombatRank> combatRanks, out string combatReqs)
        {

            combatReqs = "";

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
        }
        #endregion

        public static void DumpMembershipStatusListToCSVFile(string fileName, List<Participant> members)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            var memberData = members
                .Where(member => member != null)
                .Select(member =>
                '"' + member.Name.Replace("\"", "'") + "\",\""
                + member.Url + "\",\""
                + member.Chapter + "\",\""
                + member.UpdatedDate.ToShortDateString() + "\",\""
                + member.ExpiresOn.ToShortDateString() + "\""
            ).ToList();
            memberData.Insert(0, "Name,URL,Chapter,Date Last Updated, Expires On");
            File.WriteAllLines(fileName, memberData);
        }
        public static void DumpListToCSVFile(string fileName, List<Participant> fighters)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            List<string> fighterData = fighters
                .Where(fight => fight != null)
                .Select(fight => fight.ToString()).ToList();
            fighterData.Insert(0, "Name,URL,Chapter,Date Last Updated, Highest Possible Combat Rank,Missing Combat Requirements,Highest Possible Artisan Rank,Missing Artisan Requirements, Highest Possible Archery Rank, Missing Archery Requirements,Highest Possible Ministry Rank, Missing Ministry Requirements");
            File.WriteAllLines(fileName, fighterData);
        }
        public static void DumpListToXMLFile(string fileName, List<Participant> participants)
        {
            XElement rootNode = new XElement("Participants",
                participants.Select(part => part.ToXml()));
            File.WriteAllText(fileName, rootNode.ToString());
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(
                '"'+this.Name.Replace("\"","'")+"\",\""
                +this.Url+"\",\""
                + this.Chapter + "\",\""
                + this.UpdatedDate.ToShortDateString() + "\",\""
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

        public XElement ToXml()
        {
            return new XElement("Participant",
                new XElement("Name",this.Name),
                new XElement("Chapter",this.Chapter),
                new XElement("ExpirationDate",this.ExpiresOn),
                new XElement("Id",this.Id),
                new XElement("Url",this.Url),
                new XElement("BestCombatRank",this.QualifiedCombatRanks.Last().GetRankName()),
                new XElement("NextCombatRankReqs",this.MissingRequirementsForNextCombatRank),
                new XElement("BestArtisanRank",this.QualifiedArtisanRanks.Last().GetRankName()),
                new XElement("NextArtisanRankReqs",this.MissingRequirementsForNextArtisanRank),
                new XElement("BestArcheryRank",this.QualifiedArcheryRanks.Last().GetRankName()),
                new XElement("NextArcheryRankReqs",this.MissingRequirementsForNextArcheryRank),
                new XElement("BestMinistryRank",this.QualifiedMinistryRanks.Last().GetRankName()),
                new XElement("NextMinistryRankReqs",this.MissingRequirementsForNextMinistryRank)
                );
        }
    }
}
