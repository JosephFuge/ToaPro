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
                Class1Requirements = requirements.Where(r => r.ClassId == 1).OrderBy(r => r.Id).ToList(),
                Class2Requirements = requirements.Where(r => r.ClassId == 2).OrderBy(r => r.Id).ToList(),
                Class3Requirements = requirements.Where(r => r.ClassId == 3).OrderBy(r => r.Id).ToList(),
                Class4Requirements = requirements.Where(r => r.ClassId == 4).OrderBy(r => r.Id).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Rubric(int id)
        {
            // Redirect to the AddObjective action method with the class ID as a route parameter
            return RedirectToAction("AddObjective", new { id = id });
        }
        [HttpPost]
        public IActionResult EditObjective(int id, string description, int points)
        {
            try
            {
                var objective = _context.Requirements.Find(id);
                if (objective == null)
                {
                    return Json(new { success = false, message = "Objective not found" });
                }

                objective.Description = description;
                objective.Points = points;
                _context.SaveChanges();

                return Json(new { success = true, message = "Objective updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public IActionResult DeleteObjective(int id)
        {
            try
            {
                var objective = _context.Requirements.Find(id);
                if (objective == null)
                {
                    return Json(new { success = false, message = "Objective not found" });
                }

                _context.Requirements.Remove(objective);
                _context.SaveChanges();

                return Json(new { success = true, message = "Objective deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public IActionResult AddObjective(string description, int points, int classId)
        {
            try
            {
                var newObjective = new Requirement
                {
                    Description = description,
                    Points = points,
                    ClassId = classId
                };

                _context.Requirements.Add(newObjective);
                _context.SaveChanges();

                return Json(new { success = true, message = "Objective added successfully" });
            }
            catch (Exception ex)
            {
                // Inner exception handling
                if (ex.InnerException != null)
                {
                    return Json(new { success = false, message = $"An error occurred: {ex.Message}. Inner exception: {ex.InnerException.Message}" });
                }
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }



    }

}


