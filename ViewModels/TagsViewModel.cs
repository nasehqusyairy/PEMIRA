using System.ComponentModel.DataAnnotations;
using PEMIRA.Models;

namespace PEMIRA.ViewModels
{
  public class TagsViewModel : TableViewModel<Tag>
  {
    public long Id { get; set; }

    [Required(ErrorMessage = "Nama Penanda harus diisi")]
    public string Name { get; set; } = "";
    public override string OrderBy { get; set; } = "Name";
  }
}