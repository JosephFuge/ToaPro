using Microsoft.AspNetCore.Mvc;

namespace ToaPro.Controllers
{
    public class StudentSubmissionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StudentSubmitPeerEvals()
        {
            return View();
        }

        public IActionResult StudentPeerEvalThanks()
        {
            return View();
        }

        public IActionResult StudentSubmitFiles()
        {
            return View();
        }

    }
}
