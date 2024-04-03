using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Models;

namespace ToaPro.Controllers
{
    public class StudentSubmissionsController : Controller
    {

        private IIntexRepository _repo;

        public StudentSubmissionsController(IIntexRepository temp) //Constructor
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


        [HttpGet]
        public IActionResult StudentSubmitFilesConfirmation()
        {

            return View("StudentSubmitFilesConfirmation");
        }


        [HttpGet]
        public IActionResult Submission()
        {
            ViewBag.Categories = _repo.Submissions
                .OrderBy(x => x.GithubLink)
            .ToList();

            //var studentId = _repo.Students.StudentId(x => x.StudentId);
            //var groupId = _repo.Groups.GroupId(x => x.GroupId);

            return View("StudentSubmitFiles", new Submission()); //create new application to get rid of the error that says " is not a valid input

        }
        [HttpPost]
        public IActionResult StudentSubmitFiles(Submission response)
        {
            if (ModelState.IsValid)
            {
                _repo.AddSubmission(response); // Corrected line

                return View("StudentSubmitFilesConfirmation", response);
            }
            else
            {
                //ViewBag.Categories = _repo.Submissions.ToList();
                return View(response); // Corrected to return the right view
            }
        }

    }
}
