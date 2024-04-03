namespace ToaPro.Models.ViewModels
{
    public class GradingSummaryViewModel
    {
        public Class? Class { get; set; }
        public Requirement? Requirement { get; set; }
        public Grade? Grade { get; set; }
        public Submission? Submission { get; set; }
        public Student? Student { get; set; }
        public Group? Group { get; set; }
        public Ranking? Rank { get; set; }
    }
}
