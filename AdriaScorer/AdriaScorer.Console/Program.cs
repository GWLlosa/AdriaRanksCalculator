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
            int minValue = 8500;
            int maxValue = 8600;
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
            System.Console.WriteLine("Any key to exit plz");
            System.Console.ReadKey();
            
        }

       
    }
}
