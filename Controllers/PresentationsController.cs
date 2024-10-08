﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Infrastructure;
using ToaPro.Models;


namespace ToaPro.Controllers
{
    public class PresentationsController : Controller
    {
        private IIntexRepository _repo;
        private SignInManager<ToaProUser> _signInManager;

        public PresentationsController (IIntexRepository tempRepo, SignInManager<ToaProUser> tempSignInManager)
        {
            _repo = tempRepo;
            _signInManager = tempSignInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PresentationSchedule()
        {
            var judges = _repo.Judges.Include(j => j.ToaProUser).ToList()
                        //.Where(x => x.COLUM == value)
                        .OrderBy(x => x.Id).ToList();
            return View(judges);
           
            //Croordinatior view shows Judges, Prof, Group, and rooms, times. Can edit table! 

            //Judge View shows the room number time and judge name, along with the group number. Can request new time

            //Prof view shows the room number, time, and prof name, along with the group number


            //Add functionality to load the presentation page based on user type (Coord, Judge, Prof)
            //Coordinator can edit presentation schedule and judges can request new timeslots
        }
        public IActionResult StudentViewSchedule()
        {
            return View();
        }

        //Made some changes here with requesting new time (removed the judge availability model and placed its data inside the judge model).
        //We did not need both models.
        [HttpGet]
        public IActionResult JudgeRequestNewTime() 
        {
            return View(new Judge());
        }
        [HttpPost]
        public IActionResult JudgeRequestNewTime(Judge judge)
        {
            _repo.RequestAvailability(judge);

            return View(new Judge());
        }

        //If anything breaks here, note that I removed the student availability model and moved its data into the student model. We did not need both of them
        [HttpGet]
        public IActionResult StudentRequestNewTime()
        {
            return View(new Student());
        }
        [HttpPost]
        public IActionResult StudentRequestNewTime(Student student)
        {
            _repo.StudentRequestAvailability(student);

            return View(new Student());
        }

        // Action to get judge data by ID
        [HttpGet]
        public IActionResult GetJudgeData(Judge judge)
        {
            // Fetch judge data from the database based on the provided ID
            // Replace this with your actual data retrieval logic
            var judge2 = _repo.GetJudgeById(judge.Id);

            // Assuming you're returning JSON data
            return Json(judge2);
        }


        [HttpGet]
        public async Task<IActionResult> JudgeChangeAvailability()
        {
            ToaProUser? currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            bool isJudge = await _signInManager.UserManager.IsInRoleAsync(currentUser, "Judge");

            if (!isJudge)
            {
                return RedirectToAction("Index", "Home");
            }

            Judge? userJudge = _repo.GetJudgeById(currentUser.Id);

            if (userJudge != null)
            {
                return View(userJudge);
            }

            return RedirectToAction("JudgeIndex", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> JudgeChangeAvailability(Judge updatedJudge)
        {
            ToaProUser? currentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);

            if (currentUser == null || currentUser.Id != updatedJudge.Id)
            {
                return RedirectToAction("Index", "Home");
            }

            bool isJudge = await _signInManager.UserManager.IsInRoleAsync(currentUser, "Judge");

            if (!isJudge)
            {
                return RedirectToAction("Index", "Home");
            }

            Judge? userJudge = _repo.GetJudgeById(currentUser.Id);

            if (userJudge == null)
            {
                return View("Index", "Home");
            } else
            {
                userJudge.JudgeAvailability = updatedJudge.JudgeAvailability;
                _repo.UpdateJudgeAvailability(userJudge);
            }

            return RedirectToAction("JudgeIndex", "Home");
        }
    }
}
