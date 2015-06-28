using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdriaScorer.Lib
{
    public class ArtsParticipationRecord : ParticipationRecord
    {
        public int JourneymanListParticipations { get; set; }

        public int JourneymanListWins { get; set; }

        public int KnightsListParticipations { get; set; }

        public int KnightsListWins { get; set; }

        public int MasterworksMade { get; set; }

        public int WarParticipations { get; set; }

        public int DemoParticipations { get; set; }

        public override void Deduct(ParticipationRecord ParticipationRecord)
        {
            ArtsParticipationRecord artsRecord = (ArtsParticipationRecord)ParticipationRecord;
            DemoParticipations -= artsRecord.DemoParticipations;
            WarParticipations -= artsRecord.WarParticipations;

            for (int i = artsRecord.JourneymanListParticipations; i > 0; i--)
            {
                if (JourneymanListParticipations > 0)
                    JourneymanListParticipations--;
                else if (KnightsListParticipations > 0)
                    KnightsListParticipations--;
                else
                    JourneymanListParticipations--;
            }

            for (int i = artsRecord.JourneymanListWins; i >0; i--)
            {
                if (JourneymanListWins > 0)
                    JourneymanListWins--;
                else if (KnightsListWins > 0)
                    KnightsListWins--;
                else
                    JourneymanListWins--;
            }

            KnightsListParticipations -= artsRecord.KnightsListParticipations;
            KnightsListWins -= artsRecord.KnightsListWins;
            MasterworksMade -= artsRecord.MasterworksMade;
        }

        public override ParticipationRecord Photocopy()
        {
            return new ArtsParticipationRecord()
            {
                DemoParticipations = this.DemoParticipations,
                JourneymanListParticipations = this.JourneymanListParticipations,
                JourneymanListWins = this.JourneymanListWins,
                KnightsListParticipations = this.KnightsListParticipations,
                KnightsListWins = this.KnightsListWins,
                MasterworksMade = this.MasterworksMade,
                WarParticipations = this.WarParticipations
            };
        }
    }
}
