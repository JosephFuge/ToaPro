using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ToaPro.Controllers
{
    public class JudgeRankingsController : Controller
    {
        private IIntexRepository _repo;
        
        public JudgeRankingsController(IIntexRepository temp)
        {
            _repo = temp;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult JudgePresentationForm()
        {
             var joinedData = _repo.Judges
                .Where(x => x.Id == 1)
                .Include(p => p.Presentations)
                    .ThenInclude(g => g.Group)
                .ToList();

            return View(joinedData);
        }

        public IActionResult ProfessorViewAssignAwards()
        {
            return View();
        }

        public IActionResult CoordinatorAssignJudges()
        {
            return View();
        }
    }
}
