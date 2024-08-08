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

            List<User> users = [.. DBContext.Users];
            List<Role> roles = [.. DBContext.Roles];
            List<Election> elections = [.. DBContext.Elections];

            List<RoleUser> items = [
                new() {
                    UserId = users[11].Id,
                    RoleId = roles[3].Id,
                    ElectionId = elections[0].Id
                },
                new() {
                    UserId = users[11].Id,
                    RoleId = roles[2].Id,
                    ElectionId = elections[1].Id
                },
            ];

            DBContext.RoleUsers.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
