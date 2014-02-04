using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApexAsiaDAL;

namespace ApexAsiaEMR.Controllers
{
    public class RoleApiController : ApiController
    {
        private ApexAsiaDAL.DAL db = new ApexAsiaDAL.DAL();


        

        // GET api/roleapi/5
        public List<GetUserRolesByUserId_Result> Get()
        {
            return db.GetUserRolesByUserId(2);
        }

        public List<GetUserRolesByUserId_Result> Get(int id)
        {
            return db.GetUserRolesByUserId(id);
        }

        
        // POST api/roleapi
        public bool Post(ViewModels.UserRoleViewModel userRoleVM)
        {
            try
            {
                int selectedUserId = userRoleVM.UserId;
                int[] selectedRoles = userRoleVM.RoleIdArray;
                if (selectedRoles.Length > 0 && selectedUserId > 0)
                {
                    db.DeleteUserRoleByUserId(selectedUserId);
                    for (int i = 0; i < selectedRoles.Length; i++)
                    {
                        UserRole newUserRole = new UserRole();
                        newUserRole.UserId = selectedUserId;
                        newUserRole.RoleId = selectedRoles[i];
                        db.InsertUserRole(newUserRole);
                    }

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // PUT api/roleapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/roleapi/5
        public void Delete(int id)
        {
            
        }
    }
}
