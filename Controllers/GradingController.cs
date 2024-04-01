using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Models;

namespace ToaPro.Controllers
{
    public class GradingController : Controller
    {
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
