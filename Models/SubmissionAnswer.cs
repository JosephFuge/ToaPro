using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToaPro.Models
{
    public class SubmissionAnswer
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("SubmissionField")]
        public int SubmissionFieldId { get; set; }
        public virtual SubmissionField SubmissionField { get; set; }
        [ForeignKey("SubmittingGroup")]
        public int GroupId { get; set; }
        public virtual Group SubmittingGroup { get; set; }
        public DateTime SubmitDate { get; set; }
        public string TextData { get; set; } // Store text data. If an image or file is uploaded (FileData is not null), store the MIME type (i.e. image/jpeg, image/png)
        public byte[]? FileData { get; set; } // Store file data as array of bytes (bytea type in Postgres)
        
    }
}
