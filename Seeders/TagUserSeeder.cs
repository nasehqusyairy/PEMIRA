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
            List<User> UserList = [.. DBContext.Users];
            List<Tag> TagList = [.. DBContext.Tags];
            List<TagUser> items = [
                new () 
                { 
                    UserId = 12,
                    TagId = 1,

                },
                new()
                {
                    UserId = 12,
                    TagId = 4,
                }
            ];
            DBContext.TagUsers.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
