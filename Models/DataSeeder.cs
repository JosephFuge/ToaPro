using Microsoft.AspNetCore.Identity;
using System.Net.WebSockets;
using ToaPro.Controllers;

namespace ToaPro.Models
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

        public async Task<bool> SeedUsers()
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
                UserName = "hcowart312",
                Email = "hayden@cowart.com",
                FirstName = "Hayden",
                LastName = "Cowart"
            };

            ToaProUser studentLily = new ToaProUser
            {
                UserName = "ltsubaki85",
                Email = "lily@tsubaki.com",
                FirstName = "Lilian",
                LastName = "Tsubaki"
            };

            ToaProUser studentNick = new ToaProUser
            {
                UserName = "nthomas12",
                Email = "nick@thomas.com",
                FirstName = "Nicholas",
                LastName = "Thomas"
            };


            ToaProUser judgeJudy = new ToaProUser
            {
                UserName = "JudyMcPherson",
                Email = "judy@mcpherson.com",
                FirstName = "Judy",
                LastName = "JudyMcPherson"
            };

            ToaProUser judgePhoenix = new ToaProUser
            {
                UserName = "PhoenixBrave",
                Email = "phoenix@brave.com",
                FirstName = "Phoenix",
                LastName = "Brave"
            };


            bool userSeedingSuccessful = false;

            userSeedingSuccessful = await SeedIndividualUser(coordinator, "Password123!", "Coordinator");
            userSeedingSuccessful = await SeedIndividualUser(professorHilton, "Password123!", "Professor");
            userSeedingSuccessful = await SeedIndividualUser(professorCutler, "Password123!", "Professor");
            userSeedingSuccessful = await SeedIndividualUser(taToa, "Password123!", "TA");
            userSeedingSuccessful = await SeedIndividualUser(studentLily, "Password123!", "Student");
            userSeedingSuccessful = await SeedIndividualUser(studentNick, "Password123!", "Student");
            userSeedingSuccessful = await SeedIndividualUser(judgeJudy, "Password123!", "Judge");
            userSeedingSuccessful = await SeedIndividualUser(judgePhoenix, "Password123!", "Judge");
            userSeedingSuccessful = await SeedIndividualUser(studentHayden, "Password123!", "Student");

            return userSeedingSuccessful;
        }

        public async Task<bool> SeedIndividualUser(ToaProUser user, String password, String role)
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

        public int SeedGroup(int semesterId, short section, short number)
        {
            try
            {
                Group newGroup = new Group { SemesterId = semesterId, Section = section, Number = number };

                var result = _context.Groups.Add(newGroup);

                _context.SaveChanges();

                var groupId = result.Entity.Id;

                return groupId;
            } catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> SeedGroupsAndStudents(Semester semester)
        {
            bool overallSuccess = true;

            int group4_1 = SeedGroup(semesterId: semester.Id, 4, 1);
            int group3_5 = SeedGroup(semesterId: semester.Id, 3, 5);
            int group4_8 = SeedGroup(semesterId: semester.Id, 4, 8);

            if (group4_1 != 0)
            {
                await AddStudentsWithGroup(0, group4_1);
            } else
            {
                overallSuccess = false;
            }

            if (group3_5 != 0) 
            {
                await AddStudentsWithGroup(4, group3_5);
            } else
            {
                overallSuccess = false;
            }

            if (group4_8 != 0)
            {
                await AddStudentsWithGroup(8, group4_8);
            } else
            {
                overallSuccess = false;
            }

            return overallSuccess;
        }

        public async Task AddStudentsWithGroup(int startNumber, int groupId)
        {
            for (int i = startNumber; i < startNumber + 4; i++)
            {
                ToaProUser newStudentUser = new ToaProUser
                {
                    UserName = "studentUserName" + i.ToString(),
                    Email = "student" + i.ToString() + "@gmail.com",
                    FirstName = "studentFirst" + i.ToString(),
                    LastName = "studentLast" + i.ToString(),
                };

                bool success = await SeedIndividualUser(newStudentUser, "Password123!", "Student");

                await _context.SaveChangesAsync();

                if (success)
                {
                    ToaProUser studentUserFullDetails = await _userManager.FindByNameAsync(userName: newStudentUser.UserName);

                    if (studentUserFullDetails != null)
                    {
                        Student newStudent = new Student
                        {
                            FName = "studentFName" + i.ToString(),
                            LName = "studentLName" + i.ToString(),
                            NetId = "netId" + i.ToString(),
                            Id = studentUserFullDetails.Id,
                            GroupId = groupId,
                            TimeSlot1 = true,
                            TimeSlot2 = true,
                            TimeSlot3 = true,
                            TimeSlot4 = true,
                            TimeSlot5 = true,
                            Reason = "",
                        };

                        _context.Add(newStudent);
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }
    
        public async Task SeedData()
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

                await _context.SaveChangesAsync();
            }

            //Semester Seeding
            Semester winter2024Semester = new Semester { Term = "Winter", Year = 2024 };

            if (!_context.Semesters.ToList().Any())
            {
                _context.Semesters.Add(winter2024Semester);

                await _context.SaveChangesAsync();
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
            }

            await _context.SaveChangesAsync();

            // Repeat for other tables, respecting foreign key dependencies
            //Grader Seeding
            if (!_context.Graders.ToList().Any())
            {
                List<Class> classes = _context.Classes.ToList() ?? [];
                var professorHilt = await _userManager.FindByNameAsync("SpencerHilton");
                var professorCutler = await _userManager.FindByNameAsync("LauraCutler");

                _context.Graders.AddRange(
                    new Grader
                    {
                        Id = professorHilt.Id,
                        ClassId = classes.FirstOrDefault(x => x.Code == "413").Id,
                        IsProfessor = false
                    },
                    new Grader
                    {
                        Id = professorCutler.Id,
                        ClassId = classes.FirstOrDefault(x => x.Code == "401").Id,
                        IsProfessor = true
                    }
                );

                await _context.SaveChangesAsync();
            }

            if (!_context.Groups.ToList().Any())
            {
                List<Semester> semesters = _context.Semesters.ToList() ?? [];

                if (semesters.Any())
                {
                    await SeedGroupsAndStudents(semester: semesters.FirstOrDefault(x => x.Year == 2024 && x.Term == "Winter"));
                }
            }

            await _context.SaveChangesAsync();

            ////Group Seeding
            //if (!_context.Groups.ToList().Any())
            //{
            //    List<Semester> semesters = _context.Semesters.ToList() ?? [];

            //    _context.Groups.AddRange(
            //        new Group
            //        {
            //            SemesterId = semesters.Where(x => x.Year == 2023 && x.Term == "Winter")
            //                         .Select(x => x.Id).FirstOrDefault(),
            //            Section = 4,
            //            Number = 12
            //        },
            //        new Group
            //        {
            //            SemesterId = semesters.Where(x => x.Year == 2023 && x.Term == "Winter")
            //                         .Select(x => x.Id).FirstOrDefault(),
            //            Section = 4,
            //            Number = 12
            //        }
            //    );

            //    await _context.SaveChangesAsync();
            //}

            //Judge Seeding
            if (!_context.Judges.ToList().Any())
            {
                var judgeUser1 = await _userManager.FindByNameAsync("JudyMcPherson");
                var judgeUser2 = await _userManager.FindByNameAsync("PhoenixBrave");

                _context.Judges.AddRange(
                    new Judge
                    {
                        Id = judgeUser1.Id,
                        FName = "randomjudgefirstname",
                        LName = "randomjudgelastname",
                        Affiliation = "KPMG",
                        TimeSlot1 = true,
                        TimeSlot2 = false,
                        TimeSlot3 = true,
                        TimeSlot4 = false,
                        TimeSlot5 = false,
                    },
                    new Judge
                    {
                        Id = judgeUser2.Id,
                        FName = "randomfirstname",
                        LName = "randomlastname",
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
                        GroupId = groups.FirstOrDefault(x => x.Section == 4 && x.Number == 8).Id,
                        Location = "W322",
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(14).ToUniversalTime()
                    },
                    new Presentation
                    {
                        GroupId = groups.FirstOrDefault(x => x.Section == 4 && x.Number == 1).Id,
                        Location = "5267",
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(14).ToUniversalTime()
                    }
                );

                await _context.SaveChangesAsync();
            }

            //Ranking Seeding
            if (!_context.Rankings.ToList().Any())
            {
                var groups = _context.Groups;
                var judges = _context.Judges;
                var presentations = _context.Presentations;

                _context.Rankings.AddRange(
                    new Ranking
                    {
                        GroupId = groups.FirstOrDefault(x => x.Section == 4 && x.Number == 1).Id,
                        JudgeId = judges.FirstOrDefault(x => x.Affiliation == "KPMG").Id,
                        TeamRanking = 2,
                        CommunicationPoints = 4,
                        CommunicationComments = "Needed more communication",
                        TechnologyPoints = 10,
                        TechnologyComments = "Great tech",
                        OverallPoints = 14,
                        Nomination = "Number 2 in INTEX",
                        PresentationId = presentations.FirstOrDefault().Id
                    },
                    new Ranking
                    {
                        GroupId = groups.FirstOrDefault(x => x.Section == 4 && x.Number == 8).Id,
                        JudgeId = judges.FirstOrDefault(x => x.Affiliation == "Disney Corp.").Id,
                        TeamRanking = 1,
                        CommunicationPoints = 6,
                        CommunicationComments = "Great communication",
                        TechnologyPoints = 10,
                        TechnologyComments = "Great tech",
                        OverallPoints = 16,
                        Nomination = "Number 1 in INTEX",
                        PresentationId = presentations.FirstOrDefault().Id
                    }
                );

                await _context.SaveChangesAsync();
            }

            //Student Seeding
            if (!_context.Students.ToList().Any())
            {
                var student0 = await _userManager.FindByNameAsync("studentUserName0");
                var student1 = await _userManager.FindByNameAsync("studentUserName1");
                _context.Students.AddRange(
                    new Student
                    {
                        Id = student0.Id,
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
                        Id = student1.Id,
                        FName = "Lily",
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
            }

            //Submission Seeding
            //    if (!_context.Submissions.ToList().Any())
            //    {
            //        List<Group> groups = _context.Groups.ToList() ?? [];
            //        List<Student> students = _context.Students.ToList() ?? [];

            //    await _context.SaveChangesAsync();

            //        _context.Submissions.AddRange(
            //            new Submission
            //            {
            //                GroupId = groups.FirstOrDefault(x => x.Id == 1).Id,
            //                StudentId = students.FirstOrDefault(x => x.Id == 1).Id,
            //                CreatedDate = DateTime.Now,
            //                GithubLink = "github",
            //                YoutubeLink = "youtube",
            //                UploadFile = "file string"

            //            },
            //            new Submission
            //            {
            //                GroupId = groups.FirstOrDefault(x => x.Id == 2).Id,
            //                StudentId = students.FirstOrDefault(x => x.Id == 2).Id,
            //                CreatedDate = DateTime.Now,
            //                GithubLink = "github",
            //                YoutubeLink = "youtube",
            //                UploadFile = "file string"
            //            }
            //        );

            //        _context.SaveChanges();
            //    }
        }
    }
}
