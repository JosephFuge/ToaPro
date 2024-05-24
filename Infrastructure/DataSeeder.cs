using Microsoft.AspNetCore.Identity;
using System.Net.WebSockets;
using ToaPro.Controllers;
using ToaPro.Models;

namespace ToaPro.Infrastructure
{
    public class DataSeeder
    {
        public ToaProContext _context { get; set; }
        public UserManager<ToaProUser> _userManager { get; set; }

        public DataSeeder(ToaProContext tempContext, UserManager<ToaProUser> userManager)
        {
            _context = tempContext;
            _userManager = userManager;
        }

        public async Task<bool> SeedSemester(string term = "Fall", int year = 2024)
        {
            int result = 0;

            if (!_context.Semesters.Any(s => s.Term == term && s.Year == year))
            {
                Semester newSemester = new Semester { Year = year, Term = term };
                _context.Semesters.Add(newSemester);
                result = await _context.SaveChangesAsync();
            }

            return result > 0;
        }

        public async Task<bool> SeedIndividualUser(ToaProUser user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);


            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (roleResult.Succeeded)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task SeedData()
        {
            if (!_context.Users.Any())
            {
                ToaProUser coordinator = new ToaProUser
                {
                    UserName = "BrewmasterTaylor",
                    Email = "taylor@wells.com",
                    FirstName = "Taylor",
                    LastName = "Wells"
                };

                await SeedIndividualUser(coordinator, "Password123!", "Coordinator");
            }

            Class IS401 = new Class { Code = "401", Description = "Product Management, Project Management, UX/UI, and so much more!" };
            Class IS413 = new Class { Code = "413", Description = "Big Boss Dawg's class about ASP .NET." };
            Class IS414 = new Class { Code = "414", Description = "Cybersecurity; sleep with one eye open, you'll get social engineered out of your first-born child." };
            Class IS455 = new Class { Code = "455", Description = "Machine Learning with Dr. Keith, the man with the biggest heart and the fastest fingers in the west." };

            // Check if data exists (class and requirements seeding)
            if (!_context.Classes.ToList().Any())
            {
                _context.Classes.AddRange(
                    IS401,
                    IS413,
                    IS414,
                    IS455
                );

                await _context.SaveChangesAsync();

                //Requirements seeding
                if (!_context.Requirements.ToList().Any())
                {
                    List<Class> classes = _context.Classes.ToList() ?? [];

                    _context.Requirements.AddRange(
                        new Requirement
                        {
                            ClassId = classes.FirstOrDefault(x => x.Code == "401").Id,
                            Description = "Figma is fully prototyped with every possible user flow."
                        },
                        new Requirement
                        {
                            ClassId = classes.FirstOrDefault(x => x.Code == "413").Id,
                            Description = "Use the word 'yeet' somewhere in your website."
                        },
                        new Requirement
                        {
                            ClassId = classes.FirstOrDefault(x => x.Code == "413").Id,
                            Description = "Code is clean; excessively commented and with descriptive variable names"
                        }
                    );
                }

            await _context.SaveChangesAsync();

            }
        }
    }
}
