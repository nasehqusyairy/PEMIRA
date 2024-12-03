using System.Security.Claims;
using System.Security.Principal;
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
            MonitoringService service = new(_context, model.LimitEntry, selectedElectionId != null ? long.Parse(selectedElectionId) : 0, model.SelectedTags);

            List<Election> elections = service.GetElections();
            elections.Insert(0, new Election { Id = 0, Name = "Pilih Pemilihan" });
            model.ElectionId = selectedElectionId;
            model.Elections = new SelectList(elections, "Id", "Name", selectedElectionId);
            model.Election = service.GetElection(Convert.ToInt64(selectedElectionId));
            model.GolputUsersCount = service.GetGolputUsersCount();
            model.Tags = service.GetTags();
            model.TagUsers = service.GetTagUsers();
            TempData["GolputCount"] = model.GolputUsersCount.ToString();
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

        public IActionResult Golputs(MonitoringViewModel model)
        {
            string? selectedElectionId = Cookie.FindFirst("ElectionId")?.Value;
            MonitoringService service = new(_context, model.LimitEntry, selectedElectionId != null ? long.Parse(selectedElectionId) : 0, model.SelectedTags);
            TableHelper.SetTableViewModel(service, model);
            model.TagUsers = service.GetTagUsers();
            ViewBag.ElectionName = service.GetElectionName();
            return View(model);
        }
        public IActionResult Print()
        {
            if (User.Identity is not ClaimsIdentity identity)
            {
                return Unauthorized();
            }
            var existingClaim = identity.FindFirst("ElectionId");
            if (existingClaim != null)
            {
                MonitoringService service = new(_context, Convert.ToInt32(TempData["GolputCount"]), Convert.ToInt64(existingClaim.Value));
                MonitoringViewModel model = new MonitoringViewModel();
                TableHelper.SetTableViewModel(service, model);

                TempData.Keep("GolputCount");
                model.TagUsers = service.GetTagUsers();
                return View(model);
            }
            else
            {
                return NoContent();
            }
        }

    }
}
