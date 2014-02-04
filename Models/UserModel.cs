using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApexAsiaEMR.Models
{
    public class UserModel
     
     
    {
        public string Username { get; set; }
        public string LoweredUsername { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }

    }
}