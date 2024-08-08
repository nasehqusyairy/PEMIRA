using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
    public class RoleUserSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
    {
        public override Type Model => typeof(RoleUser);

        public override void Seed()
        {
            if (DBContext.RoleUsers.Any()) return;

            List<Role> roles = [.. DBContext.Roles];
            List<User> users = [.. DBContext.Users];
            List<RoleUser> items = [
                new() {
                    RoleId = roles[0].Id,
                    UserId = users[0].Id
                }
            ];

            for (int i = 0; i < 10; i++)
            {
                items.Add(new RoleUser
                {
                    RoleId = roles[3].Id,
                    UserId = users[i + 1].Id
                });
            }
            DBContext.RoleUsers.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
