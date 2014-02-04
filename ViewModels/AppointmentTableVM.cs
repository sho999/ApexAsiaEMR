using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApexAsiaEMR.ViewModels
{
    public class AppointmentTableVM
    {
        public int AppointmentId { get; set; }
        public string AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public string EndTime { get; set; } // this time is 15 minutes plus the startTime or TimeSlot in this case.
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string AppointmentTitle { get; set; }
        public string Reason { get; set; }
        public string IsHomeVisit { get; set; } // set it to string to display Yes or No or blank

    }
}