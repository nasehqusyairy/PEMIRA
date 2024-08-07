using System;
using System.Collections.Generic;

namespace PEMIRA.Models;

public partial class Menusegment
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
}
