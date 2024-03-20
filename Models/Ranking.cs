using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Ranking
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int JudgeId { get; set; }

    public float? Points { get; set; }

    public int? Ranking1 { get; set; }

    public string? Comments { get; set; }

    public string? Nomination { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Judge Judge { get; set; } = null!;
}
