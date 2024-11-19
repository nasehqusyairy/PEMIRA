using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.Requests;
using PEMIRA.Services;
using PEMIRA.ViewModels;

namespace PEMIRA.Controllers
{
    [Authorize(Policy = "Admin")]
    public class CandidateController : BaseController
    {
        public IActionResult Index(CandidateViewModel input)
        {
            input.LimitEntry = TableHelper.SetLimitEntry(input.LimitEntry);

            CandidateService service = new(_context, input.LimitEntry);

            TableHelper.SetTableViewModel(service, input);
            return View(input);
        }
        public IActionResult Create()
        {
            return ViewCreatePage(new CandidateViewModel());
        }

        private ViewResult ViewCreatePage(CandidateViewModel input)
        {
            return View("Create", input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CandidateViewModel input)
        {
            CandidateService service = new(_context);
            CandidateRequest requestValidator = new(ModelState, input, service);

            if (!requestValidator.Validate())
            {
                return ViewCreatePage(input);
            }
            Candidate candidate = ModelHelper.MapProperties<CandidateViewModel, Candidate>(input);
            service.Store(candidate, UserId);
            TempData["SuccessMessage"] = "Kandidat berhasil ditambahkan";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(long id)
        {
            CandidateService service = new(_context);
            Candidate? candidate = service.GetCandidate(id);
            if (candidate == null)
            {
                return NotFound();
            }

            CandidateViewModel input = ModelHelper.MapProperties<Candidate, CandidateViewModel>(candidate);
            return View("Edit", input);
        }

        private IActionResult ViewEditPage(CandidateViewModel input)
        {
            return Edit(input.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CandidateViewModel input)
        {
            CandidateService service = new(_context);
            CandidateRequest requestValidator = new(ModelState, input, service);

            if (!requestValidator.Validate())
            {
                return ViewEditPage(input);
            }

            Candidate? candidate = service.GetCandidate(input.Id);
            if (candidate == null)
            {
                return NotFound();
            }

            service.Update(input, candidate, UserId);
            TempData["SuccessMessage"] = "Kandidat berhasil diubah";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(long id)
        {
            CandidateService service = new(_context);
            Candidate? candidate = service.GetCandidate(id);
            if (candidate == null)
            {
                return NotFound();
            }

            service.SoftDelete(candidate, UserId);
            TempData["SuccessMessage"] = "Kandidat berhasil dihapus";
            return RedirectToAction("Index");
        }
    }

}
