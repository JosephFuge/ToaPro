using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToaPro.Infrastructure;

namespace ToaPro.Models
{
    public class SubmissionField
    {
        [Key]
        public int Id { get; set; }
        public int SemesterId { get; set; }
        public DateTime DueDate { get; set; }
        public string Prompt { get; set; } // Label shown as text to students when they are looking to submit
        public SubmissionInputType DataType { get; set; }
        public bool IsDefault { get; set; } // Whether it should carry over to a newly created semester or not
    }
}
