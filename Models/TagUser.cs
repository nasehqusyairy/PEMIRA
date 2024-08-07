using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class TagUser
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long TagId { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
