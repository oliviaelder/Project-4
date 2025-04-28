using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Appt_Scheduler.Models
{
    public class PatientViewModel
    {
        public int SelectedPatientId { get; set; }
        public List<SelectListItem> Patients { get; set; }
    }
}
