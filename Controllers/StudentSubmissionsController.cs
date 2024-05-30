using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Infrastructure;
using ToaPro.Models;
using ToaPro.Models.ViewModels;

namespace ToaPro.Controllers
{
    public class StudentSubmissionsController : Controller
    {

        const int currentSemesterId = 1;
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
            List<SubmissionField> subFields = _repo.SubmissionFields(tracking: false).Where(sf => sf.SemesterId == currentSemesterId).OrderBy(sf => sf.Id).ToList();

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

            List<SubmissionField> changedFields = subFields.SubmissionFields
                                                    .Where(sf => subFields.UpdatedSubmissionFieldIds.Contains(sf.Id))
                                                    .ToList();

            var changedIds = subFields.UpdatedSubmissionFieldIds;

            newFields = convertSubmissionDateTimesToUTC(newFields);
            changedFields = convertSubmissionDateTimesToUTC(changedFields);

            List<SubmissionField> deleteFields = _repo.SubmissionFields(tracking: false).Where(sf => subFields.DeleteFieldIds.Contains(sf.Id)).ToList();

            _repo.AddSubmissionFieldList(newFields);
            _repo.UpdateSubmissionFieldList(changedFields);
            _repo.DeleteSubmissionFieldList(deleteFields);
            int numChanged = await _repo.CommitChangesAsync();

            if (numChanged > 0)
            {
                var bodyText = "Successfully completed the following changes:\n";
                if (newFields.Count > 0)
                {
                    bodyText += "• Added " + newFields.Count + " new fields";
                }

                if (changedFields.Count > 0)
                {
                    bodyText += "• Updated " + changedFields.Count + " fields";
                }

                if (deleteFields.Count > 0)
                {
                    bodyText += "• Deleted " + deleteFields.Count + " fields";
                }

                TempData["NotificationTitle"] = "Success!";
                TempData["NotificationBody"] = bodyText;
            }

            return RedirectToAction("StudentSubmissionFields"); 
        }

        private List<SubmissionField> convertSubmissionDateTimesToUTC(List<SubmissionField> submissionFields)
        {
            foreach (var newField in submissionFields)
            {
                if (newField.DueDate.Kind == DateTimeKind.Unspecified)
                {
                    newField.DueDate = DateTime.SpecifyKind(newField.DueDate, DateTimeKind.Utc);
                }

                if (newField.SemesterId <= 0)
                {
                    newField.SemesterId = currentSemesterId;
                }
            }

            return submissionFields;
        }

    }
}
