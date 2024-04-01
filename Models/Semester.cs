using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models;

public partial class Semester
{
    [Key]
    public int Id { get; set; }

    public string Term { get; set; } = null!;

    public int Year { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Grader> Graders { get; set; } = new List<Grader>();
}
