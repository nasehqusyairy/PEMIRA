using System.ComponentModel.DataAnnotations;
using PEMIRA.Models;

namespace PEMIRA.ViewModels;

public class ProfileViewModel
{
  public long Id { get; set; }

  [Required(ErrorMessage = "Nama harus diisi")]
  [RegularExpression(@"^[a-zA-Z0-9\s\.\,\']+$", ErrorMessage = "Nama hanya boleh berisi huruf, angka, spasi, titik, koma, dan tanda petik")]
  public string? Name { get; set; }

  [Required(ErrorMessage = "ID harus diisi")]
  [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "ID hanya boleh berisi huruf dan angka")]
  public string? Code { get; set; }

  [Required(ErrorMessage = "Jenis Kelamin harus diisi")]
  public bool Gender { get; set; }

  public string? Password { get; set; }

  [Compare("Password", ErrorMessage = "Konfirmasi Password tidak sama dengan Password")]
  public string? ConfirmPassword { get; set; }
}