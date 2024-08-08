using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
    public class CandidateUserSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
    {
        public override Type Model => typeof(CandidateUser);

        public override void Seed()
        {
            if (DBContext.CandidateUsers.Any()) return;

            List<CandidateUser> items = new List<CandidateUser>
            {
                new CandidateUser() { }
            };
            DBContext.CandidateUsers.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
