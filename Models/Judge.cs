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
    public string FName { get; set; } = null!;
    public string LName { get; set; } = null!;
    public string Affiliation { get; set; } = null!;

    //This is how we get the Foreign key relationship info
    public ToaProUser ToaProUser { get; set; }

    public virtual ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();

    public virtual ICollection<Presentation> Presentations { get; set; } = new List<Presentation>();

    public bool TimeSlot1 { get; set; }
    public bool TimeSlot2 { get; set; }
    public bool TimeSlot3 { get; set; }
    public bool TimeSlot4 { get; set; }
    public bool TimeSlot5 { get; set; }

}
