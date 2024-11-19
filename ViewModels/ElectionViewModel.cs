using PEMIRA.Models;
using System.ComponentModel.DataAnnotations;

namespace PEMIRA.ViewModels
{
    public class ElectionViewModel : TableViewModel<Election>
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Nama Pemilihan Harus Diisi")]
        public string Name { get; set; } = "";
        public override string OrderBy { get; set; } = "Name";

    }
}