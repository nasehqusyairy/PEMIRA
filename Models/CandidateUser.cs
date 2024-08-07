using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class CandidateUser
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long CandidateId { get; set; }

    public virtual Candidate Candidate { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
