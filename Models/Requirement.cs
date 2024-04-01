using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Requirement
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Class")]
    public int ClassId { get; set; }

    public string Description { get; set; } = null!;

    public float Points { get; set; }

    public virtual ClassInfo Class { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
