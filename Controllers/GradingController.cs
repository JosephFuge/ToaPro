using Microsoft.AspNetCore.Mvc;

namespace ToaPro.Controllers
{
    public class GradingController : Controller
    {
        private IRepoName _RepoName;

        public GradingController(IRepoName temp)
        {
            _RepoName = temp;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProfessorNormalizeGrades()
        {
            return View();
        }

        public IActionResult GradingSummaryPage()
        {
            //Only Professors and TAs have access to this page. Only Professors can export the files.
            //(TAs, Prof)
            return View();
        }

        public IActionResult GradingPage()
        {
            //Only Professors and TAs have access to this page. Professors can view which TAs are assigned to which team.
            //TAs are the only ones who can input grades--professors only have view capability when it comes to this.
            //(TAs, Prof)
            return View();
        }

        public IActionResult StudentViewGrades()
        {
            return View();
        }

    }
}
