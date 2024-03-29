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
        public IEnumerable<Class> Classes => (IEnumerable<Class>)_toaProContext;
        public IEnumerable<Grade> Grades => (IEnumerable<Grade>)_toaProContext;
        public IEnumerable<Grader> Graders => (IEnumerable<Grader>)_toaProContext;
        public IEnumerable<Models.Group> Groups => (IEnumerable<Models.Group>)_toaProContext;
        public IEnumerable<Judge> Judges => (IEnumerable<Judge>)_toaProContext;
        public IEnumerable<Presentation> Presentations => (IEnumerable<Presentation>)_toaProContext;
        public IEnumerable<Ranking> Rankings => (IEnumerable<Ranking>)_toaProContext;
        public IEnumerable<Requirement> Requirements => (IEnumerable<Requirement>)_toaProContext;
        public IEnumerable<Semester> Semesters => (IEnumerable<Semester>)_toaProContext;
        public IEnumerable<Student> Students => (IEnumerable<Student>)_toaProContext;
        public IEnumerable<Submission> Submissions => (IEnumerable<Submission>)_toaProContext;

        public void RequestAvailability(JudgeAvailability judgeAvailability)
        {
            _toaProContext.Add(judgeAvailability);
            _toaProContext.SaveChanges();
        }
        public IEnumerable<Class> Classes => _toaProContext.Classes;
        //comment
        public IEnumerable<Grade> Grades => _toaProContext.Grades;
        public IEnumerable<Grader> Graders => _toaProContext.Graders;
        public IEnumerable<Group> Groups => _toaProContext.Groups;
        public IEnumerable<Judge> Judges => _toaProContext.Judges;
        public IEnumerable<Presentation> Presentations => _toaProContext.Presentations;
        public IEnumerable<Ranking> Rankings => _toaProContext.Rankings;
        public IEnumerable<Requirement> Requirements => _toaProContext.Requirements;
        public IEnumerable<Semester> Semesters => _toaProContext.Semesters;
        public IEnumerable<Student> Students => _toaProContext.Students;
        public IEnumerable<Submission> Submissions => _toaProContext.Submissions;
    }
}
