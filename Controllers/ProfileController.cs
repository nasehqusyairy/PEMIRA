using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.Requests;
using PEMIRA.Services;
using PEMIRA.ViewModels;

namespace PEMIRA.Controllers
{
  [Authorize]
  public class ProfileController : BaseController
  {

    public IActionResult Index()
    {
      string userId = Cookie.FindFirst("UserId")?.Value ?? string.Empty;
      ProfileViewModel model = ModelHelper.MapProperties<User, ProfileViewModel>(new ProfileService(_context).GetUser(long.Parse(userId)));
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ProfileViewModel input)
    {
      ProfileService service = new(_context);
      ProfileRequest requestValidator = new(ModelState, input, service);
      if (!requestValidator.Validate())
      {
        return View(input);
      }

      service.UpdateUser(input);

      TempData["SuccessMessage"] = "Profil berhasil diperbarui";
      return RedirectToAction("Index");
    }
  }
}