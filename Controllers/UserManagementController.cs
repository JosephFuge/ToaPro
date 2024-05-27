using System.Collections;
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

        public UserManagementController(IIntexRepository repository, SignInManager<ToaProUser> signInManager)
        {
            _repo = repository;
            _userManager = signInManager.UserManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Users");
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var students = await _repo.Students
                .Include(s => s.ToaProUser)
                .Include(s => s.Group)
                .ToListAsync();
            var userRoles = new Dictionary<ToaProUser, IList<string>>();
            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user] = roles;
            }
            var model = new UsersViewModel
            {
                UserRoles = userRoles,
                Students = students
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Upload(FileUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CsvFile != null && model.CsvFile.Length > 0) {
                    if (model.UserRole == UserRole.Student)
                    {
                        ViewBag.UploadSuccess = await UploadStudents(model.CsvFile);
                    } else if (model.UserRole == UserRole.TA)
                    {
                        ViewBag.UploadSuccess = await UploadTAs(model.CsvFile);
                    } else if (model.UserRole == UserRole.Judge)
                    {
                        ViewBag.UploadSuccess = await UploadJudges(model.CsvFile);
                    }

                    // TODO: Show success/failure dialog depending on results of bulk upload

                    return RedirectToAction("Users");
                }
            }

            return RedirectToAction("Users");
        }

        private async Task<bool> UploadStudents(IFormFile CsvFile)
        {
            try
            {
                var users = new List<StudentImportFormat>();

                using (var reader = new StreamReader(CsvFile.OpenReadStream()))
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
                        return students.Count > 0;
                    }
                }
            } catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        private async Task<bool> UploadTAs(IFormFile CsvFile)
        {
            try
            {
                var users = new List<TAImportFormat>();

                using (var reader = new StreamReader(CsvFile.OpenReadStream()))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = "\t" // Set the correct delimiter here
                }))
                {
                    users = csv.GetRecords<TAImportFormat>().ToList();

                    if (users != null)
                    {
                        var uploader = new UserBulkUploader(_userManager, _repo);
                        var TAs = await uploader.CreateTAsFromImport(users);
                        return TAs.Count > 0;
                    }
                }
            } catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        private async Task<bool> UploadJudges(IFormFile CsvFile)
        {
            try
            {
                var users = new List<JudgeImportFormat>();

                using (var reader = new StreamReader(CsvFile.OpenReadStream()))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = "\t" // Set the correct delimiter here
                }))
                {
                    users = csv.GetRecords<JudgeImportFormat>().ToList();

                    if (users != null)
                    {
                        var uploader = new UserBulkUploader(_userManager, _repo);
                        var judges = await uploader.CreateJudgesFromImport(users);
                        return judges.Count > 0;
                    }
                }
            } catch (Exception ex)
            {
                return false;
            }

            return false;
        }
    }
}
