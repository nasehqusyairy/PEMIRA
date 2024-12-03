using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
    public class VoteViewModel
    {

        public string? ElectionId { get; set; }

        public SelectList Elections { get; set; } = new(new Election[] { }, "Id", "Name");

        public List<Candidate> Candidates { get; set; } = [];

        public long? SelectedCandidateId { get; set; }
        public long UserId { get; set; }

    }
}