using ToaPro.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ToaPro
{
    public class EFIntexRepository : IIntexRepository
    {
        private readonly ToaProContext _toaProContext;

        public EFIntexRepository(ToaProContext toaProContext)
        {
            _toaProContext = toaProContext;
        }

        public IEnumerable<Student> Students => _toaProContext.Students.ToList();
        public IEnumerable<Submission> Submissions => _toaProContext.Submissions.ToList();
        public IQueryable<Judge> Judges => _toaProContext.Judges;
        public IQueryable<Presentation> Presentations => _toaProContext.Presentations;
        public IEnumerable<Class> Classes => _toaProContext.Classes;
        public IEnumerable<Grade> Grades => _toaProContext.Grades;
        public IEnumerable<Grader> Graders => _toaProContext.Graders;
        public IEnumerable<Requirement> Requirements => _toaProContext.Requirements;
        public IEnumerable<Semester> Semesters => _toaProContext.Semesters;
        public IEnumerable<JudgeAvailability> JudgeAvailabilities => _toaProContext.JudgeAvailabilities;
        public IQueryable<Ranking> Rankings => _toaProContext.Rankings;
        public IQueryable<Models.Group> Groups => _toaProContext.Groups;

        public void RequestAvailability(JudgeAvailability judgeAvailability)
        {
            _toaProContext.Add(judgeAvailability);
            _toaProContext.SaveChanges();
        }

        public void UpdateRanking(Ranking ranking)
        {
            _toaProContext.Rankings.Update(ranking);
            _toaProContext.SaveChanges();
        }

        public void AddRanking(Ranking ranking)
        {
            _toaProContext.Rankings.Add(ranking);
            _toaProContext.SaveChanges();
        }

        public void StudentRequestAvailability(Student studentAvailability)
        {
            _toaProContext.Add(studentAvailability);
            _toaProContext.SaveChanges();
        }

        public void AddSubmission(Submission submission)
        {
            _toaProContext.Submissions.Add(submission);
            _toaProContext.SaveChanges();
        }
    }
}
