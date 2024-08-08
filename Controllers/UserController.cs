using Microsoft.AspNetCore.Mvc;

namespace PEMIRA.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index() => View();

    }
}
