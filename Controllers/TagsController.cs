using Microsoft.AspNetCore.Mvc;
using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.Requests;
using PEMIRA.Services;
using PEMIRA.ViewModels;

namespace PEMIRA.Controllers
{
    public class TagsController : BaseController
    {
        public IActionResult Index()
        {
            return View(new TagsViewModel()
            {
                Tags = new TagService(_context).GetTags()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(TagsViewModel input)
        {
            TagService service = new(_context);
            TagsRequest requestValidator = new(ModelState, input, service);

            if (!requestValidator.Validate())
            {
                input.Tags = service.GetTags();
                return View("Index", input);
            }

            service.Store(requestValidator.DerivedData["Tag"]);

            return RedirectToAction("Index");
        }

    }
}
