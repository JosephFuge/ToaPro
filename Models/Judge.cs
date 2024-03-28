using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models;

public partial class Judge
{
    [Key]
    public int Id { get; set; }
    public int user_id { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public int semester_id { get; set; }

    public string Affiliation { get; set; } = null!;

    public virtual ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();

    public virtual ICollection<Presentation> Presentations { get; set; } = new List<Presentation>();

    public bool TimeSlot1 { get; set; }
    public bool TimeSlot2 { get; set; }
    public bool TimeSlot3 { get; set; }
    public bool TimeSlot4 { get; set; }
    public bool TimeSlot5 { get; set; }
}
