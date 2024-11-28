using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Models;
using PEMIRA.Services;
using PEMIRA.ViewModels;

namespace PEMIRA.Controllers
{
    public class MonitoringController : BaseController
    {
        [Authorize(Policy = "Admin")]
        public IActionResult Index()
        {
            MonitoringService service = new(_context);

            List<Election> elections = service.GetElections();
            elections.Insert(0, new Election { Id = 0, Name = "Pilih Pemilihan" });

            string? selectedElectionId = Cookie.FindFirst("ElectionId")?.Value;

            MonitoringViewModel model = new()
            {
                ElectionId = selectedElectionId,
                Elections = new SelectList(elections, "Id", "Name", selectedElectionId)
            };

            return View(model);
        }

    }
}
