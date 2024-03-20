using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Requirement
{
    public int Id { get; set; }

    public int ClassId { get; set; }

    public string Description { get; set; } = null!;

    public float Points { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
