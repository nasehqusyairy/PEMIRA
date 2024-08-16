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
        new() { Name = "Guru" },
        new() { Name = "SMP" },
        new() { Name = "SMA" },
        new() { Name = "SMK" },
        new() { Name = "Putra" },
        new() { Name = "Putri" },
      ];

      // buat pengulangan untuk menambahkan 150 data
      // List<Tag> tags = [];
      // for (int i = 0; i < 150; i++)
      // {
      //   tags.Add(new Tag() { Name = "Tag " + i });
      // }

      DBContext.Tags.AddRange(tags);
      DBContext.SaveChanges();
    }
  }
}