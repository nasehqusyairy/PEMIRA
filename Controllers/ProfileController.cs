using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEMIRA.Models;
using PEMIRA.ViewModels;

namespace PEMIRA.Controllers
{
  [Authorize]
  public class ProfileController : BaseController
  {

    public IActionResult Index()
    {
      string? userId = _cookie.FindFirst("UserId")?.Value;
      ProfileViewModel profile = new(_context.Users.Where(u => u.Id == int.Parse(userId!)).First());
      return View(profile);
    }
  }
}