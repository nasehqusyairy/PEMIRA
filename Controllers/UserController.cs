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
        public IActionResult Index(UserViewModel input)
        {
            input.LimitEntry = ModelHelper.SetLimitEntry(input.LimitEntry);

            UserService service = new (_context, input.LimitEntry);

            int pageCount = service.GetPageCount(input.Search ?? "");

            input.PageCount = pageCount;
            input.CurrentPage = ModelHelper.SetCurrentPage(input.CurrentPage, pageCount);

            input.Users = service.GetUsers(input.Search ?? "", input.CurrentPage, input.OrderBy, input.IsAsc);
            return View("Index", input);
        }

        public IActionResult Create()
        {
            return ViewCreatePage(new UserViewModel());
        }

        private ViewResult ViewCreatePage(UserViewModel input)
        {
            return View("Create", input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel input)
        {
            long id = Convert.ToInt64(Cookie.FindFirst("UserId")?.Value);
            UserService service = new(_context);
            UserRequest requestValidator = new(ModelState, input, service);
            if (!requestValidator.Validate())
            {
                return ViewCreatePage(input);
            }
            User user = ModelHelper.MapProperties<UserViewModel, User>(input);
            service.Store(user, id);
            TempData["SuccessMessage"] = "User berhasil ditambahkan";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(long id)
        {
            UserService service = new(_context);
            User? user = service.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            UserViewModel input = ModelHelper.MapProperties<User, UserViewModel>(user);
            return View("Edit", input);
        }

        private IActionResult ViewEditPage(UserViewModel input)
        {
            return Edit(input.Id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserViewModel input) 
        {
            long id = Convert.ToInt64(Cookie.FindFirst("UserId")?.Value);
            UserService service = new(_context);
            UserRequest requestValidator = new(ModelState, input, service);
            if (!requestValidator.Validate()) 
            {
                return ViewEditPage(input);
            }
            User? user = service.GetUserById(input.Id);
            if (user == null)
            {
                return NotFound();
            }
            service.Update(input, user, id);
            TempData["SuccessMessage"] = "Penanda berhasil diubah";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long id)
        {
            long idUser = Convert.ToInt64(Cookie.FindFirst("UserId")?.Value);
            UserService service = new(_context);
            User? user = service.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            service.SoftDelete(user, idUser);
            TempData["SuccessMessage"] = "Penanda berhasil dihapus";
            return RedirectToAction("Index");
        }

    }
}
