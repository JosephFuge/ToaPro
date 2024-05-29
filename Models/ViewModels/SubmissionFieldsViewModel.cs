namespace ToaPro.Models.ViewModels
{
    public class SubmissionFieldsViewModel
    {
        public List<SubmissionField> SubmissionFields { get; set; } = new List<SubmissionField>();
        public List<int> SubmissionFieldFrequencies { get; set; }
        public List<int>? DeleteFieldIds { get; set; } = new List<int>();
        public string TermYear { get; set; }
    }
}
