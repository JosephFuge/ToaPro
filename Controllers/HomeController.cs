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
            //Add functionality to load the Index page based on user type (Coord, Prof, Stud, TA, Judge)
            return View();
        }

        public IActionResult CoordinatorChecklist()
        {
            return View();
        }

        public IActionResult ProfessorDashboard()
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

        public IActionResult CoodinatorAssignJudges()
        {
            return View();
        }

    }
}
