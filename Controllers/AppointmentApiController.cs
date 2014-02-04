using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ApexAsiaDAL;

namespace ApexAsiaEMR.Controllers
{
    public class AppointmentApiController : ApiController
    {
        private ApexAsiaDAL.DAL db = new ApexAsiaDAL.DAL();

        public List<Appointment> Get()
        {
            return db.GetAllAppointments();
        }

        // POST 
        public int Post(ApexAsiaDAL.Appointment appointment)
        {
            int result = 0;
            if (appointment != null)
            {
                appointment.DayOfTheWeek = (int)appointment.StartDateTime.DayOfWeek;
                
                result = db.InsertAppointment(appointment);
            }
            return result;
        }
    }
}
