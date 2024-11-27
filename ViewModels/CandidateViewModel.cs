using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
    public class CandidateViewModel() : TableViewModel<Candidate>
    {
        public long Id { get; set; }
        public override string OrderBy { get; set; } = "Name";

        public long UserId { get; set; }

        [Required(ErrorMessage = "ID pengguna harus diisi")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "ID hanya boleh berisi huruf dan angka")]
        public string Code { get; set; } = "";

        [Required(ErrorMessage = "ID pemilihan harus diisi")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "ID hanya boleh berisi huruf dan angka")]
        public long ElectionId { get; set; }
        public string Color { get; set; } = "#00bcd4";
        public string? Img { get; set; }

        [Required(ErrorMessage = "Gambar tidak boleh kosong")]
        public IFormFile Image { get; set; } = null!;
    }
}