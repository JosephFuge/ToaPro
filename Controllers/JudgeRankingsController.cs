using Microsoft.AspNetCore.Mvc;
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
        public IActionResult JudgePresentationForm()
        {
            return View();
        }

        public IActionResult ProfessorViewAssignAwards()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CoordinatorAssignJudges()
        {
            var Judges = _repo.Judges
                .OrderBy(x => x.Id).ToList();
            return View(Judges);
        }

        [HttpPost]
        public IActionResult CoordinatorAssignJudges(Judge updatedInfo) // this would be for if the user decide to update a detail
        {
            _repo.UpdateJudgeAvailability(updatedInfo);
            return View();
        }
    }
}
