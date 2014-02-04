using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApexAsiaEMR.Models
{
    public class PhysicianScheduleModels
    {
        

    }

    public class PhysicianScheduleObject
    {
        public int PhysicianTitleId { get; set; }
        public int DayOfWeekId { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string Note { get; set; }
    }

    public class PhysicianScheculesArrayObject
    {
        public int PhysicianTitleId { get; set; }
        public PhysicianScheduleObject[] PhysicianScheculesArray { get; set; }
    }
}