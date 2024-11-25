using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
  public class VoteViewModel
  {

    public string? ElectionId { get; set; }

    [Required(ErrorMessage = "This field is required")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "This field can only contain letters and numbers")]
    public string? ExampleField { get; set; }

    public SelectList Elections { get; set; } = new(new Election[] { }, "Id", "Name");

  }
}