using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Grade
{
    public string Id { get; set; }

    [ForeignKey("Requirement")]
    public int RequirementId { get; set; }

    public string GraderId { get; set; }

    public int GroupId { get; set; }

    public float? Points { get; set; }

    public string? Comments { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Requirement Requirement { get; set; } = null!;
}
