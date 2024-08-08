using Microsoft.AspNetCore.Mvc;
using PEMIRA.Requests;
using PEMIRA.ViewModels;
using PEMIRA.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace PEMIRA.Controllers;
public class AuthController : BaseController
{

    public IActionResult Index() => (User?.Identity?.IsAuthenticated == true) ? RedirectToAction("Index", "Home") : View(new AuthViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(AuthViewModel input)
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
        List<Claim> claims = [
            new Claim(ClaimTypes.Name, requestValidator.ValidatedData.User.Name),
            new Claim(ClaimTypes.Role, requestValidator.ValidatedData.RoleId.ToString())
        ];

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Home");
    }

    public ActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Remove("code");
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Auth"); // Redirect to login page after logout
    }
}