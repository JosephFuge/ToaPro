using ToaPro.Models;
using System.Linq;

namespace ToaPro
{
    public class EFIntexRepository : IIntexRepository
    {
        private ToaProContext _context;

        public EFIntexRepository(ToaProContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> Students => _context.Students.ToList();
        public IEnumerable<Submission> Submissions => _context.Submissions.ToList();

        public void AddSubmission(Submission submission)
        {
            _context.Submissions.Add(submission);
            _context.SaveChanges();
        }
    }
}
