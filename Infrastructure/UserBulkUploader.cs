﻿using Microsoft.AspNetCore.Identity;
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
        public int semesterId { get; set; } = 1; // Hardcoded for now, replace with actual value if needed
        public List<Group> Groups { get; set; } = new List<Group>();

        public UserBulkUploader(UserManager<ToaProUser> userManager, IIntexRepository intexRepository)
        {
            _userManager = userManager;
            _repo = intexRepository;
        }

        private async Task<List<IdentityResult?>> CreateUserAccounts(List<ToaProUser> users)
        {
            List<IdentityResult?> results = new List<IdentityResult?>();

            for (int i = 0; i < users.Count; i++)
            {
                var result = await _userManager.CreateAsync(users[i], "A123!!!" + users[i].NetId);
                results.Add(result);
            }

            return results;
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
                var users = userImportModels.Select(uim =>
                    new ToaProUser
                    {
                        NetId = uim.NetID,
                        FirstName = uim.FirstName,
                        LastName = uim.LastName,
                        Email = uim.Email,
                        UserName = uim.Email // Assuming email is used as username
                    }
                ).ToList();

                var results = await CreateUserAccounts(users);

                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i] != null && results[i].Succeeded)
                    {
                        await _userManager.AddToRoleAsync(users[i], "Student");

                        int? GroupId = await GetOrCreateGroup(userImportModels[i].SectionNumber, userImportModels[i].GroupNumber, semesterId);
                        if (GroupId != null)
                        {
                            Student newStudent = new Student
                            {
                                Id = users[i].Id,
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

        public async Task<List<ToaProUser>> CreateTAsFromImport(List<TAImportFormat> userImportModels)
        {
            try
            {
                var users = userImportModels.Select(uim =>
                    new ToaProUser
                    {
                        NetId = uim.NetID,
                        FirstName = uim.FirstName,
                        LastName = uim.LastName,
                        Email = uim.Email,
                        UserName = uim.Email // Assuming email is used as username
                    }
                ).ToList();

                var results = await CreateUserAccounts(users);

                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i] != null && results[i].Succeeded)
                    {
                        await _userManager.AddToRoleAsync(users[i], "TA");
                    }
                }

                return users;
            } catch (Exception ex)
            {

            }
            
            return new List<ToaProUser>();
        }
    }
}
