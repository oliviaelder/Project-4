using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Appt_Scheduler.ViewModels
{
    public class DoctorViewModel
    {
          
        public int SelectedDoctorId { get; set; }
        public List<SelectListItem> Doctors { get; set; }
    }
}
