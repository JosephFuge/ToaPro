using Microsoft.AspNetCore.Mvc;
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
        public IActionResult JudgePresentationForm(string judgeId = "37020e47-bd55-4cc8-bf3d-e3288e15b7fb", int groupId = 0)
        {
            // List of rankings for the specified judge
            var judgeRankings = _repo.Rankings.Where(x => x.JudgeId.Equals(judgeId)).ToList();

            // List of Judges and their Presentations
            var judgesList = _repo.Judges.Where(x => x.Id.Equals(judgeId)).Include(x => x.Presentations).ToList();

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
                            where rankings.JudgeId.Equals(judgeId)
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
                .Where(x => x.JudgeId.Equals(judgeId) && x.GroupId == groupId)
                .FirstOrDefault();

            // Set the record to be edited to the new Ranking created in the table
            var rankingId = rankingToEdit.Id;

            // Pass in data for buttons 
            /*ViewBag.joinedData = _repo.Judges
                .Where(x => x.Id.Equals(judgeId))
                .Include(r => r.Rankings)
                .Include(p => p.Presentations)
                    .ThenInclude(g => g.Group)
                .ToList();*/

            ViewBag.joinedData = (from judges in _repo.Judges
                                  join rankings in _repo.Rankings
                                  on judges.Id equals rankings.JudgeId
                                  join groups in _repo.Groups
                                  on rankings.GroupId equals groups.Id
                                  join presentations in _repo.Presentations
                                  on rankings.PresentationId equals presentations.Id
                                  where judges.Id.Equals(judgeId)
                                  orderby presentations.StartDate
                                  select new
                                  {
                                      GroupId = groups.Id,
                                      Section = groups.Section,
                                      Number = groups.Number
                                  }
                                  ).ToList();

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
        public IActionResult TeamRankings(string judgeId = "37020e47-bd55-4cc8-bf3d-e3288e15b7fb")
        {
            ViewBag.joinedData = (from judges in _repo.Judges
                                  join rankings in _repo.Rankings
                                  on judges.Id equals rankings.JudgeId
                                  join groups in _repo.Groups
                                  on rankings.GroupId equals groups.Id
                                  join presentations in _repo.Presentations
                                  on rankings.PresentationId equals presentations.Id
                                  where judges.Id.Equals(judgeId)
                                  orderby presentations.StartDate
                                  select new
                                  {
                                      GroupId = groups.Id,
                                      Section = groups.Section,
                                      Number = groups.Number
                                  }
                                  ).ToList();

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
