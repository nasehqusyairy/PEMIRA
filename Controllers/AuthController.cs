using Microsoft.AspNetCore.Mvc;
using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.Requests;

namespace PEMIRA.Controllers;

public class AuthController : BaseController
{
  public IActionResult Index() => View();

  [HttpPost]
  public IActionResult Login(User input)
  {
    if (new AuthRequest(_context, ModelState, input).Validate())
    {
      TempData["ErrorMessages"] = RequestHelper.GetErrorMessages(ModelState);
      return View("Index", input);
    }

    return RedirectToAction("Index", "Home");
  }

  public IActionResult Logout()
  {
    return RedirectToAction("Index");
  }
}