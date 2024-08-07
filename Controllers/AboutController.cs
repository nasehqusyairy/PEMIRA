using Microsoft.AspNetCore.Mvc;
using PEMIRA.Models;
using PEMIRA.Requests;
using PEMIRA.Services;
using System.Diagnostics;

namespace PEMIRA.Controllers
{
	public class AboutController : Base
	{
		public IActionResult Index() => View();
	}
}
