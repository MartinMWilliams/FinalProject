using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace FinalProject.Models
{

    public class FlightCreateViewModel
    {
        
        [Display(Name = "Flight Number")]
        public Int32 FlightNumber { get; set; }

        [Display(Name = "Departure City")]
        public Int32 DepartureCityID { get; set; }

        [Display(Name = "Departure City")]
        public String DepartureCityName { get; set; }

        [Display(Name = "Arrival City")]
        public Int32 ArrivalCityID { get; set; }

        [Display(Name = "Departure City")]
        public String ArrivalCityName { get; set; }

        [Display(Name = "Base Fare")]
        public Decimal BaseFare { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Departure Time")]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Days")]
        public List<String> SelectedDays { get; set; }

    }

    public class BruteForce1
    {
        [Display(Name = "Departure City")]
        public Int32 DepartureCityID { get; set; }

        [Display(Name = "Departure City")]
        public String DepartureCityName { get; set; }
    }

    public class BruteForce2
    {
        [Display(Name = "Departure City")]
        public Int32 ArrivalCityID { get; set; }

        [Display(Name = "Departure City")]
        public String ArrivalCityName { get; set; }
    }
}