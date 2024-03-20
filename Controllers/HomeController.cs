using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToaPro.Models;

namespace ToaPro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
