using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
    public class PermissionSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
    {
        public override Type Model => typeof(Permission);

        public override void Seed()
        {
            if (DBContext.Permissions.Any()) return;

            List<Permission> items = new List<Permission>
            {
                new Permission() { }
            };
            DBContext.Permissions.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
