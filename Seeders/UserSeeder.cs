using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
    public class UserSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
    {
        public override Type Model => typeof(User);

        public override void Seed()
        {
            if (DBContext.Users.Any()) return;

            List<User> items = new List<User>
            {
                new User() { }
            };
            DBContext.Users.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
