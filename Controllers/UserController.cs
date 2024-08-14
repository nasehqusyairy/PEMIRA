using Microsoft.AspNetCore.Mvc;
using PEMIRA.ViewModels;
using PEMIRA.Services;
using PEMIRA.Requests;
namespace PEMIRA.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {

            return View(new UserViewModel()
            {
                Users = new UserService(_context).GetUsers()
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(UserViewModel input)
        {
            UserService service = new(_context);
            UserRequest requestValidator = new(ModelState, input, service);
            if (!requestValidator.Validate())
            { 
                input.Users = new UserService(_context).GetUsers();
                return View("Index", input);
            }
            service.Store(requestValidator.DerivedData["User"]);
            return RedirectToAction("Index");
        }


    }
}
