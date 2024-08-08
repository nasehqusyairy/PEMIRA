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

            List<ElectionUser> items = new List<ElectionUser>
            {
                new ElectionUser() { }
            };
            DBContext.ElectionUsers.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
