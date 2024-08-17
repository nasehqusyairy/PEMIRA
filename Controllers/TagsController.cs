using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.Requests;
using PEMIRA.Services;
using PEMIRA.ViewModels;

namespace PEMIRA.Controllers
{
    [Authorize]
    public class TagsController : BaseController
    {
        public ViewResult Index(TagsViewModel input)
        {
            input.LimitEntry = TableHelper.SetLimitEntry(input.LimitEntry);

            TagService service = new(_context, input.LimitEntry);

            TableHelper.SetTableViewModel(service, input);
            return View("Index", input);
        }

        public IActionResult Create()
        {
            return ViewCreatePage(new TagsViewModel());
        }

        private ViewResult ViewCreatePage(TagsViewModel input)
        {
            return View("Create", input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TagsViewModel input)
        {
            TagService service = new(_context);
            TagsRequest requestValidator = new(ModelState, input, service);

            if (!requestValidator.Validate())
            {
                return ViewCreatePage(input);
            }
            Tag tag = ModelHelper.MapProperties<TagsViewModel, Tag>(input);
            service.Store(tag, UserId);
            TempData["SuccessMessage"] = "Penanda berhasil ditambahkan";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(long id)
        {
            TagService service = new(_context);
            Tag? tag = service.GetTag(id);
            if (tag == null)
            {
                return NotFound();
            }

            TagsViewModel input = ModelHelper.MapProperties<Tag, TagsViewModel>(tag);
            return View("Edit", input);
        }

        private IActionResult ViewEditPage(TagsViewModel input)
        {
            return Edit(input.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TagsViewModel input)
        {
            TagService service = new(_context);
            TagsRequest requestValidator = new(ModelState, input, service);

            if (!requestValidator.Validate())
            {
                return ViewEditPage(input);
            }

            Tag? tag = service.GetTag(input.Id);
            if (tag == null)
            {
                return NotFound();
            }

            service.Update(input, tag, UserId);
            TempData["SuccessMessage"] = "Penanda berhasil diubah";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long id)
        {
            TagService service = new(_context);
            Tag? tag = service.GetTag(id);
            if (tag == null)
            {
                return NotFound();
            }

            service.SoftDelete(tag, UserId);
            TempData["SuccessMessage"] = "Penanda berhasil dihapus";
            return RedirectToAction("Index");
        }

    }
}
