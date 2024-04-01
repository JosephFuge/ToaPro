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
                        Affiliation = "KPMG",
                        TimeSlot1 = true,
                        TimeSlot2 =false,
                        TimeSlot3 = true,
                        TimeSlot4 = false,
                        TimeSlot5 = false,
                    },
                    new Judge
                    {
                        FName = "Joseph",
                        LName = "Kit",
                        Affiliation = "Disney Corp.",
                        TimeSlot1 = true,
                        TimeSlot2 = true,
                        TimeSlot3 = true,
                        TimeSlot4 = true,
                        TimeSlot5 = true,
                    }
                );

                _context.SaveChanges();
            }

            //Presentation Seeding
            if (!_context.Presentations.ToList().Any())
            {
                List<Group> groups = _context.Groups.ToList() ?? [];

                _context.Presentations.AddRange(
                    new Presentation
                    {
                        GroupId = groups.FirstOrDefault(x => x.Id == 1).Id,
                        Location = "W322",
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(14)
                    },
                    new Presentation
                    {
                        GroupId = groups.FirstOrDefault(x => x.Id == 2).Id,
                        Location = "5267",
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(14)
                    }
                );

                _context.SaveChanges();
            }

            //Ranking Seeding
            if (!_context.Rankings.ToList().Any())
            {
                List<Group> groups = _context.Groups.ToList() ?? [];
                List<Judge> judges = _context.Judges.ToList() ?? [];

                _context.Rankings.AddRange(
                    new Ranking
                    {
                        GroupId = groups.FirstOrDefault(x => x.Id == 1).Id,
                        JudgeId = judges.FirstOrDefault(x => x.Id == 1).Id,
                        TeamRanking = 2,
                        CommunicationPoints = 4,
                        CommunicationComments = "Needed more communication",
                        TechnologyPoints = 10,
                        TechnologyComments = "Great tech",
                        OverallPoints = 14,
                        Nomination = "Number 2 in INTEX"
                    },
                    new Ranking
                    {
                        GroupId = groups.FirstOrDefault(x => x.Id == 2).Id,
                        JudgeId = judges.FirstOrDefault(x => x.Id == 2).Id,
                        TeamRanking = 1,
                        CommunicationPoints = 6,
                        CommunicationComments = "Great communication",
                        TechnologyPoints = 10,
                        TechnologyComments = "Great tech",
                        OverallPoints = 16,
                        Nomination = "Number 1 in INTEX"
                    }
                );

                _context.SaveChanges();
            }

            //Student Seeding
            if (!_context.Students.ToList().Any())
            {
                _context.Students.AddRange(
                    new Student
                    {
                        FName = "Hayden",
                        LName = "Bro",
                        NetId = "123 456 1234",
                        TimeSlot1 = true,
                        TimeSlot2 = false,
                        TimeSlot3 = false,
                        TimeSlot4 = false,
                        TimeSlot5 = false,
                        Reason = "I don't like mornings"

                    },
                    new Student
                    {
                        FName = "Luke",
                        LName = "Forest",
                        NetId = "123 478 3456",
                        TimeSlot1 = false,
                        TimeSlot2 = false,
                        TimeSlot3 = false,
                        TimeSlot4 = false,
                        TimeSlot5 = true,
                        Reason = "LOL"
                    }
                );

                _context.SaveChanges();
            }

            //Submission Seeding
            if (!_context.Submissions.ToList().Any())
            {
                List<Group> groups = _context.Groups.ToList() ?? [];
                List<Student> students = _context.Students.ToList() ?? [];

                _context.Submissions.AddRange(
                    new Submission
                    {
                        GroupId = groups.FirstOrDefault(x => x.Id == 1).Id,
                        StudentId = students.FirstOrDefault(x => x.Id == 1).Id,
                        CreatedDate = DateTime.Now,
                        GithubLink = "github",
                        YoutubeLink = "youtube",
                        UploadFile = "file string"

                    },
                    new Submission
                    {
                        GroupId = groups.FirstOrDefault(x => x.Id == 2).Id,
                        StudentId = students.FirstOrDefault(x => x.Id == 2).Id,
                        CreatedDate = DateTime.Now,
                        GithubLink = "github",
                        YoutubeLink = "youtube",
                        UploadFile = "file string"
                    }
                );

                _context.SaveChanges();
            }
        }
    }
}
