
using PEMIRA.Models;

namespace PEMIRA.Interfaces;

public interface ISeeder
{
  DatabaseContext DBContext { get; set; }
  void Seed();
  void Revert();
}