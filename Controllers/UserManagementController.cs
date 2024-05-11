using Microsoft.AspNetCore.Mvc;
using ToaPro.Models.ViewModels;
using ToaPro.Infrastructure;
using Microsoft.AspNetCore.Identity;
using ToaPro.Models;
using Microsoft.EntityFrameworkCore;

namespace ToaPro.Controllers
{
    public class UserManagementController : Controller
    {
        private IIntexRepository _repository;
        private SignInManager<ToaProUser> _signInManager;

        public UserManagementController(IIntexRepository repository, SignInManager<ToaProUser> signInManager)
        {
            _repository = repository;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("UserRoles");
        }

        [HttpGet]
        public async Task<IActionResult> UserRoles()
        {
            List<Student> students = _repository.Students.Include(s => s.ToaProUser).Include(s => s.Group).ToList();
            Dictionary<Student, IList<string>> studentRoles = new Dictionary<Student, IList<string>>();
            foreach (var student in students)
            {
                var roles = await _signInManager.UserManager.GetRolesAsync(student.ToaProUser);
                if (roles != null)
                {
                    studentRoles.Add(student, roles);
                } else
                {
                    studentRoles.Add(student, new List<string>());
                }
            }

            UserRolesViewModel userRolesViewModel = new UserRolesViewModel
            {
                SelectedRole = UserRole.Student,
                UserRoles = { },
                StudentRoles = studentRoles
            };

            return View(userRolesViewModel);
        }
    }
}
