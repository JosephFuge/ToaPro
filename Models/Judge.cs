using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ToaPro.Models;
public partial class Judge
{
    [Key]
    [ForeignKey("ToaProUser")]
    public string Id { get; set; }
    public ToaProUser ToaProUser { get; set; }
    public string? JudgeType { get; set; } = null!; // For example: Alumni, Professor, Industry
    public string? Affiliation { get; set; } = null!;
    public bool HasConfirmedTimes { get; set; } = false; // If the judges have set their schedule for the available time slots
    public bool TimeSlot1 { get; set; }
    public bool TimeSlot2 { get; set; }
    public bool TimeSlot3 { get; set; }
    public bool TimeSlot4 { get; set; }
    public bool TimeSlot5 { get; set; }
    public virtual ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();
    public virtual ICollection<Presentation> Presentations { get; set; } = new List<Presentation>();

}