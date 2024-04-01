using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
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
            var judges = from Judge in _repo.Judges
                         join ToaProUser in _repo.ToaProUsers
                         on Judge.user_id equals ToaProUser.user_id
                         select new Judge
                         {
                             Id = Judge.Id,
                             user_id = Judge.user_id,
                             semester_id = Judge.semester_id,
                             Affiliation = Judge.Affiliation,
                             TimeSlot1 = Judge.TimeSlot1,
                             TimeSlot2 = Judge.TimeSlot2,
                             TimeSlot3 = Judge.TimeSlot3,
                             TimeSlot4 = Judge.TimeSlot4,
                             TimeSlot5 = Judge.TimeSlot5,
                             FirstName = ToaProUser.FirstName,
                             LastName = ToaProUser.FirstName
                         };
                                //idk how to do this part or what they do?
                                //Ranking  = new List<Ranking>();
                                //Presentation = new List<Presentation>();
                        //.Where(x => x.COLUM == value)
                        //.OrderBy(x => x.Id).ToList();
              
            return View(judges);
            //return View(judges);
           
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
        public IActionResult JRequestNewTime(int id)
        {
            _repo.JRequestAvailability(id);
            return View("JRequestNewTime");
        }
        [HttpPost]
        public IActionResult JRequestNewTime(Judge updatedInfo)
        {
            _repo.JUpdateAvailability(updatedInfo);

            return RedirectToAction("JRequestNewTime"); //instead of going to the view MovieList, it will go to the ACTION
        }


    }
}
