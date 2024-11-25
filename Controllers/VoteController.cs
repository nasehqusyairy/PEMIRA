using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Models;
using PEMIRA.Services;
using PEMIRA.ViewModels;

namespace PEMIRA.Controllers
{
    [Authorize]
    public class VoteController : BaseController
    {

        public IActionResult Index()
        {
            List<Election> elections = new VoteService(_context).GetElections();
            elections.Insert(0, new Election { Id = 0, Name = "Pilih Pemilihan" });
            string? selectedElectionId = Cookie.FindFirst("ElectionId")?.Value;
            VoteViewModel model = new()
            {
                ElectionId = selectedElectionId,
                Elections = new SelectList(elections, "Id", "Name", selectedElectionId)
            };
            return View(model);
        }

        public IActionResult ChangeElection(long ElectionId)
        {
            return RedirectToAction("Index");
        }

    }
}
