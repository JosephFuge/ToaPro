using Microsoft.AspNetCore.Mvc;
using ToaPro.Models;

namespace ToaPro.Controllers
{
    public class StudentSubmissionsController : Controller
    {

        private IStudentsRepository _repo;

        public StudentSubmissionsController(IStudentsRepository temp) //Constructor
        {
            _repo = temp;
        }
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

        [HttpGet]
        public IActionResult StudentSubmitFiles()
        {

            return View("StudentSubmitFiles");
        }

        [HttpPost]
        public IActionResult StudentSubmitFiles(Submission response)
        {
            if (ModelState.IsValid)
            {
                _repo.StudentSubmitFiles(response);

                return View("StudentSubmitFilesConfirmation", response);
            }
            else
            {
                ViewBag.Categories = _repo.Categories.OrderBy(x => x.CategoryName).ToList();
                return View(response);
            }
        }

    }
}
