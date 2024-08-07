using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class ElectionUser
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ElectionId { get; set; }

    public virtual Election Election { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
