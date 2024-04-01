﻿using ToaPro.Models;

namespace ToaPro
{
    public interface IIntexRepository
    {
        IEnumerable<Student> Students { get; } 
        IEnumerable<Class> Classes { get; }
        IEnumerable<Grade> Grades { get; }
        IEnumerable<Grader> Graders { get; }
        IEnumerable<Requirement> Requirements { get; }
        IEnumerable<Semester> Semesters { get; }
        IEnumerable<Submission> Submissions { get; }
        IQueryable<Judge> Judges { get; }
        IQueryable<Presentation> Presentations { get; }
        IQueryable<Ranking> Rankings { get; }
        IQueryable<Group> Groups { get; }
        IQueryable<ToaProUser> ToaProUsers { get; }

        public void RequestAvailability(Judge judge);
        public void StudentRequestAvailability(Student student);

        /*public void RequestAvailability(Judge Id);*/
        public void UpdateAvailability(Judge Id);
        /*public void StudentRequestAvailability(StudentAvailability availabilities);*/
        public void UpdateRanking(Ranking ranking);

        public void AddRanking(Ranking ranking);
        public void AddSubmission(Submission submission);
    }
}
