using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models
{
    public class Student
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string NetId { get; set; } = null!;
        public bool TimeSlot1 { get; set; }
        public bool TimeSlot2 { get; set; }
        public bool TimeSlot3 { get; set; }
        public bool TimeSlot4 { get; set; }
        public bool TimeSlot5 { get; set; }
        public string Reason { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
