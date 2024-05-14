using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using ToaPro.Models;

namespace ToaPro.Infrastructure
{
    public class UserBulkUploader
    {
        private UserManager<ToaProUser> _userManager;
        private IIntexRepository _repo;
        public List<Group> Groups { get; set; } = new List<Group>();

        public UserBulkUploader(UserManager<ToaProUser> userManager, IIntexRepository intexRepository)
        {
            _userManager = userManager;
            _repo = intexRepository;
        }

        public async Task<int?> GetOrCreateGroup(int Section, int Number, int semesterId)
        {
            var group = Groups.FirstOrDefault(g => g.Section == Section && g.Number == Number && g.SemesterId == semesterId);
            if (group != null)
            {
                return group.Id;
            } else
            {
                var dbGroup = await _repo.Groups.FirstOrDefaultAsync(g => g.Section == Section && g.Number == Number && g.SemesterId == semesterId );
                if (dbGroup != null)
                {
                    return dbGroup.Id;
                } else
                {
                    Group newGroup = new Group
                    {
                        Section = Section,
                        Number = Number,
                        SemesterId = semesterId,
                        TimeSlot1 = true,
                        TimeSlot2 = true,
                        TimeSlot3 = true,
                        TimeSlot4 = true,
                        TimeSlot5 = true
                    };
                    await _repo.AddGroup(newGroup);
                    Group? findGroup = await _repo.Groups.FirstOrDefaultAsync(g => g.Section == Section && g.Number == Number && g.SemesterId == semesterId);
                    if (findGroup != null)
                    {
                        return findGroup.Id;
                    }
                }
            }
           
            return null;
        }

        public async Task<List<Student>> CreateStudentsFromImport(List<StudentImportFormat> userImportModels)
        {
            try
            {
                var students = new List<Student>();
                var users = new List<ToaProUser>();
                const int semesterId = 1; // Hardcoded for now, replace with actual value if needed
                var results = new List<IdentityResult>();

                for (int i = 0; i < userImportModels.Count; i++)
                {
                    ToaProUser studentUser = new ToaProUser
                    {
                        FirstName = userImportModels[i].FirstName,
                        LastName = userImportModels[i].LastName,
                        Email = userImportModels[i].Email,
                        UserName = userImportModels[i].Email // Assuming email is used as username
                    };
                    users.Add(studentUser);
                    var result = await _userManager.CreateAsync(studentUser, "A123!!!" + userImportModels[i].NetID);
                    results.Add(result);
                }

                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i] != null && results[i].Succeeded)
                    {
                        int? GroupId = await GetOrCreateGroup(userImportModels[i].SectionNumber, userImportModels[i].GroupNumber, semesterId);
                        if (GroupId != null)
                        {
                            Student newStudent = new Student
                            {
                                Id = users[i].Id,
                                NetId = userImportModels[i].NetID,
                                Reason = string.Empty,
                                GroupId = GroupId!.Value,
                            };
                            students.Add(newStudent);
                        }
                    }
                }

                if (students.Count > 0)
                {
                    await _repo.AddStudentList(students);
                }

                return students;
            } catch (Exception ex)
            {

            }
            
            return new List<Student>();
        }
    }
}
