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
            return View();
        }
    }
}
