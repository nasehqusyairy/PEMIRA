using System.ComponentModel.DataAnnotations;

namespace PEMIRA.ViewModels
{
  public class CandidateViewModel
  {

    public CandidateViewModel()
    {
      // fill example data
      ExampleItems = [
        "Item 1",
        "Item 2",
        "Item 3"
      ];
    }

    [Required(ErrorMessage = "This field is required")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "This field can only contain letters and numbers")]
    public string? ExampleField { get; set; }

    public List<string> ExampleItems { get; set; }

  }
}