using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApexAsiaEMR.Models;
using System.Web.Security;
using WebMatrix.WebData;
using ApexAsiaEMR.Filters;


namespace ApexAsiaEMR.Controllers
{
    
    public class AuthController : Controller
    {
      

        private ApexAsiaDAL.DAL _aamcDal = new ApexAsiaDAL.DAL();
        //
        // GET: /Auth/
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
      
        public ActionResult Login(LoginObjModel loginModel)
        {
            bool result = this._aamcDal.IsValidUser(loginModel.Username, loginModel.Password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(loginModel.Username, false);
                return RedirectToLocal("~/Dispatch/Index");
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(loginModel);
        }

     
        [HttpPost]
        public ActionResult Logout()
        {
            //WebSecurity.Logout();
            FormsAuthentication.SignOut();
            Roles.DeleteCookie();
            Session.Clear();
            return RedirectToAction("Login", "Auth"); //("~/Auth/Login");
        }


        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

    }

}
