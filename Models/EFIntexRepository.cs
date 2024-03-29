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



        public IQueryable<Ranking> Rankings => _toaProContext.Rankings;

        public IQueryable<Group> Groups => _toaProContext.Groups;

        public void UpdateRanking(Ranking ranking)
        {
            _toaProContext.Rankings.Update(ranking);
            _toaProContext.SaveChanges();
        }

        public void AddRanking(Ranking ranking)
        {
            _toaProContext.Rankings.Add(ranking);
            _toaProContext.SaveChanges();
        }

        public void StudentRequestAvailability(StudentAvailability studentAvailability)
        {
            _toaProContext.Add(studentAvailability);
            _toaProContext.SaveChanges();
        }
    }
}
