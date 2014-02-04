using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApexAsiaEMR.Models;
using ApexAsiaDAL;
using System.Web.Security;

namespace ApexAsiaEMR.Controllers
{
    
    public class AdminController : Controller
    {
        private ApexAsiaDAL.DAL _db = new DAL();
        //
        // GET: /Admin/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Physician")]
        public ActionResult AddPhysicianTitle(int id)
        {
            User thisUser = this._db.GetUserById(id);
            ViewBag.currentUserId = id;
            ViewBag.currentUserFullName = thisUser.FullName;

            PhysicianTitle physicianTitle = this._db.GetPhysicianTitleByUserId(id);
            if (physicianTitle == null)
            {
                PhysicianTitle newPhysicianTitle = new PhysicianTitle();
                newPhysicianTitle.UserId = id;
                newPhysicianTitle.Degree = "";
                newPhysicianTitle.LastUpdated = DateTime.Now;
                this._db.InsertPhysicianTitle(newPhysicianTitle);

                physicianTitle = newPhysicianTitle;
            }
            
            var specialtyList = this._db.GetAllSpecialities().Select(s => new{SpecialtyId = s.Id, SpecialtyName = s.SpecialtyName});
           
            ViewBag.SpecialtyList = specialtyList;

            return View(physicianTitle);
        }



        [Authorize(Roles = "Admin,Physician")]
        [HttpPost]
        public ActionResult AddPhysicianTitle(PhysicianTitle physicianTitle)
        {
            
            if (physicianTitle != null)
            {
                physicianTitle.LastUpdated = DateTime.Now;
                this._db.UpdatePhysicianTitle(physicianTitle);

                return RedirectToAction("ManagePhysicians","Admin");
            }
            else
            {
                return View();
            }
        }

        // Let anonamous user to enter user profile
        public ActionResult AddNewUser()
        {
            return View();
        }

        // Let anonamous user to save user profile
        [HttpPost]
        public ActionResult AddNewUser(User user)
        {
            user.CreatedDateTime = DateTime.Now;
            user.LoweredUsername = user.Username.ToLower();

            ApexAsiaDAL.DAL _db = new ApexAsiaDAL.DAL();
            int newId = _db.InsertNewUserReturnId(user);
            if ( newId> 0)
            {
               return RedirectToAction("ManageUserRoles", "Admin", new { id = newId });
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin,Physician")]
        public ActionResult ManagePhysicians()
        {
            return View(this._db.GetAllPhysicianViews());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult UpdateManagePhysicians(int id)
        {
            PhysicianTitle physicianTitle = this._db.GetPhysicianById(id);
            if (physicianTitle != null)
            {
                return View(physicianTitle);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdateManagePhysicians(PhysicianTitle physicianTitle)
        {

            if (physicianTitle != null)
            {
                this._db.UpdatePhysicianTitle(physicianTitle);
                return RedirectToAction("ManagePhysicians");
            }
            else
            {
                return View();
            }
            
        }


        [Authorize(Roles = "Admin,Physician,FrontDesk")]
        public ActionResult PhysicianSchedules(int id=0)
        {
            ViewBag.selectedPhysicianId = id;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManageUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManageUserRoles(int id)
        {
            ViewBag.currentUserId = id;
            return View();
        }

    }
}
