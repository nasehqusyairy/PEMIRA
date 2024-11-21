using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
    public class CandidateViewModel : TableViewModel<Candidate>
    {
        public long Id { get; set; }
        public override string OrderBy { get; set; } = "Name";
        public long UserId { get; set; }

        public string Code { get; set; } = "";

        public long ElectionId { get; set; }
        public string Color { get; set; } = "#00bcd4";

        public string Img { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
    }
}