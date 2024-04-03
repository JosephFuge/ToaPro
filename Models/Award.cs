using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models
{
    public class Award
    {
        [Key]
        public int AwardId { get; set; } 
        public string? AwardName { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; } = null!;
    }
}
