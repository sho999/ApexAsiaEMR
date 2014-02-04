using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

using ApexAsiaDAL;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Json;
using DayPilot.Web.Ui.Data;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Events.Calendar;
using System.Web.Security;
//using DayPilot.Web.Ui.Data;

namespace ApexAsiaEMR.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        
        public ActionResult Backend()
        {
            return new Dpc().CallBack(this);
        }

        public ActionResult Day()
        {
            return new Dpc().CallBack(this);
        }

        public ActionResult Week()
        {
            return new Dpc().CallBack(this);
        }

        public class Dpc : DayPilotCalendar
        {
            protected override void OnInit(InitArgs initArgs)
            {
                Update(CallBackUpdateType.Full);
                //Events = new EventManager().FilteredData(StartDate, StartDate.AddDays(Days)).AsEnumerable();
                //DataStartField = "StartDateTime";
                //DataEndField = "EndDateTime";
                //DataTextField = "Title";
                //DataIdField = "Id";
                UpdateWithMessage("Welcome!");
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                Events = new EventManager().FilteredData(StartDate, StartDate.AddDays(Days)).AsEnumerable();

                DataStartField = "StartDateTime";
                DataEndField = "EndDateTime";
                DataTextField = "Title";
                DataIdField = "Id";
            }
           
            protected override void OnBeforeEventRender(BeforeEventRenderArgs e)
            {
                e.Areas.Add(new Area().Right(3).Top(3).Width(15).Height(15).CssClass("event_action_delete").JavaScript("switcher.active.control.commandCallBack('delete', {'e': e});"));
            }


            protected override void OnCommand(CommandArgs e)
            {
                switch (e.Command)
                {
                    case "navigate":
                        StartDate = (DateTime)e.Data["day"];
                        Update(CallBackUpdateType.Full);
                        break;
                    case "refresh":
                        Update(CallBackUpdateType.EventsOnly);
                        break;
                    case "delete":
                        if (Roles.IsUserInRole("Admin") || Roles.IsUserInRole("Manager"))
                        {
                            new EventManager().EventDelete((string)e.Data["e"]["id"]);
                            Update(CallBackUpdateType.EventsOnly);
                        }
                        break;
                }
            }
        }

        public ActionResult Edit(string id)
        {
            var e = new EventManager().GetAppointmentById(id);
            return View(e);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection form)
        {
            DateTime start = Convert.ToDateTime(form["StartDateTime"]);
            DateTime end = Convert.ToDateTime(form["EndDateTime"]);
            new EventManager().EventEdit(form["Id"], form["Title"], start, end, form["ReasonForAppointment"], form["IsHomeVisit"], User.Identity.Name.ToString());
            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }


        public ActionResult Create()
        {
            return View(new EventManager.Event
            {
                Start = Convert.ToDateTime(Request.QueryString["start"]),
                End = Convert.ToDateTime(Request.QueryString["end"]),
                PatientId = Convert.ToInt32(Request.QueryString["patientId"]),
                PhysicianId = Convert.ToInt32(Request.QueryString["physicianId"])
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            DateTime start = Convert.ToDateTime(form["Start"]);
            DateTime end = Convert.ToDateTime(form["End"]);
            int patientId = !String.IsNullOrEmpty(form["PatientId"]) ? Convert.ToInt32(form["PatientId"]) : 0;
            int physicianId = !String.IsNullOrEmpty(form["PhysicianId"]) ? Convert.ToInt32(form["PhysicianId"]) : 0;
            new EventManager().EventCreate(patientId, physicianId, start, end, form["Text"], form["ReasonForAppointment"], form["IsHomeVisit"], User.Identity.Name);
            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }


        public class Db
        {

            private static string ConnectionString
            {
                get
                {
                    return ConfigurationManager.ConnectionStrings["daypilotConnection"].ConnectionString;
                }
            }

            private static DbProviderFactory Factory
            {
                get
                {
                    return DbProviderFactories.GetFactory(FactoryName());
                }
            }

            private static string FactoryName()
            {
               
                return "System.Data.SqlClient";
            }

            private static string IdentityCommand()
            {
                switch (FactoryName())
                {
                    case "System.Data.SQLite":
                        return "select last_insert_rowid();";
                    case "System.Data.SqlClient":
                        return "select @@identity;";
                    default:
                        throw new NotSupportedException("Unsupported DB factory.");
                }
            }


            private static string GetNew()
            {
                string today = DateTime.Today.ToString("yyyy-MM-dd");
                string guid = Guid.NewGuid().ToString();
                
                string path =  guid;



                return String.Format("Data Source={0}", path);
            }

            public static DbDataAdapter CreateDataAdapter(string select)
            {
                DbDataAdapter da = Factory.CreateDataAdapter();
                da.SelectCommand = CreateCommand(select);
                return da;
            }


            public static DbConnection CreateConnection()
            {
                DbConnection connection = Factory.CreateConnection();
                connection.ConnectionString = ConnectionString;
                return connection;
            }

            public static DbCommand CreateCommand(string text)
            {
                DbCommand command = Factory.CreateCommand();
                command.CommandText = text;
                command.Connection = CreateConnection();

                return command;
            }

            public static DbCommand CreateCommand(string text, DbConnection connection)
            {
                DbCommand command = Factory.CreateCommand();
                command.CommandText = text;
                command.Connection = connection;

                return command;
            }

            public static void AddParameterWithValue(DbCommand cmd, string name, object value)
            {
                var parameter = Factory.CreateParameter();
                parameter.Direction = ParameterDirection.Input;
                parameter.ParameterName = name;
                parameter.Value = value;
                cmd.Parameters.Add(parameter);
            }

            public static int GetIdentity(DbConnection c)
            {
                var cmd = CreateCommand(Db.IdentityCommand(), c);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

        }

        //public class EventManager
        //{
        //    public class Event
        //    {
        //        public string Id { get; set; }
        //        public string Text { get; set; }
        //        public DateTime Start { get; set; }
        //        public DateTime End { get; set; }
        //        public int PatientId { get; set; }
        //        public int PhysicianId { get; set; }
        //    }

        //    public DataTable FilteredData(DateTime start, DateTime end)
        //    {
        //        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Appointment] WHERE NOT (([EndDateTime] <= @start) OR ([StartDateTime] >= @end))", ConfigurationManager.ConnectionStrings["daypilotConnection"].ConnectionString);
        //        da.SelectCommand.Parameters.AddWithValue("start", start);
        //        da.SelectCommand.Parameters.AddWithValue("end", end);

        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        return dt;
        //    }

        //    public void EventEdit(string id, string name, DateTime start, DateTime end, string reason, string isHomeVisit, string userName)
        //    {
        //        if(!String.IsNullOrEmpty(id)){
        //            int intId = Convert.ToInt32(id);
        //            DAL dal = new ApexAsiaDAL.DAL();
        //            Appointment appointment = dal.GetAppointmentById(intId);
        //            if (appointment != null)
        //            {
        //                appointment.StartDateTime = start;
        //                appointment.EndDateTime = end;
        //                appointment.Title = name;
        //                appointment.ReasonForAppointment = reason;
        //                appointment.IsHomeVisit = isHomeVisit == "on" ? true : false;
        //                appointment.UpdatedBy = userName;
        //                appointment.UpdatedDateTime = DateTime.Now;

        //                dal.UpdateAppointment(appointment);
        //            }
        //        }
                
        //        //using (var con = Db.CreateConnection())
        //        //{
        //        //    con.Open();

        //        //    var cmd = Db.CreateCommand("UPDATE [event] SET [name] = @name, [eventstart] = @start, [eventend] = @end WHERE [id] = @id", con);
        //        //    Db.AddParameterWithValue(cmd, "id", id);
        //        //    Db.AddParameterWithValue(cmd, "start", start);
        //        //    Db.AddParameterWithValue(cmd, "end", end);
        //        //    Db.AddParameterWithValue(cmd, "name", name);
        //        //    cmd.ExecuteNonQuery();
        //        //}
        //    }

        //    public void EventMove(string id, DateTime start, DateTime end)
        //    {
        //        using (var con = Db.CreateConnection())
        //        {
        //            con.Open();

        //            var cmd = Db.CreateCommand("UPDATE [event] SET [eventstart] = @start, [eventend] = @end WHERE [id] = @id", con);
        //            Db.AddParameterWithValue(cmd, "id", id);
        //            Db.AddParameterWithValue(cmd, "start", start);
        //            Db.AddParameterWithValue(cmd, "end", end);
        //            cmd.ExecuteNonQuery();
        //        }

        //    }

        //    public Appointment GetAppointmentById(string id)
        //    {
        //        if (!String.IsNullOrEmpty(id))
        //        {
        //            int intId = Convert.ToInt32(id);
        //            DAL dal = new ApexAsiaDAL.DAL();
        //            return dal.GetAppointmentById(intId);
        //        }
        //        else { 
        //            return null; 
        //        }   
        //    }

        //    public void EventCreate(int patientId, int physicianId, DateTime start, DateTime end, string name, string reason, string isHomeVisit)
        //    {
        //        int dayOfWeek = (int)start.DayOfWeek;
        //        Appointment appointment = new Appointment();
        //        appointment.StartDateTime = start;
        //        appointment.EndDateTime = end;
        //        appointment.DayOfTheWeek = (int)start.DayOfWeek;
        //        appointment.PatientId = patientId;
        //        appointment.PhysicianId = physicianId;
        //        appointment.Title = name;
        //        appointment.ReasonForAppointment = reason;
        //        appointment.IsHomeVisit = isHomeVisit == "on" ? true : false;
        //        appointment.HasSeen = false; // this is the first time created this appointment. So, HasSeen is set to false!
        //        appointment.EnteredDateTime = DateTime.Now;
        //        DAL dal = new ApexAsiaDAL.DAL();
        //        dal.InsertAppointment(appointment);
               
        //    }

            
        //    public void EventDelete(string id)
        //    {
        //        using (var con = Db.CreateConnection())
        //        {
        //            con.Open();

        //            var cmd = Db.CreateCommand("DELETE FROM [Appointment] WHERE Id = @id", con);
        //            Db.AddParameterWithValue(cmd, "id", id);
        //            cmd.ExecuteNonQuery();

        //        }
        //    }

        //}

    }
}
