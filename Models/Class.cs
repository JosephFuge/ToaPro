using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Class
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();
    [ForeignKey("Semester")]
    public int SemesterId { get; set; }

    public virtual Semester Semester { get; set; }
}
