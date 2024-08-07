using Microsoft.AspNetCore.Mvc;
using PEMIRA.Models;

namespace PEMIRA.Controllers
{
	public class Base : Controller
	{
		protected readonly DatabaseContext _context;

		public Base()
		{
			_context = new DatabaseContext();
		}
	}
}
