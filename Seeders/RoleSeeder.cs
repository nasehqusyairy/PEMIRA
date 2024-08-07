using Microsoft.EntityFrameworkCore;
using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
  public class RoleSeeder(DatabaseContext context) : TableSeeder(context), ISeeder
  {
    public override Type Model => typeof(Role);

    public override void Seed()
    {
      if (DBContext.Roles.Any()) return;

      List<Role> roles = [
        new() { Name = "Admin" },
        new() { Name = "User" }
      ];
      DBContext.Roles.AddRange(roles);
      DBContext.SaveChanges();
    }
  }
}