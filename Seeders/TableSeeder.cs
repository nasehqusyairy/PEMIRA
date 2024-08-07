using PEMIRA.Interfaces;
using PEMIRA.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace PEMIRA.Seeders;

public abstract class TableSeeder(DatabaseContext context) : ISeeder
{
  public abstract Type Model { get; }
  public DatabaseContext DBContext { get; set; } = context;

  public void Revert()
  {
    var tableProperty = DBContext.GetType().GetProperty(Model.Name + "s");
    if (tableProperty == null) return;

    if (tableProperty.GetValue(DBContext) is not IQueryable<object> table) return;

    DBContext.RemoveRange(table);
    DBContext.SaveChanges();

    // reset the auto increment
    string tableName = Model.Name;

    if (Regex.IsMatch(tableName, "(?<!^)([A-Z])")) tableName = Regex.Replace(tableName, "(?<!^)([A-Z])", "_$1").ToLower();
    else tableName = tableName.ToLower() + "s";

    DBContext.Database.ExecuteSqlRaw("ALTER TABLE `" + tableName + "` AUTO_INCREMENT = 1;");
    DBContext.SaveChanges();
  }

  public abstract void Seed();
}