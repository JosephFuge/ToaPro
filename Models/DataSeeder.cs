using Microsoft.AspNetCore.Identity;

namespace ToaPro.Models
{
    public class DataSeeder
    {
        public ToaProContext _context { get; set; }

        public DataSeeder(ToaProContext tempContext)
        {
            _context = tempContext;
        }

        public async Task<bool> SeedUsers(UserManager<ToaProUser> userManager)
        {
            ToaProUser coordinator = new ToaProUser
            {
                UserName = "BrewmasterTaylor",
                Email = "taylor@wells.com",
                FirstName = "Taylor",
                LastName = "Wells"
            };

            ToaProUser professorHilton = new ToaProUser
            {
                UserName = "SpencerHilton",
                Email = "bigbossdog6@outlook.com",
                FirstName = "Spencer",
                LastName = "Hilton"
            };

            ToaProUser professorCutler = new ToaProUser
            {
                UserName = "LauraCutler",
                Email = "laura@cutler.com",
                FirstName = "Laura",
                LastName = "Cutler"
            };

            ToaProUser taToa = new ToaProUser
            {
                UserName = "ToaTheGoat",
                Email = "toa@example.com",
                FirstName = "Toa",
                LastName = "Pita"
            };

            ToaProUser studentHayden = new ToaProUser
            {
                UserName = "HaydenCowart",
                Email = "hayden@cowart.com",
                FirstName = "Hayden",
                LastName = "Cowart"
            };

            ToaProUser studentLily = new ToaProUser
            {
                UserName = "LilianTsubaki",
                Email = "lily@tsubaki.com",
                FirstName = "Lilian",
                LastName = "Tsubaki"
            };

            ToaProUser studentNick = new ToaProUser
            {
                UserName = "NicholasThomas",
                Email = "nick@thomas.com",
                FirstName = "Nicholas",
                LastName = "Thomas"
            };

            bool userSeedingSuccessful = false;

            userSeedingSuccessful = await SeedIndividualUser(userManager, coordinator, "Password123!", "Coordinator");
            userSeedingSuccessful = await SeedIndividualUser(userManager, professorHilton, "Password123!", "Professor");
            userSeedingSuccessful = await SeedIndividualUser(userManager, professorCutler, "Password123!", "Professor");
            userSeedingSuccessful = await SeedIndividualUser(userManager, taToa, "Password123!", "TA");
            userSeedingSuccessful = await SeedIndividualUser(userManager, studentHayden, "Password123!", "Student");
            userSeedingSuccessful = await SeedIndividualUser(userManager, studentLily, "Password123!", "Student");
            userSeedingSuccessful = await SeedIndividualUser(userManager, studentNick, "Password123!", "Student");

            return userSeedingSuccessful;
        }

        public async Task<bool> SeedIndividualUser(UserManager<ToaProUser> userManager, ToaProUser user, String password, String role)
        {
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(user, role);
                if (roleResult.Succeeded)
                {
                    return true;
                }
            }

            return false;
        }

        public void SeedData()
        {
            Class IS401 = new Class { Code = "401", Description = "Product Management, Project Management, UX/UI, and so much more!" };
            Class IS413 = new Class { Code = "413", Description = "Big Boss Dawg's class about ASP .NET." };
            Class IS414 = new Class { Code = "414", Description = "Cybersecurity; sleep with one eye open, you'll get social engineered out of your first-born child." };
            Class IS455 = new Class { Code = "455", Description = "Machine Learning with Dr. Keith, the man with the biggest heart and the fastest fingers in the west." };

            // Check if data exists (class seeding)
            if (!_context.Classes.ToList().Any())
            {
                _context.Classes.AddRange(
                    IS401,
                    IS413,
                    IS414,
                    IS455
                );

                _context.SaveChanges();
            }

            //Semester Seeding
            if (!_context.Semesters.ToList().Any())
            {
                _context.Semesters.AddRange(
                    new Semester { Term = "Winter", Year = 2023 }
                    //new Semester { /* initialize properties */ }
                );

                _context.SaveChanges();
            }

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

                _context.SaveChanges();
            }

            // Repeat for other tables, respecting foreign key dependencies
            //Grader Seeding
            if (!_context.Graders.ToList().Any())
            {
                List<Class> classes = _context.Classes.ToList() ?? [];

                _context.Graders.AddRange(
                    new Grader
                    {
                        ClassId = classes.FirstOrDefault(x => x.Code == "413").Id,
                        IsProfessor = false
                    },
                    new Grader
                    {
                        ClassId = classes.FirstOrDefault(x => x.Code == "401").Id,
                        IsProfessor = true
                    }
                );

                _context.SaveChanges();
            }

            //Group Seeding
            if (!_context.Groups.ToList().Any())
            {
                List<Semester> semesters = _context.Semesters.ToList() ?? [];

                _context.Groups.AddRange(
                    new Group
                    {
                        SemesterId = semesters.Where(x => x.Year == 2023 && x.Term == "Winter")
                                     .Select(x => x.Id).FirstOrDefault(),
                        Section = 4,
                        Number = 12
                    },
                    new Group
                    {
                        SemesterId = semesters.Where(x => x.Year == 2023 && x.Term == "Winter")
                                     .Select(x => x.Id).FirstOrDefault(),
                        Section = 4,
                        Number = 12
                    }
                );

                _context.SaveChanges();
            }

            //Judge Seeding
            if (!_context.Judges.ToList().Any())
            {
                _context.Judges.AddRange(
                    new Judge
                    {
                        FName = "James",
                        LName = "John",
                        Affiliation = "KPMG"
                    },
                    new Judge
                    {
                        FName = "Joseph",
                        LName = "Kit",
                        Affiliation = "Disney Corp."
                    }
                );

                _context.SaveChanges();
            }

            //Judge Availability Seeding?? Ask question about that model...

            //Presentation Seeding
            if (!_context.Presentations.ToList().Any())
            {
                List<Group> groups = _context.Groups.ToList() ?? [];

                _context.Presentations.AddRange(
                    new Presentation
                    {

                    },
                    new Presentation
                    {

                    }
                );

                _context.SaveChanges();
            }
        }
    }
}
