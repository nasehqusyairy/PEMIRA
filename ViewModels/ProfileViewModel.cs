using PEMIRA.Models;

namespace PEMIRA.ViewModels;

public class ProfileViewModel : User
{
  public ProfileViewModel(User user)
  {
    Id = user.Id;
    Code = user.Code;
    Name = user.Name;
    Password = user.Password;
    Gender = user.Gender;
  }
  public string? ConfirmPassword { get; set; }
}