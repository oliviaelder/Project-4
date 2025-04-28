using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        public DateOnly ApptDate { get; set; }

        [Required]
        public TimeOnly ApptTime { get; set; }

        [Required]
        public string? ReasonForVisit { get; set; }

        [Required]
        [RegularExpression("Scheduled|Completed|Cancelled")]
        public string Status { get; set; } = "Scheduled";

        //FK for Patient
        [Required]
        public int PatientId { get; set; }

        //Navigation property 
        [ValidateNever]
        public Patient Patient { get; set; }
    

        //FK for Doctor
        [Required]
        public int DoctorId { get; set; }

        //Navigation property 
        [ValidateNever]
        public Doctor? Doctor { get; set; }
    }
}
