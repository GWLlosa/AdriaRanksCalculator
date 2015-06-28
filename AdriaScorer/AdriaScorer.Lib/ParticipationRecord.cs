using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdriaScorer.Lib
{
    public abstract class ParticipationRecord
    {
        public abstract void Deduct(ParticipationRecord ParticipationRecord);
        public abstract ParticipationRecord Photocopy();
    }
}
