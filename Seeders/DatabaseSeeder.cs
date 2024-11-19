using System.Collections.Generic;
using System.Linq;
using PEMIRA.Interfaces;
using PEMIRA.Models;

namespace PEMIRA.Seeders
{
  public class DatabaseSeeder : ISeeder
  {

    public DatabaseSeeder()
    {
      DBContext = new DatabaseContext();
      Seeders = [
        new TagSeeder(DBContext),
        new RoleSeeder(DBContext),
        new MenusegmentSeeder(DBContext),
        new MenuSeeder(DBContext),
        new MenuRoleSeeder(DBContext),
        new UserSeeder(DBContext),
        new TagUserSeeder(DBContext),
        new ElectionSeeder(DBContext),
        new RoleUserSeeder(DBContext),
        new CandidateSeeder(DBContext),
      ];
    }
    private readonly List<ISeeder> Seeders;

    public DatabaseContext DBContext { get; set; }

    public void Seed()
    {
      Seeders.ForEach(seeder => seeder.Seed());
    }

    public void Revert()
    {
      Seeders.ForEach(seeder => { seeder.Revert(); });
    }

  }
}