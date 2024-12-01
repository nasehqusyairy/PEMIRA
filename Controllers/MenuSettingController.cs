using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.Services;
using PEMIRA.ViewModels;

namespace PEMIRA.Controllers
{
    public class MenuSettingController : BaseController
    {
        public IActionResult Index(MenuViewModel input)
        {
            MenuService service = new(_context, input.LimitEntry);
            TableHelper.SetTableViewModel(service, input);
            return View(input);
        }
        public IActionResult MenuRole(MenuRoleViewModel input, long id)
        {
            MenuRoleService service = new(_context, input.LimitEntry, id);
            input.Roles = service.GetRoles();
            if (input.SelectedId> 0)
            {
                if (!service.IsRoleExist(id, input.SelectedId))
                {
                    service.AddMenuRole(id, input.SelectedId);
                    TempData["SuccessMessage"] = "Peran Telah Ditambahkan";
                }
                else
                {
                    TempData["ErrorMessage"] = "Peran Sudah Ada di dalam Menu";
                }
            }
            TableHelper.SetTableViewModel(service, input);
            return View(input);
        }

    }
}
