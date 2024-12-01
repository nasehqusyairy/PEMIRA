using PEMIRA.Models;
using System.ComponentModel.DataAnnotations;

namespace PEMIRA.ViewModels
{
  public class MenuViewModel : TableViewModel<Menu>
  {
        public long Id { get; set; }
        public override string OrderBy { get; set; } = "Name";
        public List<Role> Roles { get; set; } = new();
    }
}