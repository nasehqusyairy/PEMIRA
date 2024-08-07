using Microsoft.EntityFrameworkCore;
using PEMIRA.Models;

namespace PEMIRA.Services;

public class MenuService(DatabaseContext context)
{
  private readonly DatabaseContext _context = context;

  public List<Menu> GetMenusByRole(string roleName) => [.. _context.Menus
    .Include(m => m.Menusegment)
    .Include(m => m.MenuRoles)
      .ThenInclude(mr => mr.Role)
    .Where(m => m.MenuRoles.Any(r => r.Role.Name == roleName))
  ];
}