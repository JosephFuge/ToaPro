using Microsoft.AspNetCore.Mvc;
using ToaPro.Models.ViewModels;
using ToaPro.Infrastructure;
using Microsoft.AspNetCore.Identity;
using ToaPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace ToaPro.Controllers
{
    public class UserManagementController : Controller
    {
        private IIntexRepository _repo;
        private SignInManager<ToaProUser> _signInManager;

        public UserManagementController(IIntexRepository repository, SignInManager<ToaProUser> signInManager)
        {
            _repo = repository;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("UserRoles");
        }

        [HttpGet]
        public async Task<IActionResult> UserRoles(string selectedRole = "Student")
        {
            Dictionary<Student, IList<string>>? studentRoles = null;
            Dictionary<ToaProUser, IList<string>> userRoles = new Dictionary<ToaProUser, IList<string>>();
            if (selectedRole == "Student")
            {
                List<Student> students = _repo.Students.Include(s => s.ToaProUser).Include(s => s.Group).ToList();
                studentRoles = new Dictionary<Student, IList<string>>();
                foreach (var student in students)
                {
                    var roles = await _signInManager.UserManager.GetRolesAsync(student.ToaProUser);
                    if (roles != null)
                    {
                        studentRoles.Add(student, roles);
                    }
                    else
                    {
                        studentRoles.Add(student, new List<string>());
                    }
                }
            } else
            {
                var users = await _signInManager.UserManager.GetUsersInRoleAsync(selectedRole);
                if (users != null)
                {
                    foreach (var user in users)
                    {
                        var roles = await _signInManager.UserManager.GetRolesAsync(user);
                        if (roles != null)
                        {
                            userRoles.Add(user, roles);
                        }
                        else
                        {
                            userRoles.Add(user, new List<string>());
                        }
                    }
                }
            }
            

            UserRolesViewModel userRolesViewModel = new UserRolesViewModel
            {
                SelectedRole = UserRole.Student,
                UserRoles = userRoles,
                StudentRoles = studentRoles
            };

            return View(userRolesViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Upload(FileUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CsvFile != null && model.CsvFile.Length > 0 && model.UserRole == UserRole.Student)
                {
                    var users = new List<StudentImportFormat>();

                    using (var reader = new StreamReader(model.CsvFile.OpenReadStream()))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = "\t" // Set the correct delimiter here
                    }))
                    {
                        users = csv.GetRecords<StudentImportFormat>().ToList();
                        
                        if (users != null)
                        {
                            var uploader = new UserBulkUploader(_signInManager.UserManager, _repo);
                            var students = await uploader.CreateStudentsFromImport(users);
                            if (students.Count > 0)
                            {
                                ViewBag.UploadSuccess = true;
                            } else
                            {
                                ViewBag.UploadSuccess = false;
                            }
                        }
                    }


                    // TODO: Bulk import users to the database
                    // Example: _userService.BulkImportUsers(users);

                    return RedirectToAction("UserRoles");
                }
            }

            return RedirectToAction("UserRoles");
        }
    }
}
