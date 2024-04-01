using Microsoft.AspNetCore.Mvc;

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

        public IActionResult CoordinatorAssignJudges()
        { 

            return View();
        }
    }
}
