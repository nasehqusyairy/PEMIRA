using Microsoft.AspNetCore.Mvc;

namespace PEMIRA.Controllers
{
    public class UserController : Base
    {
        public IActionResult Index() => View();
        
    }
}
