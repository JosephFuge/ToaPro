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

        public IEnumerable<Student> Students => (IEnumerable<Student>)_toaProContext;
        public IEnumerable<Submission> Submissions => (IEnumerable<Submission>)_toaProContext;
        public IEnumerable<Judge> Judges => (IEnumerable<Judge>)_toaProContext;
        public IEnumerable<Presentation> Presentations => (IEnumerable<Presentation>)_toaProContext;

        public IEnumerable<JudgeAvailability> JudgeAvailabilities => (IEnumerable<JudgeAvailability>)_toaProContext;

        public void RequestAvailability(JudgeAvailability judgeAvailability)
        {
            _toaProContext.Add(judgeAvailability);
            _toaProContext.SaveChanges();
        }
    }
}
