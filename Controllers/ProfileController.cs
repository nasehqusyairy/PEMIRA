using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
      long id = Convert.ToInt64(Cookie.FindFirst("UserId")?.Value);
      ProfileViewModel model = ModelHelper.MapProperties<User, ProfileViewModel>(new ProfileService(_context).GetUser(id));
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