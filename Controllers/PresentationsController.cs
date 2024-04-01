using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Models;

namespace ToaPro.Controllers
{
    public class PresentationsController : Controller
    {
        private IIntexRepository _repo;

        public PresentationsController (IIntexRepository temp)
        {
            _repo = temp;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PresentationSchedule()
        {
            var judges = _repo.Judges.ToList()
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

        [HttpGet]
        public IActionResult RequestNewTime(int id)
        {
            _repo.RequestAvailability(id);
            return View("RequestNewTime");
        }
        [HttpPost]
        public IActionResult RequestNewTime(Judge updatedInfo)
        {
            _repo.UpdateAvailability(updatedInfo);

            return RedirectToAction("RequestNewTime"); //instead of going to the view MovieList, it will go to the ACTION
        }

        [HttpGet]
        public IActionResult StudentRequestNewTime(int id)
        {
            _repo.SRequestAvailability(id);
            return View("StudentRequestNewTime");
        }
        [HttpPost]
        public IActionResult StudentRequestNewTime(Student updatedInfo)
        {
            _repo.SUpdateAvailability(updatedInfo);

            return RedirectToAction("StudentRequestNewTime"); //instead of going to the view MovieList, it will go to the ACTION
        }


    }
}
