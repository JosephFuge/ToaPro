using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models;

public partial class Submission
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("GroupId")]
    public int GroupId { get; set; } //We get this from them being logged in
    [ForeignKey("StudentId")]
    public string StudentId { get; set; } //We get this from them being logged in
    public DateTime CreatedDate { get; set; } //This is created when the submission is submitted
    [Required(ErrorMessage = "Please include a public GitHub Link.")]
    public string GithubLink { get; set; } //This is submitted by the students
    [Required(ErrorMessage = "Please include a YouTube Link.")]
    public string YoutubeLink { get; set; } //This is submitted by the students
    [Required(ErrorMessage = "Please upload the necessary files.")]
    public string? UploadFile { get; set; } //This is submitted by the students

    //These help link the submissions to other tables in the database
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public virtual Group Group { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
