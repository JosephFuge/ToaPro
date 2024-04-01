﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Group
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Semester")]
    public int SemesterId { get; set; }

    public short Section { get; set; }

    public short Number { get; set; }

    public virtual Semester Semester { get; set; } = null!;
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Presentation> Presentations { get; set; } = new List<Presentation>();

    public virtual ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
