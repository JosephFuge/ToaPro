using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Evaluation
{
    public string? Comments { get; set; }

    public int SubmissionId { get; set; }

    public int? StudentId { get; set; }

    public int? PeerEvalGrade { get; set; }

    public int? GroupId { get; set; }

    public int? EvaluatedId { get; set; }
}
