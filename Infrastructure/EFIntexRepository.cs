﻿using ToaPro.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ToaPro.Infrastructure
{
    public class EFIntexRepository : IIntexRepository
    {
        private readonly ToaProContext _toaProContext;

        public EFIntexRepository(ToaProContext toaProContext)
        {
            _toaProContext = toaProContext;
        }

        public async Task<int> CommitChangesAsync()
        {
            return await _toaProContext.SaveChangesAsync();
        }


        public IQueryable<Judge> Judges => _toaProContext.Judges;
        public IQueryable<Presentation> Presentations => _toaProContext.Presentations;
        public IEnumerable<Class> Classes => _toaProContext.Classes;
        public IQueryable<Requirement> Requirements => _toaProContext.Requirements;
        public IEnumerable<Semester> Semesters => _toaProContext.Semesters;
        public IQueryable<Ranking> Rankings => _toaProContext.Rankings;

        public IQueryable<Award> Awards => _toaProContext.Awards;

        //Change RequestAvailability to match the judge model when inputting their timeslots
        public void RequestAvailability(Judge judge)
        {
            _toaProContext.Add(judge);
            _toaProContext.SaveChanges();
        }

        public void UpdateRanking(Ranking ranking)
        {
            _toaProContext.Rankings.Update(ranking);
            _toaProContext.SaveChanges();
        }

        public void AddRanking(Ranking ranking)
        {
            _toaProContext.Rankings.Add(ranking);
            _toaProContext.SaveChanges();
        }

        public void StudentRequestAvailability(Student student)
        {
            _toaProContext.Add(student);
            _toaProContext.SaveChanges();
        }

        public void UpdateAward(Award award)
        {
            _toaProContext.Awards.Update(award);
            _toaProContext.SaveChanges();
        }
        //PLS FIX THIS TEAM :) 
        public void UpdateJudgeAvailability(Judge judge)
        {
            _toaProContext.Judges.Update(judge);
            _toaProContext.SaveChanges();
        }
        public async Task AddJudgeList(List<Judge> judges)
        {
            _toaProContext.Judges.AddRange(judges);
            _toaProContext.SaveChanges();
        }

        public Judge? GetJudgeById(string id)
        {
            return _toaProContext.Judges.FirstOrDefault(x => x.Id == id);
        }

        /* Students */
        public IQueryable<Student> Students => _toaProContext.Students;
        public IQueryable<Models.Group> Groups => _toaProContext.Groups;
        public async Task AddStudent(Student student)
        {
            _toaProContext.Students.Add(student);
            _toaProContext.SaveChanges();
        }
        public async Task AddStudentList(List<Student> students)
        {
            _toaProContext.Students.AddRange(students);
            _toaProContext.SaveChanges();
        }
        public async Task AddGroup(Models.Group group)
        {
            _toaProContext.Groups.Add(group);
            _toaProContext.SaveChanges();
        }

        /* Submissions */
        public IQueryable<SubmissionAnswer> SubmissionAnswers => _toaProContext.SubmissionAnswers;
        public async Task<int> AddSubmissionAnswers(IEnumerable<SubmissionAnswer> answers)
        {
            _toaProContext.SubmissionAnswers.AddRange(answers);
            return await _toaProContext.SaveChangesAsync();
        }

        public IEnumerable<SubmissionField> SubmissionFields(bool tracking = true)
        {
            if (tracking)
            {
                return _toaProContext.SubmissionFields.ToList();
            } else
            {
                return _toaProContext.SubmissionFields.AsNoTracking().ToList();
            }
        }
        public void AddSubmissionFieldList(List<SubmissionField> submissionFields)
        {
            _toaProContext.SubmissionFields.AddRange(submissionFields);
        }
        public void UpdateSubmissionFieldList(List<SubmissionField> submissionFields)
        {
            _toaProContext.SubmissionFields.UpdateRange(submissionFields);
        }

        public void DeleteSubmissionFieldList(List<SubmissionField> submissionFields)
        {
            _toaProContext.SubmissionFields.RemoveRange(submissionFields);
        }

        /* Grades */
        public IQueryable<Grade> Grades => _toaProContext.Grades;
        public void AddGrades(IEnumerable<Grade> grades) {
            _toaProContext.Grades.AddRange(grades);
        }

        public void UpdateGrades(IEnumerable<Grade> grades) {
            _toaProContext.Grades.UpdateRange(grades);
        }
    }
}
