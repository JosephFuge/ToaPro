using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ToaPro.Models;

namespace ToaPro
{
    public class EFIntexRepository : IIntexRepository
    {
        private ToaProContext _intexContext;
        public EFIntexRepository(ToaProContext TempDataDictionary) 
        {
            _intexContext = TempDataDictionary;
        }

        public IEnumerable<Student> Students => (IEnumerable<Student>)_intexContext;
        public IEnumerable<Submission> Submissions => (IEnumerable<Submission>)_intexContext;
    }
}