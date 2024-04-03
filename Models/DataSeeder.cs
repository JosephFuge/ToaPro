
// ﻿//using Microsoft.AspNetCore.Identity;

// //namespace ToaPro.Models
// //{
// //    public class DataSeeder
// //    {
// //        public ToaProContext _context { get; set; }

// //        public DataSeeder(ToaProContext tempContext)
// //        {
// //            _context = tempContext;
// //        }

// //        public async Task<bool> SeedUsers(UserManager<ToaProUser> userManager)
// //        {
// //            ToaProUser coordinator = new ToaProUser
// //            {
// //                UserName = "BrewmasterTaylor",
// //                Email = "taylor@wells.com",
// //                FirstName = "Taylor",
// //                LastName = "Wells"
// //            };

// //            ToaProUser professorHilton = new ToaProUser
// //            {
// //                UserName = "SpencerHilton",
// //                Email = "bigbossdog6@outlook.com",
// //                FirstName = "Spencer",
// //                LastName = "Hilton"
// //            };

// //            ToaProUser professorCutler = new ToaProUser
// //            {
// //                UserName = "LauraCutler",
// //                Email = "laura@cutler.com",
// //                FirstName = "Laura",
// //                LastName = "Cutler"
// //            };

// //            ToaProUser taToa = new ToaProUser
// //            {
// //                UserName = "ToaTheGoat",
// //                Email = "toa@example.com",
// //                FirstName = "Toa",
// //                LastName = "Pita"
// //            };

// //            ToaProUser studentHayden = new ToaProUser
// //            {
// //                UserName = "HaydenCowart",
// //                Email = "hayden@cowart.com",
// //                FirstName = "Hayden",
// //                LastName = "Cowart"
// //            };

// //            ToaProUser studentLily = new ToaProUser
// //            {
// //                UserName = "LilianTsubaki",
// //                Email = "lily@tsubaki.com",
// //                FirstName = "Lilian",
// //                LastName = "Tsubaki"
// //            };

// //            ToaProUser studentNick = new ToaProUser
// //            {
// //                UserName = "NicholasThomas",
// //                Email = "nick@thomas.com",
// //                FirstName = "Nicholas",
// //                LastName = "Thomas"
// //            };

// //            bool userSeedingSuccessful = false;

// //            userSeedingSuccessful = await SeedIndividualUser(userManager, coordinator, "Password123!", "Coordinator");
// //            userSeedingSuccessful = await SeedIndividualUser(userManager, professorHilton, "Password123!", "Professor");
// //            userSeedingSuccessful = await SeedIndividualUser(userManager, professorCutler, "Password123!", "Professor");
// //            userSeedingSuccessful = await SeedIndividualUser(userManager, taToa, "Password123!", "TA");
// //            userSeedingSuccessful = await SeedIndividualUser(userManager, studentHayden, "Password123!", "Student");
// //            userSeedingSuccessful = await SeedIndividualUser(userManager, studentLily, "Password123!", "Student");
// //            userSeedingSuccessful = await SeedIndividualUser(userManager, studentNick, "Password123!", "Student");

// //            return userSeedingSuccessful;
// //        }

// //        public async Task<bool> SeedIndividualUser(UserManager<ToaProUser> userManager, ToaProUser user, String password, String role)
// //        {
// //            var result = await userManager.CreateAsync(user, password);

// //            if (result.Succeeded)
// //            {
// //                var roleResult = await userManager.AddToRoleAsync(user, role);
// //                if (roleResult.Succeeded)
// //                {
// //                    return true;
// //                }
// //            }

// //            return false;
// //        }

// //        public void SeedData()
// //        {
// //            Class IS401 = new Class { Code = "401", Description = "Product Management, Project Management, UX/UI, and so much more!" };
// //            Class IS413 = new Class { Code = "413", Description = "Big Boss Dawg's class about ASP .NET." };
// //            Class IS414 = new Class { Code = "414", Description = "Cybersecurity; sleep with one eye open, you'll get social engineered out of your first-born child." };
// //            Class IS455 = new Class { Code = "455", Description = "Machine Learning with Dr. Keith, the man with the biggest heart and the fastest fingers in the west." };

