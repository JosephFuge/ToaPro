using Microsoft.AspNetCore.Mvc;

namespace ToaPro.Controllers
{
    public class RubricController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Rubric()
        {
            //only Professors and TAs have access to this page. Only Professors can export the files.
            //(TAs, Prof)
            return View();
        }

        public IActionResult AssignTAs()
        {
            //Only Professors can make and update assignments while TAs can only view their assignments (TAs, Prof)
            return View();
        }
    }
}
