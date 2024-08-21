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

    public IActionResult Index() => (Cookie.Identity?.IsAuthenticated == true) ? RedirectToAction("Index", "Home") : View(new AuthViewModel()
    {
        Elections = new AuthService(_context).GetElections()
    });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(AuthViewModel input)
    {
        AuthService service = new(_context);
        AuthRequest requestValidator = new(ModelState, input, service);

        if (!requestValidator.Validate())
        {
            input.Elections = service.GetElections();
            return View("Index", input);
        }

        Dictionary<string, dynamic> derivedData = requestValidator.DerivedData;

        List<Claim> claims = [
            new Claim("ElectionId", input.ElectionId.ToString()),
            new Claim("UserId",derivedData["UserId"].ToString() ),
            new Claim("RoleId", derivedData["RoleId"].ToString())
        ];

        ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, "RoleId");
        ClaimsPrincipal principal = new(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        if (input.ReturnUrl != null)
        {
            return Redirect(input.ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Auth"); // Redirect to login page after logout
    }
}