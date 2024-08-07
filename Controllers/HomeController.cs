using Microsoft.AspNetCore.Mvc;
using PEMIRA.Models;
using PEMIRA.Requests;
using PEMIRA.Services;
using System.Diagnostics;
using PEMIRA.ViewModels;


namespace PEMIRA.Controllers
{
	public class HomeController : Base
	{
		public IActionResult Index() => View();

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
