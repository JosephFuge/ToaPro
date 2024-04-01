using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models;

public partial class Submission
{
    [Required(ErrorMessage = "Please add a GitHub Link.")]
    public string GithubLink { get; set; }
    [Required(ErrorMessage = "Please add a YouTube Link or write 'N/A")]
    public string YoutubeLink { get; set; }
    [Required(ErrorMessage = "Please upload any other needed files.")]
    public string UploadFile { get; set; }
    [Key]
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int StudentId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Group Group { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
