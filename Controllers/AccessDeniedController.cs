using Microsoft.AspNetCore.Mvc;
using PEMIRA.ViewModels;

namespace PEMIRA.Controllers
{
    public class AccessDenied : BaseController
    {
        public IActionResult Index(AccessDeniedViewModel input)
        {
            input.Title = input.ReturnUrl != null ? _context.Menus.FirstOrDefault(x => x.Url.Contains(input.ReturnUrl))?.Name ?? "Akses Ditolak" : input.Title;
            return View(input);
        }
    }
}
