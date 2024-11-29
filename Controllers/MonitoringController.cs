using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Helpers;
using PEMIRA.Models;
using PEMIRA.Services;
using PEMIRA.ViewModels;

namespace PEMIRA.Controllers
{
    public class MonitoringController : BaseController
    {
        [Authorize(Policy = "Admin")]
        public IActionResult Index(MonitoringViewModel model)
        {
            string? selectedElectionId = Cookie.FindFirst("ElectionId")?.Value;
            MonitoringService service = new(_context, 10, selectedElectionId != null ? long.Parse(selectedElectionId) : 0);

            List<Election> elections = service.GetElections();
            elections.Insert(0, new Election { Id = 0, Name = "Pilih Pemilihan" });


            TableHelper.SetTableViewModel(service, model);

            model.ElectionId = selectedElectionId;
            model.Elections = new SelectList(elections, "Id", "Name", selectedElectionId);
            model.Election = service.GetElection();
            model.GolputUsersCount = service.GetGolputUsersCount();
            model.Tags = service.GetTags();

            return View(model);
        }

        public async Task<IActionResult> ChangeElection(long ElectionId)
        {
            if (User.Identity is not ClaimsIdentity identity)
            {
                return Unauthorized();
            }
            var existingClaim = identity.FindFirst("ElectionId");
            if (existingClaim != null)
            {
                identity.RemoveClaim(existingClaim);
            }
            identity.AddClaim(new Claim("ElectionId", ElectionId.ToString()));
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index");
        }

    }
}
