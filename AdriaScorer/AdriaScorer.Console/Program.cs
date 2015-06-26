using AdriaScorer.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Console
{
    class Program
    {
        static string folderName = "../../../../Spreadsheets/";
        static string cachedFileData = "../../../../cache.bin";
        static void Main(string[] args)
        {
            System.Console.WriteLine("Test console mode!");
           
            List<Participant> fighters = new List<Participant>();
            //recommend range of 1-9400
            int minValue = 1;

            int maxValue = 9400;
            WebReader.LoadData(cachedFileData);
            for (int i = minValue; i < maxValue; i++)
            {
                int id = i;
             
                        System.Console.WriteLine("Querying: " + id +" of "+maxValue);
                        var retVal = Participant.GetParticipantForId(id);
                        if (retVal != null)
                        {
                            System.Console.WriteLine("Found: " + retVal.Name);
                            fighters.Add(retVal);
                        }
            }
            System.Console.Clear();
            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);
            Participant.DumpListToCSVFile(folderName+"AllCombatants.csv", fighters);
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

        }

       
    }
}
