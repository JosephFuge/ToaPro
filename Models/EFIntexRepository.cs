using ToaPro.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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


        public void RequestAvailability(Judge Id)
        {
            var recordToEdit = _toaProContext.Judges
            .Single(x => x.Id == Id);

            _toaProContext.SaveChanges();
        }
        public void UpdateAvailability(Judge updatedInfo)
        {
            _toaProContext.Update(updatedInfo);
            _toaProContext.SaveChanges();
        }

        //public void StudentRequestAvailability(StudentAvailability studentAvailability)
        //{
        //    _toaProContext.Add(studentAvailability);
        //    _toaProContext.SaveChanges();
        //}
    }
}
