using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Models;
using ToaPro.Models.ViewModels; // Make sure this matches the namespace of your ViewModel
using System.Linq;

namespace ToaPro.Controllers
{
    public class RubricController : Controller
    {
        private readonly IntexContext _context;

        public RubricController(IntexContext context)
        {
            _context = context;
        }

        public IActionResult Rubric()
        {
            // Fetching and filtering requirements for four different classes
            var requirements = _context.Requirements.Include(r => r.Class).ToList();

            var viewModel = new RequirementViewModel
            {
                Class1Requirements = requirements.Where(r => r.ClassId == 1).ToList(),
                Class2Requirements = requirements.Where(r => r.ClassId == 2).ToList(),
                Class3Requirements = requirements.Where(r => r.ClassId == 3).ToList(),
                Class4Requirements = requirements.Where(r => r.ClassId == 4).ToList()
            };

            return View(viewModel);
        }
    }
}
