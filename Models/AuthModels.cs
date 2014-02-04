using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
namespace ApexAsiaEMR.Models
{
    public class AuthModels
    {
        public class LoginModel
        {
            [Required]
            [Display(Name = "User name")]
            public string Username { get; set; }

            [Required]
            [Display(Name = "Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public class UserModel
        {
            [Required]
            [Display(Name = "User Name")]
            public string Username { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Display(Name = "Email Address")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Password Question")]
            public string PasswordQuestion { get; set; }

            [Required]
            [Display(Name = "Password Answer")]
            public string PasswordAnswer { get; set; }

            [Display(Name = "Comment")]
            public string Comment { get; set; }

        }
    }
}