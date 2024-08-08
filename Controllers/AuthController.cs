using Microsoft.AspNetCore.Mvc;
using PEMIRA.Models;
using PEMIRA.Requests;

namespace PEMIRA.Controllers;

public class AuthController : BaseController
{
  public IActionResult Index() => View();

  [HttpPost]
  public IActionResult Login(User input)
  {
    var errorMessages = new AuthRequest(_context, input).GetErrorMessages();
    if (errorMessages.Count > 0)
    {
      TempData["ErrorMessages"] = errorMessages;
      return View("Index", input);
    }

    return RedirectToAction("Index", "Home");
  }

  public IActionResult Logout()
  {
    return RedirectToAction("Index");
  }
}