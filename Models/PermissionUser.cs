using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class PermissionUser
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long PermissionId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
