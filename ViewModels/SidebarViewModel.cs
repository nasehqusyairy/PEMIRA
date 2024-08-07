using Microsoft.AspNetCore.Mvc.RazorPages;
using PEMIRA.Models;
using PEMIRA.Services;

namespace PEMIRA.ViewModels;

public class SidebarViewModel
{

  public SidebarViewModel()
  {
    _menuService = new MenuService(new DatabaseContext());
    AdminMenus = _menuService.GetMenusByRole("Admin");
  }
  private readonly MenuService _menuService;

  public List<Menu> AdminMenus { get; set; }

}