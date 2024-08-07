using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
  public class MenuRoleSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
  {
    public override Type Model => typeof(MenuRole);

    public override void Seed()
    {
      if (DBContext.MenuRoles.Any()) return;

      List<Menu> menus = [.. DBContext.Menus];
      List<Role> roles = [.. DBContext.Roles];
      List<MenuRole> menuRoles = [
        new() { MenuId = menus[0].Id, RoleId = roles[0].Id },
        new() { MenuId = menus[1].Id, RoleId = roles[1].Id },
        new() { MenuId = menus[2].Id, RoleId = roles[1].Id }
      ];
      DBContext.MenuRoles.AddRange(menuRoles);
      DBContext.SaveChanges();
    }
  }
}