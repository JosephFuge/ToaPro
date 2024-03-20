using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToaPro.Models;

namespace ToaPro.Controllers
{
    public class HomeController : Controller
    {
        //Login Page
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            //Add functinality to load the Index page based on user type (Coord, Prof, Stud, TA, Judge)
            return View();
        }

        public IActionResult CoordinatorChecklist()
        {
            return View();
        }

        public IActionResult PresentationSchedule()
        {
            //Add functinality to load the presentation page based on user type (Coord, Judge, Prof)
            //Coordinator can edit presentation schedule and judges can request new timeslots
            return View();
        }

        public IActionResult CoordinatorAssignJudges()
        {
            return View();
        }

        public IActionResult ProfessorDashboard()
        {
            return View();
        }

        public IActionResult Rubric() 
        {
            //only Professors and TAs have access to this page. Only Professors can export the files.
            //(TAs, Prof)
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

        public IActionResult AssignTAs()
        {
            //Only Professors can make and update assignments while TAs can only view their assignments (TAs, Prof)
            return View();
        }

        public IActionResult ProfessorViewAssignAwards()
        {
            return View();
        }

        public IActionResult ProfessorEditViewUserPermissions()
        {
            //THis is the main list of all users (with buttons to edit and filter by section)
            return View();
        }

        public IActionResult ProfessorAddUser() 
        {
            return View();
        }

        public IActionResult ProfessorEditUser()
        {
            return View();
        }

        public IActionResult StudentSubmitFiles()
        {
            return View();
        }

        public IActionResult StudentViewSchedule() 
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

        public IActionResult RequestNewTime()
        {
            //This view can be accessed by both the Student and Judge. Both will have the same functionality; however,
            //the student view will populate a text area box and a notice at the bottom (see notes in the RequestNewTime.cshtml file).
            return View();
        }

        public IActionResult StudentViewGrades() 
        {
            return View();
        }

        public IActionResult JudgePresentationForm()
        {
            return View();
        }
    }
}
