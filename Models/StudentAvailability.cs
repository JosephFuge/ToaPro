using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models
{
    public class StudentAvailability
    {
        [Key]
        public string StudentId { get; set; }
        public string StudentFName { get; set; }
        public string StudentLName { get; set; }
        public bool TimeSlot1 { get; set; }
        public bool TimeSlot2 { get; set; }
        public bool TimeSlot3 { get; set; }
        public bool TimeSlot4 { get; set; }
        public bool TimeSlot5 { get; set; }
        public string Reason { get; set; }
    }
}
