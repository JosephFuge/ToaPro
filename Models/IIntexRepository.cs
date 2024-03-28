using ToaPro.Models;

namespace ToaPro
{
    public interface IIntexRepository
    {
        IEnumerable<Student> Students { get; }
        
        IEnumerable<Submission> Submissions { get; }

        IEnumerable<Judge> Judges { get; }

        IEnumerable<Presentation> Presentations { get; }

        public void RequestAvailability(Judge Id);
        public void UpdateAvailability(Judge Id);
        p/*ublic void StudentRequestAvailability(StudentAvailability availabilities);*/
    }
}
