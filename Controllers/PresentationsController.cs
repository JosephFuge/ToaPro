using Microsoft.AspNetCore.Mvc;

namespace ToaPro.Controllers
{
    public class PresentationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PresentationSchedule()
        {
            //Add functionality to load the presentation page based on user type (Coord, Judge, Prof)
            //Coordinator can edit presentation schedule and judges can request new timeslots
            return View();
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
         
        public IActionResult AssignJudgeToRoom()
        {
            return View();
        }

    }
}
