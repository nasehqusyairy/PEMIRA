using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class MenuRole
{
    public long Id { get; set; }

    public long MenuId { get; set; }

    public long RoleId { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
