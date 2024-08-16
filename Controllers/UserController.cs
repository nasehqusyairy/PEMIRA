using Microsoft.AspNetCore.Mvc;
using PEMIRA.ViewModels;
using PEMIRA.Services;
using PEMIRA.Requests;
using PEMIRA.Helpers;
using PEMIRA.Models;
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
        
        public IActionResult Create()
        {
            return View("_Create", new UserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel input)
        {
            UserService service = new(_context);
            UserRequest requestValidator = new(ModelState, input, service);
            if (!requestValidator.Validate())
            {
                input.Users = new UserService(_context).GetUsers();
                return View("_Create", input);
            }
            service.Store(requestValidator.DerivedData["User"]);
            return RedirectToAction("Index");
        }
       
        public IActionResult Edit(long id)
        {
            UserViewModel model = ModelHelper.MapProperties<User, UserViewModel>(new UserService(_context).GetUserById(id));
            return View("_Update", model);
        }
        public IActionResult Update(UserViewModel input) 
        {
        UserService service = new(_context);
            UserRequest requestValidator = new(ModelState, input, service);
            if (!requestValidator.Validate()) 
            {
                input.Users = new UserService(_context).GetUsers();
                return View("_Update", input);
            }
            service.Update(input);
            return RedirectToAction("Index");
        }

    }
}
