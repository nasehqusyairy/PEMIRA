using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEMIRA.Models;
namespace PEMIRA.Controllers
{
	public class BaseController : Controller
	{
		protected readonly DatabaseContext _context;

		protected ClaimsPrincipal _cookie => HttpContext.User;

		public BaseController()
		{
			_context = new DatabaseContext();
		}
	}
}
