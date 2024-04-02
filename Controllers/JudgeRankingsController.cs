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
        public IActionResult JudgePresentationForm(int judgeId = 1, int groupId = 0)
        {
            if (groupId == 0)
            {
                var group = _repo.Groups
                    .Include(x => x.Rankings)
                    .FirstOrDefault(x => x.Rankings.Any(r => r.JudgeId == judgeId));

                if (group != null)
                {
                    groupId = group.Id;
                }
            }

            var ranking = _repo.Rankings
                .Where(x => x.JudgeId == judgeId && x.GroupId == groupId)
                .FirstOrDefault();

            if (ranking == null)
            {
                // If no ranking exists, create a new one
                ranking = new Ranking
                {
                    JudgeId = judgeId,
                    GroupId = groupId,
                    // Set any other properties of the ranking if needed
                };

                _repo.AddRanking(ranking); // Save the new ranking to the database
            }

            var rankingId = ranking.Id;


            ViewBag.joinedData = _repo.Judges
                .Where(x => x.Id == judgeId)
                .Include(r => r.Rankings)
                .Include(p => p.Presentations)
                    .ThenInclude(g => g.Group)
                .ToList();

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

            ViewBag.joinedAwards = _repo.Awards
                .Include(g => g.Group.Rankings)
                .ToList();
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

        [HttpPost]
        public IActionResult FunAwards2(Award updatedInfo)
        {
            _repo.UpdateAward(updatedInfo);

            var groupId = updatedInfo.GroupId;
            var awardId = updatedInfo.AwardId;

            return View();
        }

    }
}
