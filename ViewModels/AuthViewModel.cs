using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
  public class AuthViewModel : User
  {
    public long ElectionId { get; set; } = 0;

    public string? ReturnUrl { get; set; }

    public List<Election> GetElections() => [.. new DatabaseContext().Elections];

  }
}