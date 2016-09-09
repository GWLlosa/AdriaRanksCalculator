using AdriaScorer.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    class Program
    {
        static string cachedFileData = "cache.bin";
        static void Main(string[] args)
        {
            //recommend range of 1-9400
            int minValue = 1;
            int maxValue = 10000;
           
            WebReader.SaveData(cachedFileData,minValue,maxValue);

            System.Console.WriteLine("Beginning Processing of Participant Data");

            List<Participant> fighters = new List<Participant>();

            WebReader.LoadData(cachedFileData);
            for (int i = minValue; i < maxValue; i++)
            {
                int id = i;
                if (i % 100 == 0)
                    System.Console.WriteLine("Querying: " + id + " of " + maxValue);
                var retVal = Participant.GetParticipantForId(id);
                if (retVal != null && !string.IsNullOrEmpty(retVal.Name))
                {
                    System.Console.WriteLine("Found: " + retVal.Name);
                    fighters.Add(retVal);
                }
            }

            using (var dbConn = new ParticipantContext())
            {
              //  dbConn.Database.Delete();
                dbConn.Participants.AddRange(fighters);
                dbConn.SaveChanges();
            }

        }
    }
}
