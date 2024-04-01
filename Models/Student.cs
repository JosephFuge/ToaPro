using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models;

public partial class Student
{
    [Key]
    public int Id { get; set; }

    public string FName { get; set; } = null!;

    public string LName { get; set; } = null!;

    public string NetId { get; set; } = null!;
    public bool TimeSlot1 { get; set; }
    public bool TimeSlot2 { get; set; }
    public bool TimeSlot3 { get; set; }
    public bool TimeSlot4 { get; set; }
    public bool TimeSlot5 { get; set; }
    public string Reason { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
