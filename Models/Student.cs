using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Student
{
    public int Id { get; set; }
    public int user_id { get; set; }

    public string NetId { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public bool TimeSlot1 { get; set; }
    public bool TimeSlot2 { get; set; }
    public bool TimeSlot3 { get; set; }
    public bool TimeSlot4 { get; set; }
    public bool TimeSlot5 { get; set; }
}
