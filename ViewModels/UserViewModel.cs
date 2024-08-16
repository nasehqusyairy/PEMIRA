using PEMIRA.Models;
using System.ComponentModel.DataAnnotations;

namespace PEMIRA.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Nama harus diisi")]
        [RegularExpression(@"^[a-zA-Z0-9\s\.\,\']+$", ErrorMessage = "Nama hanya boleh berisi huruf, angka, spasi, titik, koma, dan tanda petik")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "ID harus diisi")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "ID hanya boleh berisi huruf dan angka")]
        public string? Code { get; set; }

        [Required(ErrorMessage = "Jenis Kelamin Harus diisi")]
        public bool? Gender { get; set; }

        public string? More { get; set; }

        [Required(ErrorMessage = "Kata Sandi Harus Diisi")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Konfirmasi Kata Sandi Tidak Sama")]
        public string? ConfirmPassword { get; set; }
        public List<User> Users { get; set; } = [];

    }
}