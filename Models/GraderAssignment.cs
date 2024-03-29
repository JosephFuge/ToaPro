using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models
{
    public partial class GraderAssignment
    {
        public int GraderAssignmentId { get; set; }

        [ForeignKey("Id")]
        public int GraderId { get; set; }
        public virtual Grader Grader { get; set; }

        [ForeignKey("Id")]
        public int RequirementId { get; set; }
        public virtual Requirement Requirement { get; set; }

        [ForeignKey("Id")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
