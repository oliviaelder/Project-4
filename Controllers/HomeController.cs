using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Appt_Scheduler.Models;
using Appt_Scheduler.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Appt_Scheduler.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DomainContext _context;

    public HomeController(ILogger<HomeController> logger, DomainContext context)
    {
        _logger = logger;
        _context = context;
    }

    //public IActionResult Index()
    //{
    //    var patients = _context.Patients
    //        .Select(p => new
    //        {
    //            p.PatientId,
    //            FullInfo = $"{p.LName}, {p.FName}  ({p.DOB:MM/dd/yyyy})"
    //        })
    //        .ToList();

    //    ViewData["Patients"] = new SelectList(patients, "PatientId", "FullInfo");
    //    return View();
    //}
    public IActionResult Index()
    {
        var patients = _context.Patients
            .Select(p => new
            {
                p.PatientId,
                FullInfo = $"{p.LName}, {p.FName} ({p.DOB:MM/dd/yyyy})"
            })
            .ToList();

        var viewModel = new PatientViewModel
        {
            Patients = patients.Select(p => new SelectListItem
            {
                Value = p.PatientId.ToString(),
                Text = p.FullInfo
            }).ToList()
        };

        return View(viewModel);
    }





    // POST: Home/Index
    [HttpPost]
    public IActionResult Index(int? patientId)
    {

        if (patientId.HasValue)
        {
            // Redirect to the Create appointment page in Appointments controller with the selected patientId
            return RedirectToAction("Create", "Appointments", new { patientId = patientId });
        }

        // In case no patient is selected, just return the view
        return View();
    }

    public IActionResult Admin()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
