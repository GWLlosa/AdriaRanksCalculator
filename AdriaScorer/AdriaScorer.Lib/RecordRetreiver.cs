using AdriaScorer.Lib.Archery;
using AdriaScorer.Lib.Combat;
using AdriaScorer.Lib.Ministry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public class WebReader
    {
        private static Dictionary<int, string> contentForId = new Dictionary<int, string>();

        private static string GetWebContentForId(int id)
        {
            
            lock (contentForId)
            {
                if (contentForId.ContainsKey(id))
                    return contentForId[id];
            }
            Uri query = new Uri("http://adrianempire.org/members/rolls.php?id=" + id.ToString());
            HttpClient client = new HttpClient();
            string webContent = "";
            try
            {
                webContent = client.GetAsync(query).Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception err)
            {
                int x = 5;
                throw;
            }
            lock (contentForId)
            {
                contentForId[id] = webContent;
                return webContent;
            }
        }
        public static DateTime GetExpirationDate(int id)
        {
            var webContent = GetWebContentForId(id);
            var stringContents = webContent.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string relevantString = stringContents.First(str => str.Contains("Expires:"));
            string date = relevantString.Replace("<tr><td><b>Expires:&nbsp;&nbsp;</b></td><td>", "").Replace("</td></tr>", "");
            if (date == "never")
                return DateTime.MaxValue;
            else
                return DateTime.Parse(date);

        }
        public static string GetParticipantName(int id)
        {
            var webContent = GetWebContentForId(id);
            var stringContents = webContent.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string relevantString = stringContents.First(str => str.Contains("<title>"));
            return relevantString.Replace("<title>", "").Replace("</title>", "").Replace("Summary for", "").Trim();
        }
        public static string GetParticipantChapter(int id)
        {
            var webContent = GetWebContentForId(id);
            var stringContents = webContent.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string relevantString = stringContents.First(str => str.Contains("Chapter:"));
            return relevantString.Replace("<tr><td><b>Chapter:&nbsp;&nbsp;</b></td><td>", "").Replace("</td></tr>", "").Trim();
        }
        public static CombatParticipationRecord GetCombatRecord(int id)
        {
            var webContent = GetWebContentForId(id);
            var stringContents = webContent.Split(new string[] {Environment.NewLine, "\n"},StringSplitOptions.RemoveEmptyEntries);
            string relevantString = stringContents.First(str => str.Contains("<tr ALIGN=CENTER BGCOLOR=\"#F69679\">"));
            var values = relevantString.Split(new string[] { "<td>", "</td>" }, StringSplitOptions.RemoveEmptyEntries);
            int sillyInt = 0;
            var valArray = values
                            .Where(val => int.TryParse(val, out sillyInt))
                            .Select(val => int.Parse(val))
                            .ToArray();

            CombatParticipationRecord record = new CombatParticipationRecord()
            {
                SergeantsListParticipations = valArray[0],
                SergeantsListWins = valArray[1],
                KnightsListParticipations = valArray[2],
                KnightsListWins = valArray[3],
                KnightsListArmoredParticipations = valArray[4],
                KnightsListArmoredWins = valArray[5],
                WarParticipations = valArray[6],
                DemonstrationParticipations = valArray[7]
            };
            return record;
        }

        public static ArcheryParticipationRecord GetArcheryRecord(int id)
        {
            var webContent = GetWebContentForId(id);
            var stringContents = webContent.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string relevantString = stringContents.First(str => str.Contains("<tr ALIGN=CENTER BGCOLOR=\"#82CA9C\">"));
            var values = relevantString.Split(new string[] { "<td>", "</td>" }, StringSplitOptions.RemoveEmptyEntries);
            int sillyInt = 0;
            var valArray = values
                            .Where(val => int.TryParse(val, out sillyInt))
                            .Select(val => int.Parse(val))
                            .ToArray();

            ArcheryParticipationRecord record = new ArcheryParticipationRecord()
            {
                BowmanParticipations = valArray[0],
                BowmanWins = valArray[1],
                HuntsmanParticipations = valArray[2],
                HuntsmanWins = valArray[3],
                War = valArray[4],
                Demo = valArray[5]
            };
            return record;
        }
        public static MinistryParticipationRecord GetMinistryRecord(int id)
        {
            var webContent = GetWebContentForId(id);
            var stringContents = webContent.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string relevantString = stringContents.First(str => str.Contains("<tr ALIGN=CENTER BGCOLOR=\"#6DCFF6\">"));
            var values = relevantString.Split(new string[] { "<td>", "</td>" }, StringSplitOptions.RemoveEmptyEntries);
            int sillyInt = 0;
            var valArray = values
                            .Where(val => int.TryParse(val, out sillyInt))
                            .Select(val => int.Parse(val))
                            .ToArray();

            MinistryParticipationRecord record = new MinistryParticipationRecord()
            {
                Participation = valArray[0],
                War = valArray[1],
                Demo = valArray[2],
                DemoInitiation = valArray[3]
            };
            return record;
        }
        public static ArtsParticipationRecord GetArtsRecord(int id)
        {
            var webContent = GetWebContentForId(id);
            var stringContents = webContent.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string relevantString = stringContents.First(str => str.Contains("<tr ALIGN=CENTER BGCOLOR=\"#FFF799\">"));
            var values = relevantString.Split(new string[] { "<td>", "</td>" }, StringSplitOptions.RemoveEmptyEntries);
            int sillyInt = 0;
            var valArray = values
                            .Where(val => int.TryParse(val, out sillyInt))
                            .Select(val => int.Parse(val))
                            .ToArray();

            ArtsParticipationRecord record = new ArtsParticipationRecord()
            {
                JourneymanListParticipations = valArray[0],
                JourneymanListWins = valArray[1],
                KnightsListParticipations = valArray[2],
                KnightsListWins = valArray[3],
                MasterworksMade = valArray[4],
                WarParticipations = valArray[5],
                DemoParticipations = valArray[6]
            };
            return record;
        }
        public static void SaveData(string file, int startingId, int endingId)
        {
            for (int i = startingId; i < endingId; i++)
            {
                if (i % 250 == 0)
                    System.Console.WriteLine("Processing ID:" + i);
                GetWebContentForId(i);
            }
            SaveData(file);
        }
        public static void SaveData(string file)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream outStream = File.OpenWrite(file);
            bf.Serialize(outStream, contentForId);
            outStream.Flush();
            outStream.Close();
        }
        public static void LoadData(string file)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream inStream = File.OpenRead(file);
            contentForId = (Dictionary<int,string>)bf.Deserialize(inStream);
        }



       
    }
}
