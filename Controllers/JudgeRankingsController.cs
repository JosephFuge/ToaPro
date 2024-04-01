﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Models;

namespace ToaPro.Controllers
{
    public class JudgeRankingsController : Controller
    {
        private IIntexRepository _repo;
        
        public JudgeRankingsController(IIntexRepository temp)
        {
            _repo = temp;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult JudgePresentationForm(int judgeId = 2, int groupId = 0)
        {
            // List of rankings for the specified judge
            var judgeRankings = _repo.Rankings.Where(x => x.JudgeId == judgeId).ToList();

            // List of Judges and their Presentations
            var judgesList = _repo.Judges.Where(x => x.Id == judgeId).Include(x => x.Presentations).ToList();

            Ranking ranking;

            // If first visit to site, create the rankings necessary
            if (judgeRankings.Count == 0)
            {
                foreach (Judge j in judgesList)
                {
                    foreach (Presentation p in j.Presentations)
                    {
                        ranking = new Ranking
                        {
                            JudgeId = j.Id,
                            GroupId = p.GroupId,
                            PresentationId = p.Id
                        };

                        _repo.AddRanking(ranking);
                    }
                }
            }
            
            // If this is first time to the page, default to the first group in terms of the presentation StartDate
            if (groupId == 0)
            {
                var group = (from rankings in _repo.Rankings
                            join presentations in _repo.Presentations
                            on rankings.PresentationId equals presentations.Id
                            where rankings.JudgeId == judgeId
                            orderby presentations.StartDate
                            select new
                            {
                                GroupId = rankings.GroupId,
                                StartDate = presentations.StartDate
                                // Include other properties from the Presentation or Ranking entities as needed
                            }).FirstOrDefault();

                if (group != null)
                {
                    groupId = group.GroupId;
                }
            }

            //Pull in the ranking for the specified JudgeId and GroupId
            var rankingToEdit = _repo.Rankings
                .Where(x => x.JudgeId == judgeId && x.GroupId == groupId)
                .FirstOrDefault();

            // Set the record to be edited to the new Ranking created in the table
            var rankingId = rankingToEdit.Id;

            // Pass in data for buttons 
            ViewBag.joinedData = _repo.Judges
                .Where(x => x.Id == judgeId)
                .Include(r => r.Rankings)
                .Include(p => p.Presentations)
                    .ThenInclude(g => g.Group)
                .ToList();

            //Select which ranking to edit
            var recordToEdit = _repo.Rankings.Single(x => x.Id == rankingId);

            return View(recordToEdit);
        }

        [HttpPost]
        public IActionResult JudgePresentationForm(Ranking updatedInfo)
        {
            _repo.UpdateRanking(updatedInfo);

            var groupId = updatedInfo.GroupId;
            var judgeId = updatedInfo.JudgeId;

            // Redirect to the same action method with the same parameters
            return RedirectToAction("JudgePresentationForm", new { judgeId = judgeId, groupId = groupId });
        }

        [HttpGet]
        public IActionResult TeamRankings(int judgeId = 1)
        {
            ViewBag.joinedData = _repo.Judges
                .Where(x => x.Id == judgeId)
                .Include(r => r.Rankings)
                .Include(p => p.Presentations)
                    .ThenInclude(g => g.Group)
                .ToList();

            return View();
        }

        public IActionResult ProfessorViewAssignAwards()
        {
            return View();
        }

        public IActionResult CoordinatorAssignJudges()
        { 

            return View();
        }

        public IActionResult FunAwards2()
        {
            return View();
        }
    }
}
