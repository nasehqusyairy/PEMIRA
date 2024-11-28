using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
                Elections = new SelectList(elections, "Id", "Name", selectedElectionId),
                Election = selectedElectionId != null ? service.GetElection(long.Parse(selectedElectionId)) : null,
                GolputUsers = selectedElectionId != null ? service.GetGolputUsers(long.Parse(selectedElectionId)) : []
            };

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
