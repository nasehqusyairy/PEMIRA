using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
  public class MonitoringViewModel : TableViewModel<User>
  {

    public Election? Election { get; set; }

    public string? ElectionId { get; set; }

    public SelectList Elections { get; set; } = new(new Election[] { }, "Id", "Name");

    public long GolputUsersCount { get; set; } = 0;
    public override string OrderBy { get; set; } = "Name";

    public List<Tag> Tags { get; set; } = [];

    public List<TagUser> TagUsers { get; set; } = [];

    public List<long> SelectedTags { get; set; } = [];

  }
}