using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
  public class MonitoringViewModel
  {

    public MonitoringViewModel()
    {
      // fill example data
      ExampleItems = [
        "Item 1",
        "Item 2",
        "Item 3"
      ];
    }

    public List<string> ExampleItems { get; set; }

    public string? ElectionId { get; set; }

    public SelectList Elections { get; set; } = new(new Election[] { }, "Id", "Name");


  }
}