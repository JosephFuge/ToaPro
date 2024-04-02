using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToaPro.Models;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult RequestNewTime() 
        {
            return View(new Judge());
        }
        [HttpPost]
        public IActionResult RequestNewTime(Judge judge)
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


    }
}
