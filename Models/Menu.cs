using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class Menu
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    public long MenusegmentId { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();

    public virtual Menusegment Menusegment { get; set; } = null!;
}
