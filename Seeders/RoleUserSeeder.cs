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

            List<RoleUser> items = [

            ];

            DBContext.RoleUsers.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
