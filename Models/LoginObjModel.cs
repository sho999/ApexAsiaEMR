using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ApexAsiaEMR.Models
{
    public class LoginObjModel
    {
        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}