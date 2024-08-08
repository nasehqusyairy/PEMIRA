using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
    public class TagUserSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
    {
        public override Type Model => typeof(TagUser);

        public override void Seed()
        {
            if (DBContext.TagUsers.Any()) return;

            List<TagUser> items = new List<TagUser>
            {
                new TagUser() { }
            };
            DBContext.TagUsers.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
