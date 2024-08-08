
using PEMIRA.Models;

namespace PEMIRA.Interfaces;

public interface IRequest<T>
{

  T UserInput { get; set; }

  List<string> GetErrorMessages();
}