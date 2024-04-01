using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Ranking
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Group")]
    public int GroupId { get; set; }
    [ForeignKey("Judge")]
    public int JudgeId { get; set; }

    public int? TeamRanking { get; set; }

    public int? CommunicationPoints { get; set; }
    public int? TechnologyPoints { get; set; }
    public int? OverallPoints { get; set; }

    public string? CommunicationComments { get; set; }
    public string? TechnologyComments { get; set; }
    public string? OverallComments { get; set; }

    public string? Nomination { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Judge Judge { get; set; } = null!;
}
