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

            List<User> items = [
                new() {
                    Name = "Admin",
                    Code = "70536",
                    Password = "password",
                    Gender = true
                }
            ];

            DBContext.Users.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
