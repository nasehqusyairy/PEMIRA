using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PEMIRA.Models;
namespace PEMIRA.Controllers
{
	public class BaseController : Controller
	{
		protected readonly DatabaseContext _context;

		protected ClaimsPrincipal Cookie => HttpContext.User;


		public BaseController()
		{
			_context = new DatabaseContext();
		}
	}
}
