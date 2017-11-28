using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public enum Days
    {
        Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
    }
    public class Flight
    {
        [Display(Name = "Flight ID")]
        public Int32 FlightID { get; set; }

        [Display(Name = "Flight Number")]
        public Int32 FlightNumber { get; set; }

        [Display(Name = "Departure City")]
        public String DepartureCity { get; set; }

        [Display(Name = "Arrival City")]
        public String ArrivalCity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Base Fare")]
        public Decimal BaseFare { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Departure Time")]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Duration Info")]
        public virtual Duration DurationInfo { get; set; }
    }
}