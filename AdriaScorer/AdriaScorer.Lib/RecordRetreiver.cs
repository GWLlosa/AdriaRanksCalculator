﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public static string GetCombatantName(int id)
        {
            var webContent = GetWebContentForId(id);
            var stringContents = webContent.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string relevantString = stringContents.First(str => str.Contains("<title>"));
            return relevantString.Replace("<title>", "").Replace("</title>", "").Replace("Summary for", "").Trim();
        }
        public static string GetCombatantChapter(int id)
        {
            var webContent = GetWebContentForId(id);
            var stringContents = webContent.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string relevantString = stringContents.First(str => str.Contains("Chapter:"));
            return relevantString.Replace("<tr><td><b>Chapter:&nbsp;&nbsp;</b></td><td>", "").Replace("</td></tr>", "").Trim();
        }
        public static CombatParticipationRecord GetRecord(int id)
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
                KightsListParticipations = valArray[2],
                KnightsListWins = valArray[3],
                KnightsListArmoredParticipations = valArray[4],
                KnightsListArmoredWins = valArray[5],
                WarParticipations = valArray[6],
                DemonstrationParticipations = valArray[7]
            };
            return record;

        }
    }
}