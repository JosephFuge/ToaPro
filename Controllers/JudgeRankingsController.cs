using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Models;

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

        [HttpGet]
        public IActionResult JudgePresentationForm(int Id = 1)
        {
             ViewBag.joinedData = _repo.Judges
                .Where(x => x.Id == 1)
                .Include(r => r.Rankings)
                .Include(p => p.Presentations)
                    .ThenInclude(g => g.Group)
                .ToList();

            var recordToEdit = _repo.Rankings.Single(x => x.Id == Id);

            return View(recordToEdit);
        }

        [HttpPost]
        public IActionResult JudgePresentationForm(Ranking updatedInfo)
        {
            _repo.UpdateRanking(updatedInfo);

            return RedirectToAction("JudgePresentationForm");
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
