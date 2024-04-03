﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Submission
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Group")]
    public int GroupId { get; set; }
    [ForeignKey("Student")]
    public string StudentId { get; set; }
    public DateTime CreatedDate { get; set; }
    [Required(ErrorMessage = "Please include a public GitHub Link.")]
    public string GithubLink { get; set; }
    [Required(ErrorMessage = "Please include a YouTube Link.")]
    public string YoutubeLink { get; set; }
    [Required(ErrorMessage = "Please upload the necessary files.")]
    public string? UploadFile { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Group Group { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
