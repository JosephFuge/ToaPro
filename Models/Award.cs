using System;
using System.Collections.Generic;

namespace ToaPro.Models
{
    public class Award
    {
        public int AwardId { get; set; } 
        public int GroupId { get; set; }

        public string? AwardName { get; set; }

        public virtual Group Group { get; set; } = null!;
    }
}
