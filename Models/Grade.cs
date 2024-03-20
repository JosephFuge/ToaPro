﻿using System;
using System.Collections.Generic;

namespace ToaPro.Models;

public partial class Grade
{
    public int Id { get; set; }

    public int RequirementId { get; set; }

    public int GraderId { get; set; }

    public int GroupId { get; set; }

    public int SubmissionId { get; set; }

    public float? Points { get; set; }

    public string? Comments { get; set; }

    public virtual Grader Grader { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;

    public virtual Requirement Requirement { get; set; } = null!;

    public virtual Submission Submission { get; set; } = null!;
}