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

    public IActionResult Index() => (_cookie.Identity?.IsAuthenticated == true) ? RedirectToAction("Index", "Home") : View(new AuthViewModel());

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

        List<Claim> claims = [
            // simpan id election ke cookie
            new Claim("ElectionId", input.ElectionId.ToString()),
            new Claim("UserId", requestValidator.ValidatedData.User.Id.ToString()),
            new Claim(ClaimTypes.Role, requestValidator.ValidatedData.RoleId.ToString())
        ];

        ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal principal = new(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        if (input.ReturnUrl != null)
        {
            return Redirect(input.ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    public ActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Auth"); // Redirect to login page after logout
    }
}