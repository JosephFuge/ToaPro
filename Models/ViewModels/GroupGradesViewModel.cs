namespace ToaPro.Models.ViewModels
{
    public class GroupGradesViewModel
    {
        public Group Group { get; set; }
        public double TotalScore { get; set; } = 0;
        public string PercentGraded { get; set; } = "0%";
        public Dictionary<string, double> ClassGrades { get; set; } = [];
        public double PresentationScore { get; set; } = 0;
    }
}
