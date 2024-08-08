using Microsoft.AspNetCore.Mvc;
using PEMIRA.Models;
using PEMIRA.Requests;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace PEMIRA.Controllers;
public class AuthController : BaseController
{
    public IActionResult Index() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(User input)
    {
        var errorMessages = new AuthRequest(_context, input).GetErrorMessages();
        if (errorMessages.Count > 0)
        {
            TempData["ErrorMessages"] = errorMessages;
            return View("Index", input);
        }
        HttpContext.Session.SetString("code", input.Code);
        var user = _context.Users.Where(x => x.Code == input.Code).FirstOrDefault();
        List <RoleUser> checkdataList = _context.RoleUsers.Include(x => x.User).Include(x => x.Role).Where(x => x.UserId == user.Id).ToList();
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name)
        };
        foreach (var checkdatas in checkdataList)
        {
            claims.Add(new Claim(ClaimTypes.Role, checkdatas.Role.Name));
        }
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Remove("code");
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Auth"); // Redirect to login page after logout
    }
}