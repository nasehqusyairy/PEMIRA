using Microsoft.EntityFrameworkCore;
using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
  public class TagSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
  {
    public override Type Model => typeof(Tag);

    public override void Seed()
    {
      if (DBContext.Tags.Any()) return;

      List<Tag> tags = [
        new() { Name = "Technology" },
        new() { Name = "Science" },
        new() { Name = "Art" },
      ];

      DBContext.Tags.AddRange(tags);
      DBContext.SaveChanges();
    }
  }
}