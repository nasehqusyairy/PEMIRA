using Microsoft.AspNetCore.Mvc;
using PEMIRA.Requests;
using PEMIRA.ViewModels;
using PEMIRA.Services;
using System.Security.Claims;

namespace PEMIRA.Controllers;
public class AuthController : BaseController
{

    public IActionResult Index() => View(new AuthViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(AuthViewModel input)
    {
        AuthService service = new(_context);
        AuthRequest requestValidator = new(input, service);

        List<string> errorMessages = requestValidator.GetErrorMessages();
        if (errorMessages.Count > 0)
        {
            TempData["ErrorMessages"] = errorMessages;
            return View("Index", input);
        }

        HttpContext.Session.SetString("Code", input.Code);
        long RoleId = requestValidator.ValidatedData.Id == 1 ? 1 :
              requestValidator.ValidatedData.RoleUsers.FirstOrDefault()?.RoleId ?? 4;
        List<Claim> claims = [
                    new Claim(ClaimTypes.Name, requestValidator.ValidatedData.Name),
                    new Claim(ClaimTypes.Role, RoleId.ToString())];
        return RedirectToAction("Index", "Home");
    }

    public ActionResult Logout()
    {
        HttpContext.Session.Remove("code");
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Auth"); // Redirect to login page after logout
    }
}