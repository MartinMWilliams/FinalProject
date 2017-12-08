using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class FlightCrew
    {
        public Int32 FlightCrewID { get; set; }
        public String Pilot { get; set; }

        [Display(Name = "Co-Pilot")]
        public String CoPilot { get; set; }

        [Display(Name = "Cabin Crew")]
        public String CabinCrew { get; set; }

        public virtual Flight Flight { get; set; }
    }
}