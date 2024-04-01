using ToaPro.Models;

namespace ToaPro
{
    public interface IIntexRepository
    {
        IEnumerable<ClassInfo> Classes { get; }
        IEnumerable<Grade> Grades { get; }
        IEnumerable<Grader> Graders { get; }
        IEnumerable<Group> Groups { get; }
        IEnumerable<Judge> Judges { get; }
        IEnumerable<Presentation> Presentations { get; }
        IEnumerable<Ranking> Rankings { get; }
        IEnumerable<Requirement> Requirements { get; }
        IEnumerable<Semester> Semesters { get; }
        IEnumerable<Student> Students { get; }
        IEnumerable<Submission> Submissions { get; }
        IEnumerable<JudgeAvailability> JudgeAvailabilities { get; }
        public void RequestAvailability(JudgeAvailability availabilities);
        public void StudentRequestAvailability(StudentAvailability availabilities);
    }
}
