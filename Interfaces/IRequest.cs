
using PEMIRA.Models;

namespace PEMIRA.Interfaces;

public interface IRequest<T>
{
  DatabaseContext DBContext { get; set; }

  T UserInput { get; set; }

  List<string> GetErrorMessages();
}