using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Submission
{
    public string GithubLink { get; set; }
    public string YoutubeLink { get; set; }
    public string UploadFile { get; set; }

    public int Id { get; set; }

    public int GroupId { get; set; }

    [ForeignKey("Student")]
    public string StudentId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Group Group { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
