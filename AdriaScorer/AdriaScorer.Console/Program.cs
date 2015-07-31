using AdriaScorer.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdriaScorer.Console
{
    class Program
    {
        static string folderName = "../../../../Spreadsheets/";
        static string cachedFileData = "../../../../cache.bin";
        static void Main(string[] args)
        {
            System.Console.WriteLine("Beginning Processing of Participant Data");
           
            List<Participant> fighters = new List<Participant>();
            //recommend range of 1-9400
            int minValue = 1;

            int maxValue = 9400;
            WebReader.LoadData(cachedFileData);
            DateTime cutoffDate = DateTime.Now.Subtract(new TimeSpan(365, 0, 0, 0));
            for (int i = minValue; i < maxValue; i++)
            {
                int id = i;
                if (i % 100 == 0)
                    System.Console.WriteLine("Querying: " + id + " of " + maxValue);
                var retVal = Participant.GetParticipantForId(id);
                if (retVal != null && !string.IsNullOrEmpty(retVal.Name))
                {
                    System.Console.WriteLine("Found: " + retVal.Name);
                    if (retVal.ExpiresOn > cutoffDate)
                        fighters.Add(retVal);
                }
            }
            List<Participant> renewedUsers = new List<Participant>();
            for (int i = minValue; i < maxValue; i++)
            {
                int id = i;
                if (i % 100 == 0)
                    System.Console.WriteLine("Querying: " + id + " of " + maxValue);
                var retVal = Participant.GetParticipantForId(id);
                if (retVal != null && !string.IsNullOrEmpty(retVal.Name))
                {
                    System.Console.WriteLine("Found: " + retVal.Name);
                    if (retVal.ExpiresOn.Year >= 2016)
                        renewedUsers.Add(retVal);
                }
            }

            System.Console.Clear();
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
            Participant.DumpListToCSVFile(folderName+"AllCombatants.csv", fighters);
            Participant.DumpMembershipStatusListToCSVFile(folderName + "AllRenewedUsers.csv", renewedUsers);
            var distinctChapters = fighters
                .Select(fighter => fighter.Chapter)
                .Distinct();

            foreach (var chapterName in distinctChapters)
            {
                System.Console.WriteLine("Writing results for: " + chapterName);
                Participant.DumpListToCSVFile(folderName + chapterName.Replace(" ", "").Replace("'", "")+".csv",
                    fighters.Where(fighter => fighter.Chapter == chapterName)
                    .ToList());
            }

            System.Console.WriteLine("Dumping XML");
            Participant.DumpListToXMLFile(folderName + "AllData.xml", fighters);

        }

       
    }
}
