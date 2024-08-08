using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEMIRA.Models;

namespace PEMIRA.Controllers
{
  public class ProfileController : Controller
  {

    public IActionResult Index()
    {
      return View();
    }
  }
}