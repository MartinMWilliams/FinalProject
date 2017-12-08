using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Duration
    {
        [Display(Name = "Duration ID")]
        public Int32 DurationID { get; set; }
        public Int32 Mileage { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Flight Time")]
        public TimeSpan FlightTime { get; set; }

        [Display(Name = "City 1")]
        public virtual City City1 { get; set; }

        [Display(Name = "City 2")]
        public virtual City City2 { get; set; }

    }
}