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

        public IEnumerable<Student> Students => (IEnumerable<Student>)_toaProContext;
        public IEnumerable<Submission> Submissions => (IEnumerable<Submission>)_toaProContext;

        public IEnumerable<Judge> Judges => _toaProContext.Judges;

        public IEnumerable<Presentation> Presentations => _toaProContext.Presentations;
    }
}