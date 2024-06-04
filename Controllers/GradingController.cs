using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Infrastructure;
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
