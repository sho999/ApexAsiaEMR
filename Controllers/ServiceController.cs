using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ApexAsiaDAL;
using EMRhelper;

namespace ApexAsiaEMR.Controllers
{
    public class ServiceController : Controller
    {
        private ApexAsiaDAL.DAL _aamcDal = new ApexAsiaDAL.DAL();
        //
        // GET: /Service/

        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public JsonResult GetAppointmentByPhysicianId(int physicianId, DateTime fromDateTime, DateTime toDateTime, bool includeHasSeen){
            List<viewAppointment> apptViews = this._aamcDal.GetAppointmentViewsByPhysicianId(physicianId, fromDateTime, toDateTime, includeHasSeen);
            return Json(apptViews, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteAppointmentById(int appointmentId)
        {
            bool result = this._aamcDal.DeleteAppointment(appointmentId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAppointmentById(int appointmentId)
        {
            Appointment result = this._aamcDal.GetAppointmentById(appointmentId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAppointmentSummaryByDate(string fromDateTime, string toDateTime, bool includeHasSeen)
        {
            DateTime dtFromDateTime = Convert.ToDateTime(fromDateTime);
            DateTime dtToDateTime = Convert.ToDateTime(toDateTime);

            List<GetAppointmentSummary_Result> result = this._aamcDal.GetAppointmentSummaryByDate(dtFromDateTime, dtToDateTime, includeHasSeen);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAppointment(Appointment appointment)
        {
            bool result = false;
            if (appointment != null)
            {
                if (appointment.Id > 0)
                {
                    // Update Appointment
                    Appointment existingAppt = this._aamcDal.GetAppointmentById(appointment.Id);
                    if(existingAppt != null){
                        existingAppt.Title = appointment.Title;
                        existingAppt.ReasonForAppointment = appointment.ReasonForAppointment;
                        existingAppt.IsHomeVisit = appointment.IsHomeVisit;
                        existingAppt.UpdatedBy = this.User.Identity.Name;
                        existingAppt.UpdatedDateTime = DateTime.Now;
                        try
                        {
                            this._aamcDal.UpdateAppointment(existingAppt);
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            result = false;
                        }
                    }
                }
                else
                {
                    // Insert New Appointment
                    appointment.DayOfTheWeek = (int)appointment.StartDateTime.DayOfWeek;
                    
                    appointment.EnteredBy = this.User.Identity.Name;
                    appointment.EnteredDateTime = DateTime.Now;
                    try
                    {
                        this._aamcDal.InsertAppointment(appointment);
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }
                }
            }

 
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAppointmentTable(ViewModels.AppointmentKeyObj appointmentKey)
        {
            List<ViewModels.AppointmentTableVM> result = new List<ViewModels.AppointmentTableVM>();
            DateTime selectedDate = appointmentKey.AppointmentDate;
            int dayOfWeek = (int)selectedDate.DayOfWeek;
             
            List<PhysicianSchedule> physicianSchedules =this._aamcDal.GetPhysicianScheduleByIdAndDay(appointmentKey.PhysicianTitleId, dayOfWeek);
            List<viewAppointment> appointments = this._aamcDal.GetAppointmentByPhysicianIdAndDate(appointmentKey.PhysicianTitleId, selectedDate);
           
            if (physicianSchedules != null && physicianSchedules.Count > 0)
            {
                foreach (PhysicianSchedule ps in physicianSchedules)
                {
                    DateTime startTime = Convert.ToDateTime(ps.StartDateTime);
                    DateTime endTime = Convert.ToDateTime(ps.EndDateTime);
                    ViewModels.AppointmentTableVM appt = new ViewModels.AppointmentTableVM();
                    appt.TimeSlot = startTime.ToShortTimeString();
                    appt.EndTime = startTime.AddMinutes(15).ToShortTimeString();
                    appt.AppointmentDate = startTime.ToShortDateString();
                    // We are drawing the timeslots for doctor schedule every 15 minutes.
                    result.Add(appt);
                    while (startTime < endTime)
                    {
                        startTime = startTime.AddMinutes(15);
                        ViewModels.AppointmentTableVM apptEvery15 = new ViewModels.AppointmentTableVM();
                        apptEvery15.TimeSlot = startTime.ToShortTimeString();
                        apptEvery15.EndTime = startTime.AddMinutes(15).ToShortTimeString();
                        apptEvery15.AppointmentDate = startTime.ToShortDateString();
                        result.Add(apptEvery15);
                    }
                    // Now we need to show the existing appointments in the system.
                    if(result != null && result.Count > 0){
                        foreach( ViewModels.AppointmentTableVM apptRow in result){
                            ViewModels.AppointmentTableVM currentRow = apptRow;
                            
                            foreach(viewAppointment a in appointments){
                                if (currentRow.TimeSlot == a.StartDateTime.ToShortTimeString())
                                {
                                    currentRow.AppointmentId = a.AppointmentId;
                                    currentRow.AppointmentDate = a.StartDateTime.ToShortDateString();
                                    currentRow.AppointmentTitle = a.Title;
                                    currentRow.DoctorName = a.DoctorName;
                                    currentRow.PatientName = a.PatientName;
                                    currentRow.PatientId = a.PatientId;
                                    currentRow.Reason = a.ReasonForAppointment;
                                    currentRow.IsHomeVisit = (a.IsHomeVisit) ? "Yes" : string.Empty ;
                                    break;
                                }
                            }
                        }
                    }

                }

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPhysicianSchedulesById(int physicianId)
        {
            List<PhysicianSchedule> result = new List<PhysicianSchedule>();
            result = this._aamcDal.GetPhysicianScheduleById(physicianId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchPatient(DAL.PatientSearchObj searchObj)
        {
            if (!String.IsNullOrEmpty(searchObj.SearchString))
            {
                string searchStringWithoutPrefix = helper.RemovePrefixForSearchString(searchObj.SearchString);
                searchObj.SearchString = searchStringWithoutPrefix;
            }
            List<Patient> result = this._aamcDal.SearchPatient(searchObj);
           
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       

        [HttpPost]
        public JsonResult SearchPatientDOB(string strDOB)
        {
            List<Patient> result = this._aamcDal.SearchPatientDOB(strDOB);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SavePhysicianSchedules(Models.PhysicianScheculesArrayObject scheduleArrayObj)
        {
            bool result = false;
            try
            {
                if (scheduleArrayObj != null && scheduleArrayObj.PhysicianScheculesArray.Length > 0)
                {
                    // Delete existing records for given PhysicianTitleId
                    if (scheduleArrayObj.PhysicianTitleId > 0)
                    {
                        this._aamcDal.DeletePhysicianScheduleById(scheduleArrayObj.PhysicianTitleId);
                    }
                    int arrayLength = scheduleArrayObj.PhysicianScheculesArray.Length;
                    for (int i = 0; i < arrayLength; i++)
                    {
                        ApexAsiaDAL.PhysicianSchedule physicianSchedule = new ApexAsiaDAL.PhysicianSchedule();
                        physicianSchedule.PhysicianTitleId = scheduleArrayObj.PhysicianScheculesArray[i].PhysicianTitleId;
                        physicianSchedule.DayOfWeekId = scheduleArrayObj.PhysicianScheculesArray[i].DayOfWeekId;
                        physicianSchedule.StartDateTime = scheduleArrayObj.PhysicianScheculesArray[i].StartDateTime;
                        physicianSchedule.EndDateTime = scheduleArrayObj.PhysicianScheculesArray[i].EndDateTime;
                        physicianSchedule.Note = scheduleArrayObj.PhysicianScheculesArray[i].Note;
                        this._aamcDal.InsertPhysicianSchedule(physicianSchedule);
                    }
                }
                result = true;
            }
            catch { result = false; }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
       
        [HttpPost]
        public JsonResult Login(Models.LoginObjModel loginModel)
        {
                bool result = this._aamcDal.IsValidUser(loginModel.Username, loginModel.Password);
                if (result)
                {
                    FormsAuthentication.SetAuthCookie(loginModel.Username, false);
                    Session["currentUserName"] = loginModel.Username;
                    //return Redirect(Url.Action("Index", "Admin"));
                }
                else
                {
                    result = false;
                }
                return Json(result, JsonRequestBehavior.AllowGet);

        }

        

        [HttpPost]
        [Authorize]
        public JsonResult AddUser(Models.UserModel userModel)
        {
            bool result = false;
            try
            {
                ApexAsiaDAL.User newUser = new ApexAsiaDAL.User();
                
                newUser.Username = userModel.Username;
                newUser.LoweredUsername = userModel.Username.ToLower();
                newUser.FullName = userModel.FullName;
                newUser.Password = userModel.Password;
                newUser.PasswordQuestion = userModel.PasswordQuestion;
                newUser.PasswordAnswer = userModel.PasswordAnswer;
                newUser.Email = userModel.Email;
                newUser.CreatedDateTime = DateTime.Now;
                newUser.Comment = userModel.Comment;

                result = this._aamcDal.InsertNewUser(newUser);
                
            }
            catch
            {
                result = false;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
