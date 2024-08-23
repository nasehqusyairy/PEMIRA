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

            List<User> users = [.. DBContext.Users];
            List<Tag> tags = [.. DBContext.Tags];

            List<TagUser> items = [];

            foreach (User user in users)
            {
                foreach (Tag tag in tags)
                {
                    items.Add(new TagUser
                    {
                        UserId = user.Id,
                        TagId = tag.Id
                    });
                }
            }

            DBContext.TagUsers.AddRange(items);
            DBContext.SaveChanges();
        }
    }
}
