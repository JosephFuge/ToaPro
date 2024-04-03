using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models
{
    public partial class GraderAssign
    {
        [Key]
        [ForeignKey("Grader")]
        public string graderId { get; set; }
        [ForeignKey("Group")]
        public int groupId { get; set; }
        [ForeignKey("Requirement")]
        public int requirementId {  get; set; }

        public virtual Group Group { get; set; }
        public virtual Requirement Requirement { get; set; }
    }
}
