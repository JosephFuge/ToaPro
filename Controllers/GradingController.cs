﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Infrastructure;
using ToaPro.Models;
using ToaPro.Models.ViewModels;

namespace ToaPro.Controllers
{
    public class GradingController : Controller
    {
        private IIntexRepository _gradeSummaryRepository;
        private SignInManager<ToaProUser> _signInManager;
        private int SEMESTER_ID = 1;

        public GradingController(IIntexRepository tempRepo, SignInManager<ToaProUser> signInManager)
        {
            _gradeSummaryRepository = tempRepo;
            _signInManager = signInManager;
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

            //var query = from cls in _gradeSummaryRepository.Classes
            //            join req in _gradeSummaryRepository.Requirements on cls.Id equals req.ClassId
            //            join grd in _gradeSummaryRepository.Grades on req.Id equals grd.RequirementId
            //            join sub in _gradeSummaryRepository.Submissions on grd.SubmissionId equals sub.Id
            //            join stu in _gradeSummaryRepository.Students on sub.StudentId equals stu.Id
            //            join grp in _gradeSummaryRepository.Groups on grd.GroupId equals grp.Id
            //            join rank in _gradeSummaryRepository.Rankings on grp.Id equals rank.GroupId
            //            select new
            //            {
            //                Class = cls,
            //                Requirement = req,
            //                Grade = grd,
            //                Submission = sub,
            //                Student = stu,
            //                Group = grp,
            //                Rank = rank
            //            };
            //var result = query.ToList();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GradeSubmission(int groupId)
        {
            //Only Professors and TAs have access to this page. Professors can view which TAs are assigned to which team.
            //TAs are the only ones who can input grades--professors only have view capability when it comes to this.
            //(TAs, Prof)

            ToaProUser? currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);

            Group? group = _gradeSummaryRepository.Groups.Where(g => g.Id == groupId).FirstOrDefault();

            List<Requirement> requirements = _gradeSummaryRepository.Requirements.Include(r => r.Class).Where(r => r.Class.SemesterId == SEMESTER_ID).ToList();
            List<Grade> grades = _gradeSummaryRepository.Grades.Where(grade => grade.GroupId == groupId).Include(g => g.Group).Include(g => g.Requirement).ThenInclude(r => r.Class).ToList();

            requirements.ForEach(requirement => { 
                if (!grades.Any(g => g.RequirementId == requirement.Id))
                {
                    grades.Add(new Grade
                    {
                        RequirementId = requirement.Id,
                        Requirement = requirement,
                        GroupId = groupId,
                        Group = group,
                        GraderId = currentUser?.Id
                    });
                }
            });

            List<ClassGradesViewModel> classGrades = [];

            grades.ForEach(grade =>
            {
                string classDisplayName = grade.Requirement.Class.Code + " - " + grade.Requirement.Class.Description;
                if (!classGrades.Any(cg => cg.ClassName == classDisplayName))
                {
                    classGrades.Add(new ClassGradesViewModel { ClassName = classDisplayName, Grades = new List<Grade>() });
                }

                classGrades.Find(cg => cg.ClassName == classDisplayName)?.Grades.Add(grade);
            });

            List<SubmissionAnswer> answers = _gradeSummaryRepository.SubmissionAnswers.Where(answer => answer.GroupId == groupId).Include(answer => answer.SubmissionField).ToList();

            SubmissionGradingViewModel viewModel = new SubmissionGradingViewModel
            {
                Grades = grades,
                SubmissionAnswers = answers,
                ClassGrades = classGrades
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GradeSubmission(SubmissionGradingViewModel submissionGrades)
        {
            // Inaccurate way to check if it was updated; any pre-existing grades will always be marked as updated even with same values. 
            // TODO: Track updates via ID list or by fetching from database and comparing
            ClassGradesViewModel? classGrades = submissionGrades.ClassGrades.FirstOrDefault();
            if (classGrades != null)
            {
                List<Grade> newGrades = classGrades.Grades.Where(g => g.Id == 0 && g.Points != null).ToList();

                List<Grade> updatedGrades = classGrades.Grades.Where(g => g.Id != 0 && g.Points != null).ToList(); 

                int gradedGroupId = classGrades.Grades.FirstOrDefault()?.GroupId ?? 0;

                bool saveData = false;
                if (newGrades.Count > 0)
                {
                    _gradeSummaryRepository.AddGrades(newGrades);
                    saveData = true;
                }
                
                if (updatedGrades.Count > 0)
                {
                    _gradeSummaryRepository.UpdateGrades(updatedGrades);
                    saveData = true;
                }

                if (saveData)
                {
                    await _gradeSummaryRepository.CommitChangesAsync();
                }
                
                return RedirectToAction("GradeSubmission", new { groupId = gradedGroupId });
            }

            return RedirectToAction("GradeSubmission");
        }

        public IActionResult PeerEvalDetails(int evaluationId)
        {
            //Only Professors and TAs have access to this page. Professors can view which TAs are assigned to which team.
            //TAs are the only ones who can input grades--professors only have view capability when it comes to this.
            //(TAs, Prof)
            using (var context = new ToaProContext())
            {
                // Then use the context to find the evaluation with the provided ID
                var evaluation = context.Evaluations
                                        .Include(e => e.Group) // Include related Group, if necessary
                                        .ThenInclude(g => g.Students) // Include related Students, if necessary
                                        .FirstOrDefault(e => e.EvaluationId == evaluationId);
                if (evaluation == null)
                {
                    // Handle the case where the evaluation is not found
                    return View("Error"); // Return an error view or another appropriate response
                }

                // If the evaluation is found, pass it to the view
                return View(evaluation);
            }
        }

        public IActionResult StudentViewGrades()
        {
            return View();
        }

    }
}
