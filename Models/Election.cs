using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class Election
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public long? DeletedBy { get; set; }

    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? DeletedByNavigation { get; set; }

    public virtual ICollection<ElectionUser> ElectionUsers { get; set; } = new List<ElectionUser>();

    public virtual ICollection<RoleUser> RoleUsers { get; set; } = new List<RoleUser>();

    public virtual User? UpdatedByNavigation { get; set; }
}
