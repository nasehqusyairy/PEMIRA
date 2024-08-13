using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PEMIRA.Models;

namespace PEMIRA.Services
{
  public class SidebarService
  {
    private readonly DatabaseContext _context;
    private readonly User _currentUser;

    private readonly long _roleId;
    public SidebarService(ClaimsPrincipal Cookie)
    {
      _context = new DatabaseContext();
      long UserId = Convert.ToInt64(Cookie.FindFirst("UserId")?.Value);
      _roleId = Convert.ToInt64(Cookie.FindFirst(ClaimTypes.Role)?.Value);
      _currentUser = _context.Users.First(u => u.Id == UserId);
    }

    public string Getusername()
    {
      string username = _currentUser.Name;

      // jika nama lebih dari 1 suku kata, ambil kata pertama + ...
      if (username.Split(' ').Length > 1)
      {
        username = username.Split(' ')[0] + "...";
      }

      // pastikan huruf kapital hanya di awal kata
      username = username.ToLower();
      username = char.ToUpper(username[0]) + username[1..];
      return username;

    }

    public string GetProfilePicture()
    {
      return _currentUser.Gender == true ? "/img/muslimah.png" : "/img/muslim.png";
    }

    public List<Menu> GetMenusByRole()
    {
      return [.. _context.Menus.Include(m => m.Menusegment).Include(m => m.MenuRoles).ThenInclude(mr => mr.Role).Where(m => m.MenuRoles.Any(r => r.Role.Id == _roleId))];
    }

  }
}