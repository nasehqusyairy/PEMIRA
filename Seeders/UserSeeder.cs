using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
    public class UserSeeder(DatabaseContext context) : TableSeeder(context)
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
                    Gender = false
                }
            ];

            for (int i = 0; i < 10; i++)
            {
                items.Add(new User
                {
                    Name = Faker.Name.FullName(),
                    Code = Faker.RandomNumber.Next(10000, 99999).ToString(),
                    Password = "password",
                    Gender = Faker.RandomNumber.Next(0, 1) == 0
                });
            }

            items.Add(new User
            {
                Name = "Test User 1",
                Code = "70537",
                Password = "password",
                Gender = false
            });

            DBContext.Users.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
