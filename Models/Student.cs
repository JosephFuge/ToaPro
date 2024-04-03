using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models
{
    public class Student
    {
        [Key]
        [ForeignKey("ToaProUser")]
        public string Id { get; set; }

        public ToaProUser ToaProUser { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
        public string NetId { get; set; } = null!;
        public string Reason { get; set; }


        public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

        public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    }
}
