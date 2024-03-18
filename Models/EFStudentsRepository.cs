using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ToaPro.Models;

namespace ToaPro
{
    public class EFIntexRepository : IIntexRepository
    {
        private IntexContext _intexContext;
        public EFIntexRepository(IntexContext TempDataDictionary) 
        {
            _intexContext = TempDataDictionary;
        }

        public IEnumerable<Student> Students => _intexContext;
    }
}