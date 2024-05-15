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
        private readonly IIntexRepository _repo;
        private readonly UserManager<ToaProUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(
            IIntexRepository repository, SignInManager<ToaProUser> signInManager, RoleManager<IdentityRole> roleManager
            )
        {
            _repo = repository;
            _userManager = signInManager.UserManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Users");
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var students = await _repo.Students.Include(s => s.ToaProUser).Include(s => s.Group).ToListAsync();
            var roleNames = await _roleManager.Roles.Select(r => r.Name).Where(n => !string.IsNullOrEmpty(n)).ToListAsync();
            var roleUsers = new Dictionary<string, IList<ToaProUser>>();
            foreach (var roleName in roleNames)
            {
                if (string.IsNullOrEmpty(roleName)) continue;
                var users = await _userManager.GetUsersInRoleAsync(roleName);
                roleUsers[roleName] = users.ToList();
            }
            var userRoles = new UsersViewModel
            {
                RoleUsers = roleUsers,
                Students = students
            };
            return View(userRoles);
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
                            var uploader = new UserBulkUploader(_userManager, _repo);
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

                    return RedirectToAction("Users");
                }
            }

            return RedirectToAction("Users");
        }
    }
}
