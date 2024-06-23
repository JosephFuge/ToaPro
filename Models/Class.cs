using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Class
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();

    public virtual ICollection<Semester> Semesters { get; set; } = new List<Semester>();
}
