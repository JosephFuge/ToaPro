using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Models;
using ToaPro.Models.ViewModels; // Make sure this matches the namespace of your ViewModel
using System.Linq;

namespace ToaPro.Controllers
{
    public class RubricController : Controller
    {
        private readonly ToaProContext _context;

        public RubricController(ToaProContext context)
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

        public IActionResult AssignTAs()
        {
            var TAAssignmentList = _context.Graders
                .Include(x => x.ToaProUser)
                .Where(x => x.IsProfessor == false)
                .ToList();

            return View(TAAssignmentList);
        }
        [HttpGet]
        public IActionResult AddObjective(int id)
        {
            var Requirement = new Requirement(); // Replace YourModel with the actual model type
            Requirement.ClassId = id; // Set the ClassId property with the passed ID
            return View(Requirement); // Pass the model to the view
        }

        [HttpPost]
        public IActionResult Rubric(int id)
        {
            // Redirect to the AddObjective action method with the class ID as a route parameter
            return RedirectToAction("AddObjective", new { id = id });
        }

        [HttpPost]
        public IActionResult AddObjective(Requirement response)
        {
            if (ModelState.IsValid)
            {
                _context.Requirements.Add(response);
                _context.SaveChanges(); // Save changes to the database
                return RedirectToAction("Rubric");
            }
            // If ModelState is not valid, return the view with validation errors
            return View(response);
        }

    }

}


