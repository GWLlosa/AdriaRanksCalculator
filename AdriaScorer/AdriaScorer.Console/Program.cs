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
        static void Main(string[] args)
        {
            System.Console.WriteLine("Test console mode!");
           
            List<Participant> fighters = new List<Participant>();
            //recommend range of 1-9400
            int minValue = 1;

            int maxValue = 9400;
            WebReader.LoadData("../../../../cache.bin");
            for (int i = minValue; i < maxValue; i++)
            {
                int id = i;
             
                        System.Console.WriteLine("Querying: " + id +" of "+maxValue);
                        var retVal = Participant.GetParticipantForId(id);
                        if (retVal != null)
                            System.Console.WriteLine("Found: " + retVal.Name);
                        fighters.Add(retVal);
            }
            System.Console.Clear();
            Participant.DumpListToCSVFile("../../../../output.csv", fighters);
            //WebReader.SaveData("../../../../cache.bin");
            foreach (var item in fighters.Select(tas=>tas))
            {

                if (item != null)
                {
                    System.Console.WriteLine("Combatant:" + item.Name);
                    foreach (var rank in item.QualifiedRanks)
                    {
                        System.Console.WriteLine("\t" + rank.GetRankName());
                    }
                }
            }
        }

       
    }
}
