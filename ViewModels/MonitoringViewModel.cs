using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
  public class MonitoringViewModel
  {

    public MonitoringViewModel()
    {
      // 
    }

    public Election? Election { get; set; }

    public string? ElectionId { get; set; }

    public SelectList Elections { get; set; } = new(new Election[] { }, "Id", "Name");

    public List<User> GolputUsers { get; set; } = [];


  }
}