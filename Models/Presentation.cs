using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Presentation
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public string Location { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<Judge> Judges { get; set; } = new List<Judge>();
    public ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();
}
