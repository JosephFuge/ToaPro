using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Grader
{
    public int Id { get; set; }
    public bool IsProfessor { get; set; }

    [ForeignKey("ToaProUserId")]
    public string ToaProUserId { get; set; }

    public virtual ToaProUser ToaProUser { get; set; }
}
