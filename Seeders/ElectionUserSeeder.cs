using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
    public class ElectionUserSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
    {
        public override Type Model => typeof(ElectionUser);

        public override void Seed()
        {
            if (DBContext.ElectionUsers.Any()) return;

            List<ElectionUser> electionusers =
            [
                new()
                {
                    UserId = 12,
                    ElectionId = 70536,
                },
                new()
                {
                    UserId = 2,
                    ElectionId = 70537,
                },
                new()
                {
                    UserId = 3,
                    ElectionId = 70537,
                }
            ];
            DBContext.ElectionUsers.AddRange(electionusers);
            DBContext.SaveChanges();
        }
    }
}
