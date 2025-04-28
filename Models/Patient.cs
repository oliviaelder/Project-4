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
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public string FName { get; set; }
        
        [Required]
        public string LName { get; set; }

        [Required]
        public DateOnly DOB { get; set; }

        [RegularExpression("Male|Female|Other", ErrorMessage = "Sex must be Male, Female, or Other")]
        public string Sex { get; set; }
        
        public string? Phone { get; set; }
        
        public string? Email { get; set; }


        // Navigation Property for Appointments
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}
