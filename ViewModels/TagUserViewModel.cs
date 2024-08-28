using PEMIRA.Models;
using System.ComponentModel.DataAnnotations;
namespace PEMIRA.ViewModels
{
    public class TagUserViewModel : TableViewModel<TagUser>
    {
        public override string OrderBy { get; set; } = "Name";
        public List<Tag> Tags { get; set; } = new List<Tag>();

        [Required(ErrorMessage = "Nama Penanda harus diisi")]
        public string Name { get; set; } = "";
        public long UserId { get; set; }
    }
}
