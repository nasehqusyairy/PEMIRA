using System.ComponentModel.DataAnnotations;
using PEMIRA.Models;
using PEMIRA.Services;

namespace PEMIRA.ViewModels
{
  public class AuthViewModel
  {

    [Required(ErrorMessage = "ID harus diisi")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "ID hanya boleh berisi huruf dan angka")]
    public string? Code { get; set; }

    [Required(ErrorMessage = "Password harus diisi")]
    public string? Password { get; set; }

    public long ElectionId { get; set; } = 0;

    public string? ReturnUrl { get; set; }

    public List<Election> Elections { get; set; } = [];

  }
}