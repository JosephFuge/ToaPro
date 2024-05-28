using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Infrastructure;
using ToaPro.Models;
using ToaPro.Models.ViewModels;

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

        [HttpGet]
        public IActionResult StudentSubmissionFields()
        {
            const int currentSemesterId = 1;
            List<SubmissionField> subFields = _repo.SubmissionFields.Where(sf => sf.SemesterId == currentSemesterId).ToList();

            var subFieldFrequencies = new List<int>();

            for (int i = 0; i < subFields.Count; i++)
            {
                subFieldFrequencies.Add(0);
            }

            var currentSemester = _repo.Semesters.FirstOrDefault(s => s.Id == currentSemesterId);

            String currentTermYear = currentSemester != null ? " - " + currentSemester.Term + " " + currentSemester.Year : "";

            var subFieldsViewModel = new SubmissionFieldsViewModel
            {
                SubmissionFields = subFields,
                SubmissionFieldFrequencies = subFieldFrequencies,
                TermYear = currentTermYear
            };

            return View(subFieldsViewModel); 

        }


        [HttpPost]
        public async Task<IActionResult> StudentSubmissionFields(SubmissionFieldsViewModel subFields)
        {
            const int currentSemesterId = 1;

            List<SubmissionField> newFields = subFields.SubmissionFields
                                                .Where(sf => sf.Id == 0)
                                                .ToList();

            foreach (var newField in newFields)
            {
                if (newField.DueDate.Kind == DateTimeKind.Unspecified)
                {
                    newField.DueDate = newField.DueDate.ToUniversalTime();
                    // newField.DueDate = DateTime.SpecifyKind(newField.DueDate, DateTimeKind.Utc);
                }

                newField.SemesterId = currentSemesterId;
            }


            await _repo.AddSubmissionFieldList(newFields);

            return RedirectToAction("StudentSubmissionFields"); 
        }

    }
}
