namespace ToaPro.Models.ViewModels
{
    public class SubmissionGradingViewModel
    {
        public List<ClassGradesViewModel> ClassGrades { get; set; } = [];
        public List<Grade> Grades { get; set; }
        public List<SubmissionAnswer>? SubmissionAnswers { get; set; }
    }
}
