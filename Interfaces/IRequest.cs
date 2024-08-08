
using PEMIRA.Models;

namespace PEMIRA.Interfaces;

public interface IRequest<T>
{
  DatabaseContext _context { get; set; }

  T Input { get; set; }

  List<string> GetErrorMessages();
}