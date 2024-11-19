using PEMIRA.Models;
using System.ComponentModel.DataAnnotations;

namespace PEMIRA.ViewModels
{
    public class ElectionUserViewModel : TableViewModel<ElectionUser>
    {
        public Election? Election { get; set; }
        public List<long> SelectedUsers { get; set; } = [];
        public bool? IsEnroled { get; set; }
        public List<ElectionUser> ElectionUsers { get; set; } = new List<ElectionUser>();
        public override string OrderBy { get; set; } = "Name";
        public List<TagUser> TagUsers { get; set; } = [];
        public List<Tag> Tags { get; set; } = [];
        public List<long> SelectedTags { get; set; } = [];
    }
}