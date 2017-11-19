using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Duration
    {
        public Int32 DurationID { get; set; }
        public Int32 Mileage { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime FlightTime { get; set; }

        public virtual City City1 { get; set; }
        public virtual City City2 { get; set; }

    }
}