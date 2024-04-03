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
                    assignmentDict[taa.ToaProUser.Id].Add(new List<string>());
                }
                assignmentDict[taa.ToaProUser.Id][0].Add(taa.ToaProUser.FirstName + " " + taa.ToaProUser.LastName);

                var TAGroupAndAssignmentList = _context.GraderAssigns
                    .Include(x => x.Group)
                    .Include(x => x.Requirement)
                    .Where(x => x.graderId == taa.ToaProUser.Id)
                    .ToList();

                foreach (GraderAssign ga in TAGroupAndAssignmentList)
                {
                    if (ga.groupId != 0)
                    {
                        assignmentDict[taa.ToaProUser.Id][1].Add(ga.Group.Section.ToString() + "-" + ga.Group.Number.ToString());
                    }
                    else
                    {
                        assignmentDict[taa.ToaProUser.Id][2].Add(ga.Requirement.Description);
                    }
                }
            }

            // Get all unique group numbers
            var allGroups = _context.Groups.Select(g => g.Section.ToString() + "-" + g.Number.ToString()).Distinct().ToList();
            ViewBag.AllGroups = allGroups;

            // Get all requirements for the class
            var classId = 2; // Change this to the class ID you want, based on the class the professor is in charge of
            var allRequirements = _context.Requirements
                .Where(r => r.ClassId == classId)
                .Select(r => r.Description)
                .ToList();
            ViewBag.AllRequirements = allRequirements;

            return View(assignmentDict);
        }


        public class UpdateAssignmentsModel
        {
            public string TaKey { get; set; }
            public List<string> Groups { get; set; }
            public List<string> Requirements { get; set; }
        }

        [HttpPost]
        public IActionResult UpdateAssignments([FromBody] UpdateAssignmentsModel model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.TaKey))
                {
                    return Json(new { success = false, error = "Grader not found" });
                }

                var existingAssignments = _context.GraderAssigns
                    .Where(ga => ga.graderId == model.TaKey)
                    .Include(ga => ga.Group) // Making sure to include the Group data
                    .Include(ga => ga.Requirement) // Also include the Requirement data
                    .ToList();


                // Remove existing assignments that are no longer selected
                foreach (var assignment in existingAssignments)
                {
                    if (assignment.groupId != 0 && !model.Groups.Contains(assignment.Group.Section.ToString() + "-" + assignment.Group.Number.ToString()))
                    {
                        _context.GraderAssigns.Remove(assignment);
                    }
                    else if (assignment.requirementId != 0 && !model.Requirements.Contains(assignment.Requirement.Description))
                    {
                        _context.GraderAssigns.Remove(assignment);
                    }
                }

                // Add new assignments
                foreach (var group in model.Groups)
                {
                    var groupParts = group.Split('-');
                    var section = int.Parse(groupParts[0]);
                    var number = int.Parse(groupParts[1]);

                    var existingGroup = existingAssignments.FirstOrDefault(ga => ga.Group.Section == section && ga.Group.Number == number);
                    if (existingGroup == null)
                    {
                        var newGroup = _context.Groups.FirstOrDefault(g => g.Section == section && g.Number == number);
                        if (newGroup != null)
                        {
                            _context.GraderAssigns.Add(new GraderAssign
                            {
                                graderId = model.TaKey,
                                groupId = newGroup.Id,
                                requirementId = 0
                            });
                        }
                    }
                }

                // Add new requirements
                foreach (var requirement in model.Requirements)
                {
                    var existingRequirement = existingAssignments.FirstOrDefault(ga => ga.Requirement.Description == requirement);
                    if (existingRequirement == null)
                    {
                        var newRequirement = _context.Requirements.FirstOrDefault(r => r.Description == requirement);
                        if (newRequirement != null)
                        {
                            _context.GraderAssigns.Add(new GraderAssign
                            {
                                graderId = model.TaKey,
                                groupId = 0,
                                requirementId = newRequirement.Id
                            });
                        }
                    }
                }

                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;

                // Check if there is an inner exception
                if (ex.InnerException != null)
                {
                    // Append the inner exception message to the error message
                    errorMessage += $" Inner exception: {ex.InnerException.Message}";
                }

                return Json(new { success = false, error = errorMessage });
            }

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


