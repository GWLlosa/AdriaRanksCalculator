using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Archery
{
    public class ArcheryParticipationRecord : ParticipationRecord
    {
        public int BowmanParticipations { get; set; }
        public int BowmanWins { get; set; }
        public int HuntsmanParticipations { get; set; }
        public int HuntsmanWins { get; set; }
        public int War { get; set; }
        public int Demo { get; set; }

        public override void Deduct(ParticipationRecord ParticipationRecord)
        {
            ArcheryParticipationRecord archeryRecord = (ArcheryParticipationRecord)ParticipationRecord;
            War -= archeryRecord.War;
            Demo -= archeryRecord.Demo;
            for (int i = archeryRecord.BowmanParticipations; i >0; i--)
            {
                if (BowmanParticipations > 0)
                    BowmanParticipations--;
                else if (HuntsmanParticipations > 0)
                    HuntsmanParticipations--;
                else
                    BowmanParticipations--;
            }
            for (int i = archeryRecord.BowmanWins; i >0; i--)
            {
                if (BowmanWins > 0)
                    BowmanWins--;
                else if (HuntsmanWins > 0)
                    HuntsmanWins--;
                else
                    BowmanWins--;
            }

            HuntsmanParticipations -= archeryRecord.HuntsmanParticipations;
            HuntsmanWins -= archeryRecord.HuntsmanWins;
        }

        public override ParticipationRecord Photocopy()
        {
            return new ArcheryParticipationRecord()
            {
                BowmanParticipations = this.BowmanParticipations,
                BowmanWins = this.BowmanWins,
                Demo = this.Demo,
                HuntsmanParticipations = this.HuntsmanParticipations,
                HuntsmanWins = this.HuntsmanWins,
                War = this.War
            };
        }
    }
}
