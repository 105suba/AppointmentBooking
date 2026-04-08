using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MVC.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _repo;

        public AppointmentController(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index(){
            var role = HttpContext.Session.GetString("Role");
    var email = HttpContext.Session.GetString("Email");

    var data = _repo.GetAll();

    if (role == "User")
    {
        data = data.Where(a => a.UserEmail == email).ToList();
    }

    return View(data);
    }
        public IActionResult Search()
{
    ViewBag.StatusList = Enum.GetValues(typeof(AppointmentStatus))
        .Cast<AppointmentStatus>()
        .Select(e => new SelectListItem
        {
            Value = e.ToString(),
            Text = e.ToString()
        });

    return View();
}
[HttpPost]
public IActionResult Search(string searchTerm, DateTime? date, AppointmentStatus? status)
{
    var results = _repo.Search(searchTerm, date, status);

    ViewBag.StatusList = Enum.GetValues(typeof(AppointmentStatus))
        .Cast<AppointmentStatus>()
        .Select(e => new SelectListItem
        {
            Value = e.ToString(),
            Text = e.ToString()
        });

    return View(results);
}

        public IActionResult Create() {
             ViewBag.StatusList = Enum.GetValues(typeof(AppointmentStatus))
        .Cast<AppointmentStatus>()
        .Select(e => new SelectListItem
        {
            Value = e.ToString(),
            Text = e.ToString()
        });

    return View();
        }
    [HttpPost]

public IActionResult Create(Appointment appt)
{
    //  assign BEFORE validation
    appt.UserEmail = HttpContext.Session.GetString("Email");
    appt.Status = AppointmentStatus.Pending;

    if (_repo.IsSlotBooked(appt.Date))
    {
        ModelState.AddModelError("Date", "This time slot is already booked");
    }

    if (ModelState.IsValid)
    {
        _repo.Add(appt);
        return RedirectToAction("Index");
    }

    return View(appt);
}
//         [HttpPost]
// public IActionResult Create(Appointment appt)
// {
//     if (ModelState.IsValid)
//     {
//         _repo.Add(appt);
//         return RedirectToAction("Index");
//     }

//     ViewBag.StatusList = Enum.GetValues(typeof(AppointmentStatus))
//         .Cast<AppointmentStatus>()
//         .Select(e => new SelectListItem
//         {
//             Value = e.ToString(),
//             Text = e.ToString()
//         });

//     return View(appt);
// }
        public IActionResult Details(int id){
            var appt=_repo.GetById(id);
            return View(appt);
        }

        public IActionResult Edit(int id)
        {
            var appt = _repo.GetById(id);
            return View(appt);
        }

        // [HttpPost]
        // public IActionResult Edit(Appointment appt)
        // {
        //     _repo.Update(appt);
        //     return RedirectToAction("Index");
        // }

[HttpPost]
public IActionResult Edit(Appointment appt)
{
    var existing = _repo.GetById(appt.Id);

    if (existing.Date != appt.Date && _repo.IsSlotBooked(appt.Date))
    {
        ModelState.AddModelError("Date", "This time slot is already booked");
    }

    if (ModelState.IsValid)
    {
        _repo.Update(appt);
        return RedirectToAction("Index");
    }

    return View(appt);
}
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}