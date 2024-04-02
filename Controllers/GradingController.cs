using Microsoft.AspNetCore.Mvc;
using ToaPro.Models.ViewModels;
using System.Threading.Tasks; // Add this namespace for asynchronous operations
using Microsoft.EntityFrameworkCore;
using ToaPro.Models;

namespace ToaPro.Controllers
{
    public class GradingController : Controller
    {
        private IIntexRepository _gradeSummaryRepository;

        public GradingController(IIntexRepository temp)
        {
            _gradeSummaryRepository = temp;
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

            var classes = _gradeSummaryRepository.Classes.ToList();
            var requirements = _gradeSummaryRepository.Requirements.ToList();
            var grades = _gradeSummaryRepository.Grades.ToList();
            var submissions = _gradeSummaryRepository.Submissions.ToList();
            var students = _gradeSummaryRepository.Students.ToList();
            var groups = _gradeSummaryRepository.Groups.ToList();
            var rankings = _gradeSummaryRepository.Rankings.ToList();

            IEnumerable<GradingSummaryViewModel> query = from cls in classes
                                                         join req in requirements on cls.Id equals req.ClassId
                                                         join grd in grades on req.Id equals grd.RequirementId
                                                         join sub in submissions on grd.SubmissionId equals sub.Id
                                                         join stu in students on sub.StudentId equals stu.Id
                                                         join grp in groups on grd.GroupId equals grp.Id
                                                         join rank in rankings on grp.Id equals rank.GroupId
                                                         select new GradingSummaryViewModel
                                                         {
                                                             Class = cls,
                                                             Requirement = req,
                                                             Grade = grd,
                                                             Submission = sub,
                                                             Student = stu,
                                                             Group = grp,
                                                             Rank = rank
                                                         };

            var result = query.ToList();
            return View(result);
        }

        public IActionResult GradingPage()
        {
            //Only Professors and TAs have access to this page. Professors can view which TAs are assigned to which team.
            //TAs are the only ones who can input grades--professors only have view capability when it comes to this.
            //(TAs, Prof)
            return View();
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
                                        .Include(e => e.Submission) // Include related Submission, if necessary
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
