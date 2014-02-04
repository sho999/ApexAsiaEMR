using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ApexAsiaEMR.Controllers
{
    public class DispatchController : Controller
    {
        //
        // GET: /Dispatch/

        public ActionResult Index()
        {
             if (Roles.IsUserInRole("Admin") || Roles.IsUserInRole("Manager") || Roles.IsUserInRole("SuperAdmin")){
                 ViewBag.viewMode = "Admin";
             }
             else if (Roles.IsUserInRole("FrontDesk"))
             {
                 ViewBag.viewMode = "FrontDesk";
             }
             else if (Roles.IsUserInRole("Nurse"))
             {
                 ViewBag.viewMode = "Nurse";
             }
             else if (Roles.IsUserInRole("Physician"))
             {
                 ViewBag.viewMode = "Physician";
             }
            

            return View();
        }

    }
}
