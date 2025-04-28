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
    public class DoctorAvailability
    {
        [Key]
        public int AvailId { get; set; }

        [Required]
        public DateOnly AvailDate { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }
        
        public Boolean IsBooked { get; set; } = false;

        //Fk to Doctor
        public virtual Doctor Doctor { get; set; }
        public int? DoctorId { get; set; }
    }
}
