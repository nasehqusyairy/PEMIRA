using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Models;
using PEMIRA.Services;
using PEMIRA.ViewModels;
using System.Security.Claims;
using System.Security.Principal;

namespace PEMIRA.Controllers
{
    [Authorize]
    public class VoteController : BaseController
    {

        public IActionResult Index()
        {
            VoteService service = new(_context);
            List<Election> elections = service.GetElections();
            elections.Insert(0, new Election { Id = 0, Name = "Pilih Pemilihan" });
            string? selectedElectionId = Cookie.FindFirst("ElectionId")?.Value;
            VoteViewModel model = new()
            {
                ElectionId = selectedElectionId,
                Elections = new SelectList(elections, "Id", "Name", selectedElectionId),
                Candidates = selectedElectionId != null ? service.GetCandidates(long.Parse(selectedElectionId)) : []
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Choose(long SelectedCandidateId)
        {

            return RedirectToAction("Index");
        }

    }
}
