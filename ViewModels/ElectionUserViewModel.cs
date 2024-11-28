using PEMIRA.Models;
using System.ComponentModel.DataAnnotations;

namespace PEMIRA.ViewModels
{
    public class ElectionUserViewModel : TableViewModel<ElectionUser>
    {
        public required long ElectionId { get; set; }
        public override string OrderBy { get; set; } = "Name";
        public List<TagUser> TagUsers { get; set; } = [];
        public List<Tag> Tags { get; set; } = [];
        public List<long> SelectedTags { get; set; } = [];
    }
}