using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models;

public partial class Judge
{
    [Key]
    public int Id { get; set; }

    public string FName { get; set; } = null!;

    public string LName { get; set; } = null!;

    public string Affiliation { get; set; } = null!;

    public virtual ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();

    public virtual ICollection<Presentation> Presentations { get; set; } = new List<Presentation>();
}
