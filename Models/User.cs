using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? More { get; set; }

    public bool? Gender { get; set; }

    public string? Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    public long? DeletedBy { get; set; }

    public virtual ICollection<Candidate> CandidateCreatedByNavigations { get; set; } = new List<Candidate>();

    public virtual ICollection<Candidate> CandidateDeletedByNavigations { get; set; } = new List<Candidate>();

    public virtual ICollection<Candidate> CandidateUpdatedByNavigations { get; set; } = new List<Candidate>();

    public virtual ICollection<Candidate> CandidateUsers { get; set; } = new List<Candidate>();

    public virtual ICollection<CandidateUser> CandidateUsersNavigation { get; set; } = new List<CandidateUser>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? DeletedByNavigation { get; set; }

    public virtual ICollection<Election> ElectionCreatedByNavigations { get; set; } = new List<Election>();

    public virtual ICollection<Election> ElectionDeletedByNavigations { get; set; } = new List<Election>();

    public virtual ICollection<Election> ElectionOwnerNavigations { get; set; } = new List<Election>();

    public virtual ICollection<Election> ElectionUpdatedByNavigations { get; set; } = new List<Election>();

    public virtual ICollection<ElectionUser> ElectionUsers { get; set; } = new List<ElectionUser>();

    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<User> InverseDeletedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<User> InverseUpdatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<PermissionUser> PermissionUsers { get; set; } = new List<PermissionUser>();

    public virtual ICollection<RoleUser> RoleUsers { get; set; } = new List<RoleUser>();

    public virtual ICollection<Tag> TagCreatedByNavigations { get; set; } = new List<Tag>();

    public virtual ICollection<Tag> TagDeletedByNavigations { get; set; } = new List<Tag>();

    public virtual ICollection<Tag> TagUpdatedByNavigations { get; set; } = new List<Tag>();

    public virtual ICollection<TagUser> TagUsers { get; set; } = new List<TagUser>();

    public virtual User? UpdatedByNavigation { get; set; }
}
