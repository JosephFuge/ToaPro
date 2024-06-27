namespace ToaPro.Models.ViewModels
{
    public class GradingSummaryViewModel
    {
        public int Section { get; set; } = 0;
        public List<GroupGradesViewModel> GroupGrades { get; set; } = [];
    }
}
