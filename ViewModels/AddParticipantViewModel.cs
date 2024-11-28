using System.ComponentModel.DataAnnotations;
using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
  public class AddParticipantViewModel : TableViewModel<User>
  {

    public AddParticipantViewModel()
    {
    }

    public long ElectionId { get; set; }

    public List<long> Participants { get; set; } = [];

    public List<Tag> Tags { get; set; } = [];

    public List<long> SelectedTags { get; set; } = [];

    public List<TagUser> TagUsers { get; set; } = [];

    public List<long> SelectedUsers { get; set; } = [];

    public override string OrderBy { get; set; } = "Name";
  }
}