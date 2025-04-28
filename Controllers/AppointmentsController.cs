using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Appt_Scheduler.Models;
using Appt_Scheduler.Data;

namespace Appt_Scheduler.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly DomainContext _context;

        public AppointmentsController(DomainContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var domainContext = _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient);
            return View(await domainContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create(int? patientId)
        {
            if (patientId == null)
            {
                return RedirectToAction("Index", "Home"); // If no patient is selected, redirect back to Home
            }

            // Get the patient details based on the PatientId
            var patient = _context.Patients
                .Where(p => p.PatientId == patientId)
                .FirstOrDefault();

            if (patient == null)
            {
                return RedirectToAction("Index", "Home"); // Patient ID provided, but patient not found
            }

            // Pass the selected patient to the View
            ViewBag.PatientId = patientId;
            ViewBag.SelectedPatient = $"{patient.LName}, {patient.FName}";

            // Get the list of doctors for the DoctorId dropdown
            var doctors = _context.Doctors
                .Select(d => new { d.DoctorId, FullName = d.DoctorFullName })
                .ToList();

            ViewBag.DoctorId = new SelectList(doctors, "DoctorId", "FullName");

            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Appointment appointment)
        {
            // Prevent tracking related entities
            appointment.Doctor = null;
            appointment.Patient = null;

            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                // Now update the corresponding DoctorAvailability row
                var availability = _context.DoctorAvailabilities.FirstOrDefault(da =>
                    da.DoctorId == appointment.DoctorId &&
                    da.AvailDate == appointment.ApptDate && // compare DateOnly to DateOnly
                    da.StartTime == appointment.ApptTime);



                if (availability != null)
                {
                    availability.IsBooked = true;
                    _context.SaveChanges(); // Save the update
                }

                // Set success message in TempData
                TempData["AppointmentMessage"] = $"Appointment scheduled on {appointment.ApptDate:dddd, MMMM dd, yyyy} at {appointment.ApptTime:hh:mm tt}.";

                // Stay on the current page without redirecting
                return RedirectToAction("PatientAppointments", new { patientId = appointment.PatientId });
            }

            // Reload dropdown and selected patient in case of error
            ViewBag.DoctorId = new SelectList(_context.Doctors, "DoctorId", "DoctorFullName", appointment.DoctorId);

            var patient = _context.Patients.Find(appointment.PatientId);
            ViewBag.SelectedPatient = patient != null ? $"{patient.LName}, {patient.FName}" : "Unknown";
            return View(appointment);
        }
        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id, string returnUrl, int? patientId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(appointment);
        }



        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Appointment appointment, string returnUrl)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(appointment);
        }



        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id, string returnUrl)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            ViewBag.ReturnUrl = returnUrl; // ✅ Pass it into the view

            return View(appointment);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string returnUrl)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                TempData["AppointmentMessage"] = "Appointment deleted successfully.";
            }

            // Redirect to the returnUrl if it's passed in the query string
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            // Default redirect if returnUrl is not provided (you can set a default here)
            return RedirectToAction("Index", "Home");
        }




        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }


        // Get available dates for a doctor
        //[HttpGet]
        //public IActionResult GetAvailableDates(int doctorId)
        //{
        //    var availableDates = _context.DoctorAvailabilities
        //        .Where(da => da.DoctorId == doctorId && !da.IsBooked)
        //        .Select(da => da.AvailDate.ToString("yyyy-MM-dd")) // Format for JavaScript date input
        //        .Distinct()
        //        .ToList();

        //    return Json(availableDates);
        //}

        // Get available times for a doctor and a selected date
        [HttpGet]
        public IActionResult GetAvailableTimes(int doctorId, DateOnly date)
        {
            // The rest of the method stays the same
            var times = _context.DoctorAvailabilities
                .Where(d => d.DoctorId == doctorId && d.AvailDate == date && !d.IsBooked)
                .Select(d => new {
                    value = d.StartTime.ToString(@"hh\:mm"), // TimeSpan string for value
                    label = DateTime.Today.Add(d.StartTime.ToTimeSpan()).ToString("h:mm tt") // Readable AM/PM label
                })
                .ToList();

            return Json(times);
        }




        //New method to get only current patients appointments
        public IActionResult PatientAppointments(int patientId)
        {
            var patientAppointments = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.PatientId == patientId)
                .ToList();

            var patient = _context.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patient != null)
            {
                ViewBag.PatientName = $"{patient.FName} {patient.LName}";
                ViewBag.PatientId = patientId; 
            }

            return View("PatientAppointments", patientAppointments);
        }

        public IActionResult ViewSchedule(int doctorId)
        {
            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.DoctorId == doctorId)
                .ToList();


            // Get the doctor's details to display with the appointments (optional)
            var doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);

            if (doctor == null)
            {
                return NotFound();
            }

            var viewModel = new DoctorScheduleViewModel
            {
                DoctorName = doctor.DoctorFullName,
                Appointments = appointments
            };
            ViewBag.DoctorName = doctor.DoctorFullName; 
            ViewBag.DoctorId = doctorId;
            return View(viewModel);
        }


    }
}
