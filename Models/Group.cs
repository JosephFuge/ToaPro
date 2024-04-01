using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Group
{
    public int Id { get; set; }

    public int SemesterId { get; set; }

    public short Section { get; set; }

    public short Number { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Presentation> Presentations { get; set; } = new List<Presentation>();

    public virtual ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();

    public virtual Semester Semester { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

	public bool TimeSlot1 { get; set; }
	public bool TimeSlot2 { get; set; }
	public bool TimeSlot3 { get; set; }
	public bool TimeSlot4 { get; set; }
	public bool TimeSlot5 { get; set; }
}
