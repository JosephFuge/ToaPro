using ToaPro.Models;

namespace ToaPro
{
    public interface IIntexRepository
    {
        IEnumerable<Class> Classes { get; }
        IQueryable<Student> Students { get; } 
        IEnumerable<Grade> Grades { get; }
        IEnumerable<Grader> Graders { get; }
        IEnumerable<Requirement> Requirements { get; }
        IEnumerable<Semester> Semesters { get; }
        IEnumerable<Submission> Submissions { get; }
        IQueryable<Judge> Judges { get; }
        IQueryable<Presentation> Presentations { get; }
        IQueryable<Ranking> Rankings { get; }
        IQueryable<Group> Groups { get; }
        IQueryable<Award> Awards { get; }


        public void RequestAvailability(Judge judge);
        public void StudentRequestAvailability(Student student);

        public void UpdateRanking(Ranking ranking);

        public void AddRanking(Ranking ranking);

        public void UpdateAward(Award award);

        public void AddSubmission(Submission submission);


        public void UpdateJudgeAvailability (Judge judge);
    }
}
