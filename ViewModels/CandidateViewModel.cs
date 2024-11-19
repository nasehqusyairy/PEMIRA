using PEMIRA.Models;
using System.ComponentModel.DataAnnotations;

namespace PEMIRA.ViewModels
{
    public class CandidateViewModel : TableViewModel<Candidate>
    {
        public long Id { get; set; }
        public override string OrderBy { get; set; } = "Name";
        public long UserId { get; set; }

        public long ElectionId { get; set; }

        public string Img { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
    }
}