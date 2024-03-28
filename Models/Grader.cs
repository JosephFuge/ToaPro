using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Grader
{
    public int Id { get; set; }
    [ForeignKey("Name")]
    public int NameId { get; set; }

    [ForeignKey("Requirement")]
    public int Requirement_ID { get; set; }
    [ForeignKey("Group")]
    public int Group_ID { get; set; }
    public virtual Requirement? Requirement { get; set; }
    public virtual Group? Group { get; set; }
    public virtual Class Class { get; set; } = null!;
    // public virtual Name Name { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Semester> Semesters { get; set; } = new List<Semester>();
}
