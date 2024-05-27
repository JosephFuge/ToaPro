namespace ToaPro.Models.ViewModels
{
    public class SubmissionFieldsViewModel
    {
        public Dictionary<SubmissionField, int> SubmissionFieldsFrequencies { get; set; } // A submission field as the key with the number of times students have submitted it as a value
        public string TermYear { get; set; }
    }
}
