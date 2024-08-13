using PEMIRA.Models;
using PEMIRA.Services;
using System.Security.Claims;

namespace PEMIRA.ViewModels;

public class SidebarViewModel
{
  public SidebarViewModel(ClaimsPrincipal Cookie)
  {
    SidebarService service = new(Cookie);
    Menus = service.GetMenusByRole();
    UserName = service.Getusername();
    ProfilePicture = service.GetProfilePicture();
  }

  public List<Menu> Menus { get; set; }

  public string UserName { get; set; }

  public string ProfilePicture { get; set; }

}