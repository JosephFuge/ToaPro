using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            var group = _repo.Groups.FirstOrDefault();
            List<SubmissionField> fields = _repo.SubmissionFields().ToList();
            List<SubmissionAnswer> answers = fields.Select(f => new SubmissionAnswer
            {
                SubmissionFieldId = f.Id,
                SubmissionField = f,
                GroupId = group?.Id ?? 1
            }).ToList();
            return View("StudentSubmitFiles", answers);
        }


        [HttpGet]
        public IActionResult StudentSubmitFilesConfirmation()
        {

            return View("StudentSubmitFilesConfirmation");
        }


        [HttpGet]
        public IActionResult Submission()
        {
            //var studentId = _repo.Students.StudentId(x => x.StudentId);
            //var groupId = _repo.Groups.GroupId(x => x.GroupId);

            return View("StudentSubmitFiles"); //create new application to get rid of the error that says " is not a valid input

        }
        [HttpPost]
        public async Task<IActionResult> GroupSubmitAnswers(List<SubmissionAnswer> answers)
        {
            List<SubmissionAnswer> newValidAnswers = new List<SubmissionAnswer>();
            List<SubmissionAnswer> updatedValidAnswers = new List<SubmissionAnswer>();

            for (int i = 0; i < answers.Count; i++)
            {
                var tempEntry = ModelState.Where(pair => pair.Key.Contains("TextData") || pair.Key.Contains("FileData")).ElementAtOrDefault(i);
                if ((tempEntry.Key.Contains("TextData") || tempEntry.Key.Contains("FileData")) && tempEntry.Value != null && tempEntry.Value.ValidationState == ModelValidationState.Valid)
                {
                    if (answers[i].Id > 0)
                    {
                        updatedValidAnswers.Add(answers[i]);
                    } else
                    {
                        newValidAnswers.Add(answers[i]);
                    }
                }
            }

            if (newValidAnswers.Count > 0)
            {
                var numberAdded = await _repo.AddSubmissionAnswers(newValidAnswers);
                return View("StudentSubmitFilesConfirmation", newValidAnswers.Count);
            }


            var subFields = _repo.SubmissionFields().Where(sf => answers.Select(a => a.SubmissionFieldId).Contains(sf.Id)).ToList();
            foreach (var answer in answers)
            {
                var answerSubField = subFields.FirstOrDefault(sf => sf.Id == answer.SubmissionFieldId);
                if (answerSubField != null)
                {
                    answer.SubmissionField = answerSubField;
                }
            }
            //ViewBag.Categories = _repo.Submissions.ToList();
            return View("StudentSubmitFiles", answers); // Corrected to return the right view
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
                    bodyText += "• Added " + newFields.Count + " new fields\n";
                }

                if (changedFields.Count > 0)
                {
                    bodyText += "• Updated " + changedFields.Count + " fields\n";
                }

                if (deleteFields.Count > 0)
                {
                    bodyText += "• Deleted " + deleteFields.Count + " fields\n";
                }

                TempData["NotificationTitle"] = "Success";
                TempData["NotificationType"] = NotificationDialogType.Success;
                TempData["NotificationBody"] = bodyText;
            } else if (newFields.Count > 0 || changedFields.Count > 0 || deleteFields.Count > 0)
            {
                TempData["NotificationTitle"] = "Failure";
                TempData["NotificationType"] = NotificationDialogType.Failure;
                TempData["NotificationBody"] = "None of your changes were successfully saved.";
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
