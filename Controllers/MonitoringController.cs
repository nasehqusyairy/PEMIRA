using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PEMIRA.Controllers
{
    public class MonitoringController : BaseController
    {
        [Authorize]
        public IActionResult Index() => View();

    }
}
