using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Semester
{
    public int Id { get; set; }

    public string Term { get; set; } = null!;

    public int Year { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<ClassInfo> Classes { get; set; } = new List<ClassInfo>();

    public virtual ICollection<Grader> Graders { get; set; } = new List<Grader>();
}
