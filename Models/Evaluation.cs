using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Evaluation
{
    public int EvaluationId { get; set; }
    public int? PeerEvalGrade { get; set; }
    public int? EvaluatedId { get; set; }
    public string? EvalComments { get; set; }

    public int? SubmissionId { get; set; }
    public virtual Submission? Submission { get; set; }

    // Assume each evaluation is for a single group and multiple students
    public int? GroupId { get; set; }
    public virtual Group? Group { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}

