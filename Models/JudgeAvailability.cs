using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models
{
    public class JudgeAvailability
    {
        [Key]
        public string JudgeId { get; set; }
        public string JudgeFName { get; set; }
        public string JudgeLName { get; set; }
        public bool TimeSlot1 { get; set; }
        public bool TimeSlot2 { get; set; }
        public bool TimeSlot3 { get; set; }
        public bool TimeSlot4 { get; set; }
        public bool TimeSlot5 { get; set; }
    }
}
