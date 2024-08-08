using Microsoft.AspNetCore.Mvc;

namespace PEMIRA.Controllers
{
    public class VoteController : Base
    {
        public IActionResult Index() => View();
        
    }
}
