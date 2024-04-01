using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models
{
    // FirstName and LastName are in the ToaProUser model
    public class Student
    {
        [Key]
        [ForeignKey("ToaProUser")]
        public string StudentId { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public bool TimeSlot1 { get; set; }
        public bool TimeSlot2 { get; set; }
        public bool TimeSlot3 { get; set; }
        public bool TimeSlot4 { get; set; }
        public bool TimeSlot5 { get; set; }
        public string Reason { get; set; }

        public ToaProUser ToaProUser { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

}
