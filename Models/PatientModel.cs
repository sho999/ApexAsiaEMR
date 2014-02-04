using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ApexAsiaEMR.Models
{
    public class PatientModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender{ get; set; }

        public string FatherName{ get; set; }
        public string DateOfBirth{ get; set; }
	    public string NrcNo{ get; set; }
        public string PassportNo{ get; set; }
	    public string MarritalStatus{ get; set; }
        public string HomePhone{ get; set; }
        public string MobilePhone{ get; set; }
        public string Address{ get; set; }
        public string City{ get; set; }
        public string Township{ get; set; }
        public string KinName{ get; set; }
        public string KinRelationship{ get; set; }
        public string KinAddress{ get; set; }
        public string KinHomePhone{ get; set; }
        public string KinMobilePhone{ get; set; }
        public string EnteredBy{ get; set; }
        public string EnteredDateTime{ get; set; }
    }
}