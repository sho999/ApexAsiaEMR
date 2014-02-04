using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ApexAsiaDAL;

namespace ApexAsiaEMR.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private  ApexAsiaDAL.DAL _db = new ApexAsiaDAL.DAL();
        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllAppointments()
        {
            List<viewAppointment> appointments = new List<viewAppointment>();
            //if (Roles.IsUserInRole(User.Identity.Name, "Admin"))
            //{
                appointments = this._db.GetAllViewAppointments()
                    .Where(a => a.HasSeen == false)
                    .OrderBy(a => a.DoctorName)
                    .ThenBy(a => a.StartDateTime).ToList();
            //}
            return View(appointments);
        }

        [HttpPost]
        public ActionResult AllAppointments(int id)
        {
            List<viewAppointment> appointments = new List<viewAppointment>();
            
            appointments = this._db.GetAllViewAppointments()
                .Where(a => a.HasSeen == false && a.PhysicianId == id)
                .OrderBy(a => a.DoctorName)
                .ThenBy(a => a.StartDateTime).ToList();
            //}
            return View(appointments);
        }

        public ActionResult PhysicianAppointments()
        {
            List<viewAppointment> appointments = new List<viewAppointment>();
            
            if (Roles.IsUserInRole(User.Identity.Name, "Physician"))
            {
                ApexAsiaDAL.PhysicianTitle physicianTitle = null;
                ApexAsiaDAL.User user = _db.GetUser(User.Identity.Name);
               
                if (user != null)
                {
                    physicianTitle = _db.GetPhysicianTitleByUserId(user.Id);
                    if (physicianTitle != null)
                    {
                        ViewBag.PhysicianTitle = physicianTitle;
                        appointments = _db.GetPhysicianAppointments(physicianTitle.Id)
                            .Where(a => a.HasSeen == false)
                            .OrderBy(a => a.StartDateTime).ToList();
                    }
                }
            }
            return View(appointments);
        }

        [Authorize(Roles="Physician")]
        [HttpGet]
        public ActionResult SeeAppointment(int id)
        {
            Appointment appointment = this._db.GetAppointmentById(id);
            if (appointment != null)
            {
                ViewBag.currentPatient = _db.GetPatientById(appointment.PatientId);
                return View(appointment);
                
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Physician")]
        [HttpPost]
        public ActionResult SeeAppointment(Appointment appointment)
        {
            if (appointment != null)
            {
                this._db.UpdateAppointment(appointment);
                ViewBag.currentPatient = _db.GetPatientById(appointment.PatientId);
            }
            return RedirectToAction("PhysicianAppointments", "Report");
        }
    }
}
