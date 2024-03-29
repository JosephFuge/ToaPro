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

        public IQueryable<Judge> Judges => _toaProContext.Judges;

        public IQueryable<Presentation> Presentations => _toaProContext.Presentations;

        public IQueryable<Ranking> Rankings => _toaProContext.Rankings;

        public void UpdateRanking(Ranking ranking)
        {
            _toaProContext.Rankings.Update(ranking);
            _toaProContext.SaveChanges();
        }
    }
}