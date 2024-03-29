using Microsoft.AspNetCore.Mvc;

namespace ToaPro.Controllers
{
    public class JudgeRankingsController : Controller
    {
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

        public IActionResult FunAwards2()
        {
            return View();
        }
    }
}