// //            // Check if data exists
// //            if (!_context.Classes.ToList().Any())
// //            {
// //                _context.Classes.AddRange(
// //                    IS401,
// //                    IS413,
// //                    IS414,
// //                    IS455
// //                );

// //                _context.SaveChanges();
// //            }

// //            if (!_context.Semesters.ToList().Any())
// //            {
// //                _context.Semesters.AddRange(
// //                    new Semester { Term = "Winter", Year = 2023 }
// //                    //new Semester { /* initialize properties */ }
// //                );

// //                _context.SaveChanges();
// //            }

// //            if (!_context.Requirements.ToList().Any())
// //            {
// //                List<Class> classes = _context.Classes.ToList() ?? [];

// //                _context.Requirements.AddRange(
// //                    new Requirement
// //                    {
// //                        ClassId = classes.FirstOrDefault(x => x.Code == "401").Id,
// //                        Description = "Figma is fully prototyped with every possible user flow."
// //                    },
// //                    new Requirement
// //                    {
// //                        ClassId = classes.FirstOrDefault(x => x.Code == "413").Id,
// //                        Description = "Use the word 'yeet' somewhere in your website."
// //                    },
// //                    new Requirement
// //                    {
// //                        ClassId = classes.FirstOrDefault(x => x.Code == "413").Id,
// //                        Description = "Code is clean; excessively commented and with descriptive variable names"
// //                    }
// //                );

//                 await _context.SaveChangesAsync();
//             }

//             //IF YOU ARE WONDERING WHO COMMENTED THIS OUT, IT WAS COLEMAN.  THIS PIECE WAS THROWING AN ERROR WHEN TRYING TO LOAD THE PAGE
//             //if (!_context.Groups.ToList().Any())
//             //{
//             //    List<Semester> semesters = _context.Semesters.ToList() ?? [];

//             //    if (semesters.Any())
//             //    {
//             //        await SeedGroupsAndStudents(semester: semesters.FirstOrDefault(x => x.Year == 2024 && x.Term == "Winter"));
//             //    }
//             //}

//             await _context.SaveChangesAsync();

//             ////Group Seeding
//             //if (!_context.Groups.ToList().Any())
//             //{
//             //    List<Semester> semesters = _context.Semesters.ToList() ?? [];

//             //    _context.Groups.AddRange(
//             //        new Group
//             //        {
//             //            SemesterId = semesters.Where(x => x.Year == 2023 && x.Term == "Winter")
//             //                         .Select(x => x.Id).FirstOrDefault(),
//             //            Section = 4,
//             //            Number = 12
//             //        },
//             //        new Group
//             //        {
//             //            SemesterId = semesters.Where(x => x.Year == 2023 && x.Term == "Winter")
//             //                         .Select(x => x.Id).FirstOrDefault(),
//             //            Section = 4,
//             //            Number = 12
//             //        }
//             //    );

//             //    await _context.SaveChangesAsync();
//             //}

//             //Judge Seeding
//             if (!_context.Judges.ToList().Any())
//             {
//                 var judgeUser1 = await _userManager.FindByNameAsync("JudyMcPherson");
//                 var judgeUser2 = await _userManager.FindByNameAsync("PhoenixBrave");

//                 _context.Judges.AddRange(
//                     new Judge
//                     {
//                         Id = judgeUser1.Id,
//                         Affiliation = "KPMG",
//                         TimeSlot1 = true,
//                         TimeSlot2 = false,
//                         TimeSlot3 = true,
//                         TimeSlot4 = false,
//                         TimeSlot5 = false,
//                     },
//                     new Judge
//                     {
//                         Id = judgeUser2.Id,
//                         Affiliation = "Disney Corp.",
//                         TimeSlot1 = true,
//                         TimeSlot2 = true,
//                         TimeSlot3 = true,
//                         TimeSlot4 = true,
//                         TimeSlot5 = true,
//                     }
//                 );

