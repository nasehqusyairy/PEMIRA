using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
    public class PermissionUserSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
    {
        public override Type Model => typeof(PermissionUser);

        public override void Seed()
        {
            if (DBContext.PermissionUsers.Any()) return;

            List<PermissionUser> items = new List<PermissionUser>
            {
                new PermissionUser() { }
            };
            DBContext.PermissionUsers.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
