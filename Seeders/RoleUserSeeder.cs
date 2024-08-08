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
            DBContext.RoleUsers.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
