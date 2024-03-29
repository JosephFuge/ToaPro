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
        public IEnumerable<Class> Classes => _toaProContext.Classes;
        public IEnumerable<Grade> Grades => _toaProContext.Grades;
        public IEnumerable<Grader> Graders => _toaProContext.Graders;
        public IEnumerable<Models.Group> Groups => _toaProContext.Groups;
        public IEnumerable<Judge> Judges => _toaProContext.Judges;
        public IEnumerable<Presentation> Presentations => _toaProContext.Presentations;
        public IEnumerable<Ranking> Rankings => _toaProContext.Rankings;
        public IEnumerable<Requirement> Requirements => _toaProContext.Requirements;
        public IEnumerable<Semester> Semesters => _toaProContext.Semesters;
        public IEnumerable<Student> Students => _toaProContext.Students;
        public IEnumerable<Submission> Submissions => _toaProContext.Submissions;

        public void RequestAvailability(JudgeAvailability judgeAvailability)
        {
            _toaProContext.Add(judgeAvailability);
            _toaProContext.SaveChanges();
        }

    }
}
