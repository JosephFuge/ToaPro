using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToaPro.Models;

namespace ToaPro.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<ToaProUser> _signInManager;
        //Login Page
        public IActionResult Login()
        {
            return View();
        }

        public HomeController(SignInManager<ToaProUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            //var currentUser = _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return View();
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
            //Add functionality to load the Index page based on user type (Coord, Prof, Stud, TA, Judge)

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

        public IActionResult ProfessorIndex()
        {
            return View();
        }

    }
}
