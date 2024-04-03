using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Models;
using ToaPro.Models.ViewModels; // Make sure this matches the namespace of your ViewModel
using System.Linq;
using System.Formats.Tar;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ToaPro.Controllers
{
    public class RubricController : Controller
    {
        private readonly ToaProContext _context;
        private readonly UserManager<ToaProUser> _userManager;

        public RubricController(ToaProContext context, UserManager<ToaProUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            Dictionary<string, List<List<string>>> assignmentDict = new Dictionary<string, List<List<string>>>();

            var TAAssignmentList = _context.Graders
                .Include(x => x.ToaProUser)
                .Include(x => x.GraderAssigns)
                .Where(x => x.IsProfessor == false)
                .ToList();

            foreach (Grader taa in TAAssignmentList)
            {
                if (!assignmentDict.ContainsKey(taa.ToaProUser.Id))
                {
                    assignmentDict.Add(taa.ToaProUser.Id, new List<List<string>>());
                    assignmentDict[taa.ToaProUser.Id].Add(new List<string>());
                    assignmentDict[taa.ToaProUser.Id].Add(new List<string>());
                }

                assignmentDict[taa.ToaProUser.Id][0].Add(taa.ToaProUser.FirstName + " " + taa.ToaProUser.LastName);
                // Add user to first array

                var TAGroupAndAssignmentList = _context.GraderAssigns
                    .Include(x => x.Group)
                    .Include(x => x.Requirement)
                    .Where(x => x.graderId == taa.ToaProUser.Id)
                    .ToList();

                foreach (GraderAssign ga in TAGroupAndAssignmentList)
                {
                    assignmentDict[taa.ToaProUser.Id][1].Add(ga.Group.Section.ToString() + ga.Group.Number.ToString());
                    assignmentDict[taa.ToaProUser.Id][2].Add(ga.Requirement.Description + '\n');
                }
            }

            return View(assignmentDict);
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


