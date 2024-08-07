using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class Tag
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public long? DeletedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? DeletedByNavigation { get; set; }

    public virtual ICollection<TagUser> TagUsers { get; set; } = new List<TagUser>();

    public virtual User? UpdatedByNavigation { get; set; }
}
