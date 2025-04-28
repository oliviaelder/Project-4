using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Appt_Scheduler.Models
{
    public class Doctor
    {

        [Key]
        public int DoctorId { get; set; }

        public string DoctorFullName { get; set; }
        public string? DEANumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        // Navigation Property for Appointments
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        // Navigation Property for DoctorAvailability
        public ICollection<DoctorAvailability> DoctorAvailabilities { get; set; } = new List<DoctorAvailability>();

    }
}
