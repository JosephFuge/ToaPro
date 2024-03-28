using ToaPro.Models;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<Judge> Judges => _toaProContext.Judges.ToList();
        public IEnumerable<Presentation> Presentations => _toaProContext.Presentations;

        public IEnumerable<JudgeAvailability> JudgeAvailabilities => _toaProContext.JudgeAvailabilities;

        public void RequestAvailability(JudgeAvailability judgeAvailability)
        {
            _toaProContext.Add(judgeAvailability);
            _toaProContext.SaveChanges();
        }
    }
}
