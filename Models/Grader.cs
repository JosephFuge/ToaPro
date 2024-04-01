using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Grader
{
    public int Id { get; set; }
    [ForeignKey("Name")]
    public int NameId { get; set; }

    public int ClassId { get; set; }
    public bool IsProfessor { get; set; }

    public virtual Class Class { get; set; } = null!;
    // public virtual Name Name { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Semester> Semesters { get; set; } = new List<Semester>();
}
