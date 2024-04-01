using ToaPro.Models;

namespace ToaPro
{
    public interface IIntexRepository
    {
        IEnumerable<Student> Students { get; }
        
        IEnumerable<Submission> Submissions { get; }

        IEnumerable<Judge> Judges { get; }

        IEnumerable<Presentation> Presentations { get; }

        public void RequestAvailability(int Id);
        public void UpdateAvailability(Judge updatedInfo);

        public void SRequestAvailability(int Id);
        public void SUpdateAvailability(Student updatedInfo);

        public void UpdateJudgeAvailability(Judge updatedInfo);
    }
}
