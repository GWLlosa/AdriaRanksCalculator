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
        static string cachedFileData = "../../../../cache.bin";
        static void Main(string[] args)
        {
            //recommend range of 1-9400
            int minValue = 1;
            int maxValue = 9400;
           
            WebReader.SaveData(cachedFileData,minValue,maxValue);

        }
    }
}
