using ToaPro.Models;

namespace ToaPro
{
    public interface IIntexRepository
    {
        IEnumerable<Student> Students { get; }
        
        IEnumerable<Submission> Submissions { get; }

        IEnumerable<Judge> Judges { get; }

        IEnumerable<Presentation> Presentations { get; }

        public void JRequestAvailability(int Id);
        public void JUpdateAvailability(Judge updatedInfo);

        public void UpdateJudgeAvailability(Judge updatedInfo);
    }
}
