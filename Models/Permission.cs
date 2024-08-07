using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class Permission
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PermissionUser> PermissionUsers { get; set; } = new List<PermissionUser>();
}
