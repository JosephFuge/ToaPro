using System.Runtime.CompilerServices;
using ToaPro.Models;

namespace ToaPro.Infrastructure
{
    public interface IIntexRepository
    {

        IEnumerable<Class> Classes { get; }
        IQueryable<Requirement> Requirements { get; }
        IEnumerable<Semester> Semesters { get; }
        IQueryable<Judge> Judges { get; }
        IQueryable<Presentation> Presentations { get; }
        IQueryable<Ranking> Rankings { get; }
        IQueryable<Award> Awards { get; }


        public void RequestAvailability(Judge judge);

        public void UpdateRanking(Ranking ranking);

        public void AddRanking(Ranking ranking);

        public void UpdateAward(Award award);

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

        /* Submissions */
        IQueryable<SubmissionAnswer> SubmissionAnswers { get; }
        public Task<int> AddSubmissionAnswers(IEnumerable<SubmissionAnswer> answers);
        IEnumerable<SubmissionField> SubmissionFields(bool tracking = true);
        public void AddSubmissionFieldList(List<SubmissionField> submissionFields);
        public void UpdateSubmissionFieldList(List<SubmissionField> submissionFields);
        public void DeleteSubmissionFieldList(List<SubmissionField> submissionFields);
        public Task<int> CommitChangesAsync();

        /* Grades */
        IQueryable<Grade> Grades { get; }
        public void AddGrades(IEnumerable<Grade> grades);
        public void UpdateGrades(IEnumerable<Grade> grades);
    }
}
