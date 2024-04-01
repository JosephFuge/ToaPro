using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Presentation
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Group")]
    public int GroupId { get; set; }

    public string Location { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<Judge> Judges { get; set; } = new List<Judge>();
    public ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();
}
