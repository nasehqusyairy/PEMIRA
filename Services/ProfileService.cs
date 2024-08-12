using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.ViewModels;

namespace PEMIRA.Services;

public class ProfileService(DatabaseContext context)
{
  private readonly DatabaseContext _context = context;
  public User GetUser(long id)
  {
    return _context.Users.First(u => u.Id == id);
  }

  public bool IsCodeUnique(string code, long id)
  {
    return _context.Users.FirstOrDefault(u => u.Code == code && u.Id != id) == null;
  }

  public void UpdateUser(ProfileViewModel input)
  {
    User user = _context.Users.First(u => u.Id == input.Id);
    ModelHelper.UpdateProperties(input, user);
    _context.Users.Update(user);
    _context.SaveChanges();
  }
}