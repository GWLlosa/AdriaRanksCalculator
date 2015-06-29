using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib.Ministry
{
    public class MinistryParticipationRecord : ParticipationRecord
    {

        public int Participation { get; set; }
        public int War { get; set; }
        public int Demo { get; set; }
        public int DemoInitiation { get; set; }

        public override void Deduct(ParticipationRecord ParticipationRecord)
        {
            MinistryParticipationRecord minRecord = (MinistryParticipationRecord)ParticipationRecord;
            Participation -= minRecord.Participation;
            War -= minRecord.War;
            Demo -= minRecord.Demo;
            DemoInitiation -= minRecord.DemoInitiation;
        }

        public override ParticipationRecord Photocopy()
        {
            return new MinistryParticipationRecord()
            {
                Demo = this.Demo,
                DemoInitiation = this.DemoInitiation,
                Participation = this.Participation,
                War = this.War
            };
        }
    }
}
