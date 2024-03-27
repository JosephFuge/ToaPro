using Microsoft.AspNetCore.Mvc;
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

        public IActionResult RequestNewTime()
        {
            //This view can be accessed by both the Student and Judge. Both will have the same functionality; however,
            //the student view will populate a text area box and a notice at the bottom (see notes in the RequestNewTime.cshtml file).
            return View();
        }
        

    }
}
