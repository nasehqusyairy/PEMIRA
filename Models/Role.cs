using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class Role
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();

    public virtual ICollection<RoleUser> RoleUsers { get; set; } = new List<RoleUser>();
}
