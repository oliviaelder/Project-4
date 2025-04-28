using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Appt_Scheduler.Models;

namespace Appt_Scheduler.Models
{
    public class DoctorScheduleViewModel
    {
        public string DoctorName { get; set; }
        public int DoctorId { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }

}
