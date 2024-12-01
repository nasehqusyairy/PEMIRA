using Microsoft.AspNetCore.Mvc.Rendering;
using PEMIRA.Models;
using System.ComponentModel.DataAnnotations;

namespace PEMIRA.ViewModels
{
    public class MenuRoleViewModel : TableViewModel<MenuRole>
    {
        public override string OrderBy { get; set; } = "Name";
        public List<Role> Roles { get; set; } = new();
        public long Id { get; set; } 
        public long SelectedId { get; set; }


    }
}