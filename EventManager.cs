using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApexAsiaDAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security;

namespace ApexAsiaEMR
{
    public class EventManager
    {
        private DAL dal = new ApexAsiaDAL.DAL();
        public class Event
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public int PatientId { get; set; }
            public int PhysicianId { get; set; }
        }

        public DataTable FilteredData(DateTime start, DateTime end)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Appointment] WHERE NOT (([EndDateTime] <= @start) OR ([StartDateTime] >= @end))", ConfigurationManager.ConnectionStrings["daypilotConnection"].ConnectionString);
            da.SelectCommand.Parameters.AddWithValue("start", start);
            da.SelectCommand.Parameters.AddWithValue("end", end);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public void EventEdit(string id, string name, DateTime start, DateTime end, string reason, string isHomeVisit, string userName)
        {
            if (!String.IsNullOrEmpty(id))
            {
                int intId = Convert.ToInt32(id);
                
                Appointment appointment = dal.GetAppointmentById(intId);
                if (appointment != null)
                {
                    appointment.StartDateTime = start;
                    appointment.EndDateTime = end;
                    appointment.Title = name;
                    appointment.ReasonForAppointment = reason;
                    appointment.IsHomeVisit = isHomeVisit == "on" ? true : false;
                    appointment.UpdatedBy = userName;
                    appointment.UpdatedDateTime = DateTime.Now;

                    dal.UpdateAppointment(appointment);
                }
            }

            //using (var con = Db.CreateConnection())
            //{
            //    con.Open();

            //    var cmd = Db.CreateCommand("UPDATE [event] SET [name] = @name, [eventstart] = @start, [eventend] = @end WHERE [id] = @id", con);
            //    Db.AddParameterWithValue(cmd, "id", id);
            //    Db.AddParameterWithValue(cmd, "start", start);
            //    Db.AddParameterWithValue(cmd, "end", end);
            //    Db.AddParameterWithValue(cmd, "name", name);
            //    cmd.ExecuteNonQuery();
            //}
        }

        //public void EventMove(string id, DateTime start, DateTime end)
        //{
        //    using (var con = Db.CreateConnection())
        //    {
        //        con.Open();

        //        var cmd = Db.CreateCommand("UPDATE [event] SET [eventstart] = @start, [eventend] = @end WHERE [id] = @id", con);
        //        Db.AddParameterWithValue(cmd, "id", id);
        //        Db.AddParameterWithValue(cmd, "start", start);
        //        Db.AddParameterWithValue(cmd, "end", end);
        //        cmd.ExecuteNonQuery();
        //    }

        //}

        public Appointment GetAppointmentById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                int intId = Convert.ToInt32(id);
                DAL dal = new ApexAsiaDAL.DAL();
                return dal.GetAppointmentById(intId);
            }
            else
            {
                return null;
            }
        }

        public void EventCreate(int patientId, int physicianId, DateTime start, DateTime end, string name, string reason, string isHomeVisit, string userName)
        {
            int dayOfWeek = (int)start.DayOfWeek;
            Appointment appointment = new Appointment();
            appointment.StartDateTime = start;
            appointment.EndDateTime = end;
            appointment.DayOfTheWeek = (int)start.DayOfWeek;
            appointment.PatientId = patientId;
            appointment.PhysicianId = physicianId;
            appointment.Title = name;
            appointment.ReasonForAppointment = reason;
            appointment.IsHomeVisit = isHomeVisit == "on" ? true : false;
            appointment.HasSeen = false; // this is the first time created this appointment. So, HasSeen is set to false!
            appointment.EnteredBy = userName;
            appointment.EnteredDateTime = DateTime.Now;
            
            dal.InsertAppointment(appointment);

        }


        public void EventDelete(string id)
        {
            if(!String.IsNullOrEmpty(id)){
                int appointmentId = Convert.ToInt32(id);
                dal.DeleteAppointment(appointmentId);
            }
        }
    }
}