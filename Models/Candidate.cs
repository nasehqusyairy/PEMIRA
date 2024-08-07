using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class Candidate
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ElectionId { get; set; }

    public string Img { get; set; } = null!;

    public string Color { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public long? DeletedBy { get; set; }

    public virtual ICollection<CandidateUser> CandidateUsers { get; set; } = new List<CandidateUser>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? DeletedByNavigation { get; set; }

    public virtual Election Election { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
