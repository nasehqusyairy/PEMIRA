using Microsoft.AspNetCore.Mvc;
using PEMIRA.Models;

namespace PEMIRA.Controllers
{
	public class BaseController : Controller
	{
		protected readonly DatabaseContext _context;

		public BaseController()
		{
			_context = new DatabaseContext();
		}
	}
}
