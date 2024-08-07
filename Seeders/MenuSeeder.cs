using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
  public class MenuSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
  {
    public override Type Model => typeof(Menu);

    public override void Seed()
    {
      if (DBContext.Menus.Any()) return;

      List<Menusegment> menusegments = [.. DBContext.Menusegments];
      List<Menu> menus = [
        new() {
          Name = "Home Page",
          Icon = "house",
          MenusegmentId = menusegments[0].Id
        },
        new() { Name = "Menu 2", MenusegmentId = menusegments[0].Id },
        new() { Name = "Menu 3", MenusegmentId = menusegments[1].Id }
      ];
      DBContext.Menus.AddRange(menus);
      DBContext.SaveChanges();
    }
  }
}