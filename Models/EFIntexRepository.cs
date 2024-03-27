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

        public IEnumerable<Class> Classes => (IEnumerable<Class>)_toaProContext;
        public IEnumerable<Grade> Grades => (IEnumerable<Grade>)_toaProContext;
        public IEnumerable<Grader> Graders => (IEnumerable<Grader>)_toaProContext;
        public IEnumerable<Group> Groups => (IEnumerable<Group>)_toaProContext;
        public IEnumerable<Judge> Judges => (IEnumerable<Judge>)_toaProContext;
        public IEnumerable<Presentation> Presentations => (IEnumerable<Presentation>)_toaProContext;
        public IEnumerable<Ranking> Rankings => (IEnumerable<Ranking>)_toaProContext;
        public IEnumerable<Requirement> Requirements => (IEnumerable<Requirement>)_toaProContext;
        public IEnumerable<Semester> Semesters => (IEnumerable<Semester>)_toaProContext;
        public IEnumerable<Student> Students => (IEnumerable<Student>)_toaProContext;
        public IEnumerable<Submission> Submissions => (IEnumerable<Submission>)_toaProContext;
    }
}