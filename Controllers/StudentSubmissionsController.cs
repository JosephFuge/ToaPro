using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IO.Pipes;
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
            List<GroupSubmissionViewModel> answers = fields.Select(f => new GroupSubmissionViewModel
            {
                SubmissionAnswer = new SubmissionAnswer
                {
                    SubmissionFieldId = f.Id,
                    SubmissionField = f,
                    GroupId = group?.Id ?? 1
                }
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
        public async Task<IActionResult> GroupSubmitAnswers(List<GroupSubmissionViewModel> answers)
        {
            (List<SubmissionAnswer> updatedValidAnswers, List<SubmissionAnswer> newValidAnswers) =  await SortSubmittedAnswers(ModelState, answers);
            
            if (newValidAnswers.Count > 0)
            {
                var numberAdded = await _repo.AddSubmissionAnswers(newValidAnswers);
                return View("StudentSubmitFilesConfirmation", newValidAnswers.Count);
            }

            var subFields = _repo.SubmissionFields().Where(sf => answers.Select(a => a.SubmissionAnswer.SubmissionFieldId).Contains(sf.Id)).ToList();
            foreach (var answer in answers)
            {
                var answerSubField = subFields.FirstOrDefault(sf => sf.Id == answer.SubmissionAnswer.SubmissionFieldId);
                if (answerSubField != null)
                {
                    answer.SubmissionAnswer.SubmissionField = answerSubField;
                }
            }
            //ViewBag.Categories = _repo.Submissions.ToList();
            return View("StudentSubmitFiles", answers); // Corrected to return the right view
        }

        private async Task<(List<SubmissionAnswer> updatedValidAnswers, List<SubmissionAnswer> newValidAnswers)> SortSubmittedAnswers(ModelStateDictionary modelState, List<GroupSubmissionViewModel> answers)
        {
            const int MAX_FILE_SIZE = 2097152; // 2 MB
            List<SubmissionAnswer> updatedValidAnswers = new List<SubmissionAnswer>();
            List<SubmissionAnswer> newValidAnswers = new List<SubmissionAnswer>();

            var validAnswers = ModelState.Where(pair => (pair.Key.Contains("TextData") || pair.Key.Contains("UploadFile"))
                                                && pair.Value != null && pair.Value.ValidationState == ModelValidationState.Valid);
            
            if (validAnswers != null)
            {
                for (int i = 0; i < validAnswers.Count(); i++)
                {
                    var tempEntry = validAnswers.ElementAt(i);
                    // Parse out the index of the new or updated answer
                    // The Key is assumed to be in the form of "[index].UploadFile" or "[index].SubmissionAnswer.TextData"
                    int answerIndex = -1;
                    var stringIndex = tempEntry.Key[(tempEntry.Key.IndexOf('[') + 1)..tempEntry.Key.IndexOf(']')];
                    bool successfulParse = int.TryParse(stringIndex, out answerIndex);

                    if (successfulParse && answerIndex >= 0)
                    {
                        bool successful = true;
                        if (answers[answerIndex].UploadFile != null)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                if (answers[answerIndex].UploadFile.Length < MAX_FILE_SIZE)
                                {
                                    await answers[answerIndex].UploadFile.CopyToAsync(memoryStream);

                                    answers[answerIndex].SubmissionAnswer.FileData = memoryStream.ToArray();
                                    answers[answerIndex].SubmissionAnswer.TextData = answers[answerIndex].UploadFile.ContentType;
                                }
                                else
                                {
                                    successful = false;
                                    ModelState.AddModelError("[" + answerIndex.ToString() + "].UploadFile", "The file is too large. Must be smaller than " + MAX_FILE_SIZE / (1024 * 1024) + " MB.");
                                }

                            }
                        }

                        if (successful)
                        {
                            if (answers[answerIndex].SubmissionAnswer.Id > 0)
                            {
                                updatedValidAnswers.Add(answers[answerIndex].SubmissionAnswer);
                            } else
                            {
                                newValidAnswers.Add(answers[answerIndex].SubmissionAnswer);
                            }
                        }
                    }
                }
            }
                                                


            return (updatedValidAnswers, newValidAnswers);
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
