using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
  public class AuthViewModel
  {

    public string Code { get; set; } = "";

    public string Password { get; set; } = "";

    public List<string> ErrorMessages { get; set; } = [];

    public long ElectionId { get; set; } = 0;

    public List<Election> GetElections()
    {
      return new DatabaseContext().Elections.ToList();
    }

  }
}