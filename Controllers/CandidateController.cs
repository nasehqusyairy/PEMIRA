using Microsoft.AspNetCore.Mvc;

namespace PEMIRA.Controllers
{
    public class CandidateController : Base
    {
        public IActionResult Index() => View();
        
    }
}
