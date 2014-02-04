using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApexAsiaDAL;
using System.Web;

namespace ApexAsiaEMR.Controllers
{
    public class UserApiController : ApiController
    {
        private ApexAsiaDAL.DAL db = new ApexAsiaDAL.DAL();
        private HttpRequest _request = HttpContext.Current.Request;

        // GET api/userapi
        public List<User> Get()
        {
            return db.GetAllUsers();
        }

        // GET api/userapi/5
        public User Get(int id)
        {
            return db.GetUserById(id);
        }

        // POST api/userapi
        public bool Post(int id)
        {
            //simon NOTE: PopUp window does not close on Update
            // http://www.kendoui.com/forums/mvc/grid/popup-window-does-not-close-on-update.aspx
            //var responseObj = new HttpResponseMessage();
            try
            {
                if (id > 0)
                {
                    User userToBeUpdate = db.GetUserById(id);
                    if (userToBeUpdate != null)
                    {
                        userToBeUpdate.FullName = string.IsNullOrEmpty(_request["FullName"]) ? userToBeUpdate.FullName : _request["FullName"];
                        userToBeUpdate.Username = string.IsNullOrEmpty(_request["Username"]) ? userToBeUpdate.Username : _request["Username"];
                        userToBeUpdate.Password = string.IsNullOrEmpty(_request["Password"]) ? userToBeUpdate.Password : _request["Password"];
                        userToBeUpdate.Email = string.IsNullOrEmpty(_request["Email"]) ? userToBeUpdate.Email : _request["Email"];
                        userToBeUpdate.PasswordQuestion = string.IsNullOrEmpty(_request["PasswordQuestion"]) ? userToBeUpdate.PasswordQuestion : _request["PasswordQuestion"];
                        userToBeUpdate.PasswordAnswer = string.IsNullOrEmpty(_request["PasswordAnswer"]) ? userToBeUpdate.PasswordAnswer : _request["PasswordAnswer"];
                        userToBeUpdate.Comment = string.IsNullOrEmpty(_request["Comment"]) ? userToBeUpdate.Comment : _request["Comment"];

                        db.UpdateUser(userToBeUpdate);
                    }
                }
                //responseObj.StatusCode = HttpStatusCode.OK;
                return true;
            }
            catch(Exception ex)
            {
                return false;
                //responseObj.StatusCode = HttpStatusCode.InternalServerError;
                //responseObj.Content = new StringContent(string.Format("Error while calling UpdateUser() for UserId : {0} . Details = " + ex.Message , id.ToString()));
            }
            //return responseObj;
        }

        // PUT api/userapi/5
        public bool Put()
        {
            try
            {
                User newUser = new User();
                newUser.Username = _request["Username"];
                newUser.LoweredUsername = newUser.Username.ToLower();
                newUser.FullName = _request["FullName"];
                newUser.Password = _request["Password"];
                newUser.PasswordQuestion = _request["PasswordQuestion"];
                newUser.PasswordAnswer = _request["PasswordAnswer"];
                newUser.Email = _request["Email"];
                newUser.Comment = _request["Comment"];
                newUser.CreatedDateTime = DateTime.Now;

                db.InsertNewUser(newUser);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // DELETE api/userapi/5
        public void Delete(int id)
        {
            if (id > 0)
            {
                User userToBeDelete = db.GetUserById(id);
                //List<GetUserRolesByUserId_Result> userRoles = db.GetUserRolesByUserId(id);

                if (userToBeDelete != null)
                {
                    db.DeleteUser(userToBeDelete);
                }
            }
        }
    }
}
