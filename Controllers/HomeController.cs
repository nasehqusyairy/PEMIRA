using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PEMIRA.ViewModels;
using PEMIRA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace PEMIRA.Controllers
{
	public class HomeController : BaseController
	{
		[Authorize]
		public IActionResult Index()
		{
			string? code = HttpContext.Session.GetString("Code");
			//if (code == null)
			//{
			//	return RedirectToAction("Index", "Auth");
			//}
			User user = _context.Users
				.Where(u => u.Code == code)
				.Include(u => u.RoleUsers.Where(ru => ru.ElectionId == 2))
					.ThenInclude(ru => ru.Role)
				.First();

			return View(user);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
