using ToaPro.Models;

namespace ToaPro
{
    public interface IIntexRepository
    {
        IEnumerable<Student> Students { get; } 
        IEnumerable<Class> Classes { get; }
        IEnumerable<Grade> Grades { get; }
        IEnumerable<Grader> Graders { get; }
        IEnumerable<Requirement> Requirements { get; }
        IEnumerable<Semester> Semesters { get; }
        IEnumerable<Submission> Submissions { get; }
        IQueryable<Judge> Judges { get; }
        IQueryable<Presentation> Presentations { get; }
        IEnumerable<JudgeAvailability> JudgeAvailabilities { get; }
        IQueryable<Ranking> Rankings { get; }
        IQueryable<Group> Groups { get; }

        public void RequestAvailability(JudgeAvailability availabilities);
        public void StudentRequestAvailability(Student availabilities);

        public void UpdateRanking(Ranking ranking);

        public void JRequestAvailability(int Id);
        public void JUpdateAvailability(Judge updatedInfo);

        public void UpdateJudgeAvailability(Judge updatedInfo);
        public void AddRanking(Ranking ranking);
        public void AddSubmission(Submission submission);
    }
}
