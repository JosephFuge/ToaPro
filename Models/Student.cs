using AspNetCore;
using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Student
{
    public int Id { get; set; }

    public string FName { get; set; } = null!;

    public string LName { get; set; } = null!;

    public string NetId { get; set; } = null!;

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

}
