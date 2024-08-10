using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PEMIRA.ViewModels;
using PEMIRA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace PEMIRA.Controllers
{
	public class HomeController : BaseController
	{

		[Authorize]
		public IActionResult Index()
		{
			long id = Convert.ToInt64(_cookie.FindFirst("UserId")?.Value);
			User user = new DatabaseContext().Users.First(u => u.Id == id);

			return View(user);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
