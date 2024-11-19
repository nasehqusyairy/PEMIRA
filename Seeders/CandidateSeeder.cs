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

            List<Candidate> items =
            [
                new() {
                    UserId = 12,
                    ElectionId = 1,
                    Color="982578"
                 }
            ];
            DBContext.Candidates.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
