using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
    public class CandidateSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
    {
        public override Type Model => typeof(Candidate);

        public override void Seed()
        {
            if (DBContext.Candidates.Any()) return;

            List<Candidate> items = new List<Candidate>
            {
                new Candidate() { }
            };
            DBContext.Candidates.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
