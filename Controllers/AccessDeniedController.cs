using Microsoft.AspNetCore.Mvc;

namespace PEMIRA.Controllers
{
    public class AccessDenied : BaseController
    {
        public IActionResult Index() => View();
    }
}
