using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Student
{
    public int Id { get; set; }
    public int user_id { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }

    public string NetId { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
