using Microsoft.AspNetCore.Identity;

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
            } catch(Exception ex)
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
                            StudentId = studentUserFullDetails.Id,
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

            // Check if data exists
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

            Semester winter2024Semester = new Semester { Term = "Winter", Year = 2024 };

            if (!_context.Semesters.ToList().Any())
            {
                _context.Semesters.Add(winter2024Semester);

                _context.SaveChanges();
            }

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

            if (!_context.Groups.ToList().Any())
            {
                List<Semester> semesters = _context.Semesters.ToList() ?? [];

                if (semesters.Any())
                {
                    await SeedGroupsAndStudents(semester: semesters.FirstOrDefault(x => x.Year == 2024 && x.Term == "Winter"));
                }
            }


            await _context.SaveChangesAsync();

            // Repeat for other tables, respecting foreign key dependencies
        }
    }
}
