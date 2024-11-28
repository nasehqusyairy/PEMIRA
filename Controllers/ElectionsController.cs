using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEMIRA.Helpers;
using PEMIRA.Migrations;
using PEMIRA.Models;
using PEMIRA.Requests;
using PEMIRA.Services;
using PEMIRA.ViewModels;

namespace PEMIRA.Controllers
{
    [Authorize(Policy = "Admin")]
    public class ElectionsController : BaseController
    {
        public IActionResult Index(ElectionViewModel input)
        {
            input.LimitEntry = TableHelper.SetLimitEntry(input.LimitEntry);

            ElectionService service = new(_context, input.LimitEntry);

            TableHelper.SetTableViewModel(service, input);

            return View("Index", input);
        }
        public IActionResult Create()
        {
            return ViewCreatePage(new ElectionViewModel());
        }

        private ViewResult ViewCreatePage(ElectionViewModel input)
        {
            return View("Create", input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ElectionViewModel input)
        {
            ElectionService service = new(_context);
            ElectionRequest requestValidator = new(ModelState, input, service);

            if (!requestValidator.Validate())
            {
                return ViewCreatePage(input);
            }
            Election election = ModelHelper.MapProperties<ElectionViewModel, Election>(input);
            service.Store(election, UserId);
            TempData["SuccessMessage"] = "Penanda berhasil ditambahkan";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(long id)
        {
            ElectionService service = new(_context);
            Election? election = service.GetElection(id);
            if (election == null)
            {
                return NotFound();
            }

            ElectionViewModel input = ModelHelper.MapProperties<Election, ElectionViewModel>(election);
            return View("Edit", input);
        }

        private IActionResult ViewEditPage(ElectionViewModel input)
        {
            return Edit(input.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ElectionViewModel input)
        {
            ElectionService service = new(_context);
            ElectionRequest requestValidator = new(ModelState, input, service);

            if (!requestValidator.Validate())
            {
                return ViewEditPage(input);
            }

            Election? election = service.GetElection(input.Id);
            if (election == null)
            {
                return NotFound();
            }

            service.Update(input, election, UserId);
            TempData["SuccessMessage"] = "Pemilihan berhasil diubah";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(long id)
        {
            ElectionService service = new(_context);
            Election? election = service.GetElection(id);
            if (election == null)
            {
                return NotFound();
            }

            service.SoftDelete(election, UserId);
            TempData["SuccessMessage"] = "Pemilihan berhasil dihapus";
            return RedirectToAction("Index");
        }

        public IActionResult Users(long id, ElectionUserViewModel input)
        {
            ElectionService service = new(_context);
            Election? election = service.GetElection(id);
            if (election == null)
            {
                return NotFound();
            }
            input.LimitEntry = TableHelper.SetLimitEntry(input.LimitEntry);
            input.ElectionId = id;
            ElectionUserService electionuserservice = new(_context, id, input.LimitEntry, input.SelectedTags);

            TableHelper.SetTableViewModel(electionuserservice, input);
            input.TagUsers = electionuserservice.GetTagUsers();
            input.Tags = electionuserservice.GetTags();
            return View(input);
        }


        public IActionResult AddParticipant(long id, AddParticipantViewModel input)
        {
            ElectionService service = new(_context);
            input.LimitEntry = TableHelper.SetLimitEntry(input.LimitEntry);
            UserService userService = new(_context, input.LimitEntry, input.SelectedTags);
            Election? election = service.GetElection(id);
            if (election == null)
            {
                return NotFound();
            }
            TableHelper.SetTableViewModel(userService, input);
            input.TagUsers = userService.GetTagUsers();
            input.Tags = userService.GetTags();
            input.ElectionId = id;
            input.Participants = service.GetParticipantIds(id);
            return View(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StoreParticipant(long ElectionId, long[] selectedUsers)
        {
            ElectionUserService service = new(_context, ElectionId);
            service.AddParticipants(selectedUsers);
            TempData["SuccessMessage"] = "Peserta berhasil ditambahkan";
            return RedirectToAction("Users", new { id = ElectionId });
        }

        public IActionResult RemoveParticipant(long id, long ElectionId)
        {
            ElectionUserService service = new(_context, ElectionId);
            service.RemoveParticipant(id);
            TempData["SuccessMessage"] = "Peserta berhasil dihapus";
            return RedirectToAction("Users", new { id = ElectionId });
        }
    }

}

