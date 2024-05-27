using System.Runtime.CompilerServices;
using ToaPro.Models;

namespace ToaPro.Infrastructure
{
    public interface IIntexRepository
    {

        IEnumerable<Class> Classes { get; }
        IEnumerable<Grade> Grades { get; }
        IEnumerable<Grader> Graders { get; }
        IEnumerable<GraderAssign> GraderAssigns { get; }
        IEnumerable<Requirement> Requirements { get; }
        IEnumerable<Semester> Semesters { get; }
        IEnumerable<Submission> Submissions { get; }
        IEnumerable<SubmissionField> SubmissionFields { get; }
        IQueryable<Judge> Judges { get; }
        IQueryable<Presentation> Presentations { get; }
        IQueryable<Ranking> Rankings { get; }
        IQueryable<Award> Awards { get; }


        public void RequestAvailability(Judge judge);

        public void UpdateRanking(Ranking ranking);

        public void AddRanking(Ranking ranking);

        public void UpdateAward(Award award);

        public void AddSubmission(Submission submission);

        /* Judges */
        public Judge GetJudgeById(string id);
        public Task AddJudgeList(List<Judge> judges);
        public void UpdateJudgeAvailability(Judge judge);

        /* Students */
        IQueryable<Student> Students { get; }
        IQueryable<Group> Groups { get; }
        public void StudentRequestAvailability(Student student);
        public Task AddStudent(Student student);
        public Task AddStudentList(List<Student> students);
        public Task AddGroup(Group group);
    }
}
