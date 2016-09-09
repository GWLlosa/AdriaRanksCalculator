using AdriaScorer.Lib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    class ParticipantContext : DbContext
    {
        public ParticipantContext()
            :base("Server=tcp:adriascorer.database.windows.net,1433;Initial Catalog=AdriaScorer.Database;Persist Security Info=False;User ID=gwllosa;Password=AccessCode!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
        {

        }
        public DbSet<Participant> Participants { get; set; }
    }
}