//                 _context.SaveChanges();
//             }

//             //Presentation Seeding
//             if (!_context.Presentations.ToList().Any())
//             {
//                 List<Group> groups = _context.Groups.ToList() ?? [];

//                 if ( groups.Any() )
//                 {
//                     _context.Presentations.AddRange(
//                     new Presentation
//                     {
//                         GroupId = groups.FirstOrDefault(x => x.Section == 4 && x.Number == 8).Id,
//                         Location = "W322",
//                         StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(14).ToUniversalTime()
//                     },
//                     new Presentation
//                     {
//                         GroupId = groups.FirstOrDefault(x => x.Section == 4 && x.Number == 1).Id,
//                         Location = "5267",
//                         StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(14).ToUniversalTime()
//                     }
//                 ); 
//                 }

//                 await _context.SaveChangesAsync();
//             }

//             //Ranking Seeding
//             if (!_context.Rankings.ToList().Any())
//             {
//                 var groups = _context.Groups;
//                 var judges = _context.Judges;
//                 var presentations = _context.Presentations;

//                 if (groups.ToList().Any())
//                 {
//                     _context.Rankings.AddRange(
//                     new Ranking
//                     {
//                         GroupId = groups.FirstOrDefault(x => x.Section == 4 && x.Number == 1).Id,
//                         JudgeId = judges.FirstOrDefault(x => x.Affiliation == "KPMG").Id,
//                         TeamRanking = 2,
//                         CommunicationPoints = 4,
//                         CommunicationComments = "Needed more communication",
//                         TechnologyPoints = 10,
//                         TechnologyComments = "Great tech",
//                         OverallPoints = 14,
//                         Nomination = "Number 2 in INTEX",
//                         PresentationId = presentations.FirstOrDefault(x => x.GroupId == 4).Id
//                     },
//                     new Ranking
//                     {
//                         GroupId = groups.FirstOrDefault(x => x.Section == 4 && x.Number == 8).Id,
//                         JudgeId = judges.FirstOrDefault(x => x.Affiliation == "KPMG").Id,
//                         TeamRanking = 1,
//                         CommunicationPoints = 6,
//                         CommunicationComments = "Great communication",
//                         TechnologyPoints = 10,
//                         TechnologyComments = "Great tech",
//                         OverallPoints = 16,
//                         Nomination = "Number 1 in INTEX",
//                         PresentationId = presentations.FirstOrDefault(x => x.GroupId == 6).Id
//                     },
//                     new Ranking
//                     {
//                         GroupId = groups.FirstOrDefault(x => x.Section == 4 && x.Number == 8).Id,
//                         JudgeId = judges.FirstOrDefault(x => x.Affiliation == "Disney Corp.").Id,
//                         TeamRanking = 1,
//                         CommunicationPoints = 6,
//                         CommunicationComments = "Great communication",
//                         TechnologyPoints = 10,
//                         TechnologyComments = "Great tech",
//                         OverallPoints = 16,
//                         Nomination = "Number 1 in INTEX",
//                         PresentationId = presentations.FirstOrDefault(x => x.GroupId == 6).Id
//                     }
//                 );
//                 }

//                 await _context.SaveChangesAsync();
//             }

//             //Student Seeding
//             if (!_context.Students.ToList().Any())
//             {
//                 var student0 = await _userManager.FindByNameAsync("studentUserName0");
//                 var student1 = await _userManager.FindByNameAsync("studentUserName1");

//                 if (student0 != null && student1 != null)
//                 {
//                     _context.Students.AddRange(
//                         new Student
//                         {
//                             Id = student0.Id,
//                             NetId = "123 456 1234",
//                             Reason = "I don't like mornings"

//                         },
//                         new Student
//                         {
//                             Id = student1.Id,
//                             NetId = "123 478 3456",
//                             Reason = "LOL"
//                         }
//                     );

//                     await _context.SaveChangesAsync();
//                 }
                
// //            }

// //            _context.SaveChanges();

// //            // Repeat for other tables, respecting foreign key dependencies
// //        }
// //    }
// //}
