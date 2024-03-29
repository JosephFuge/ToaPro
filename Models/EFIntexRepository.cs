using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ToaPro.Models;

namespace ToaPro
{
    public class EFIntexRepository : IIntexRepository
    {
        private ToaProContext _toaProContext;
        public EFIntexRepository(ToaProContext TempDataDictionary) 
        {
            _toaProContext = TempDataDictionary;
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