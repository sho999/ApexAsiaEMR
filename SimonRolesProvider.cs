using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ApexAsiaEMR
{
    /// <summary>
    /// Reference: Custom RoleProvider by following the link below.
    /// http://www.brianlegg.com/post/2011/05/09/Implementing-your-own-RoleProvider-and-MembershipProvider-in-MVC-3.aspx
    /// </summary>
    public class SimonRolesProvider : RoleProvider
    {
        private ApexAsiaDAL.DAL repoDb;
        /// <summary>
        /// Default Constructor create an instance of ApexAsiaDAL.DAL()
        /// </summary>
        public SimonRolesProvider()
        {
            this.repoDb = new ApexAsiaDAL.DAL();
        }

        public override string[] GetRolesForUser(string username)
        {
            return this.repoDb.GetRolesForUser(username);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            string[] userRoles = GetRolesForUser(username);
            if (userRoles.Contains(roleName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

       

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
            
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
           
        }

        

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}