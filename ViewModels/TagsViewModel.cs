using System.ComponentModel.DataAnnotations;
using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
  public class TagsViewModel
  {
    [MinLength(3, ErrorMessage = "Nama tag minimal 3 karakter")]
    [Required(ErrorMessage = "Nama tag harus diisi")]
    public string Name { get; set; } = "";

    public List<Tag> Tags { get; set; } = [];

  }
}