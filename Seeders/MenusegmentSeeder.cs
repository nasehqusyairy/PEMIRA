using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
  public class MenusegmentSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
  {
    public override Type Model => typeof(Menusegment);

    public override void Seed()
    {
      if (DBContext.Menusegments.Any()) return;

      List<Menusegment> menusegments = [
        new() { Name = "Pemira Bayhi" },
        new() { Name = "Administrator" },
        new() { Name = "Settings" }
      ];
      DBContext.Menusegments.AddRange(menusegments);
      DBContext.SaveChanges();
    }
  }
}