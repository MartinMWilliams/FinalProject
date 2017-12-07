using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Reservation
    {
        [Display(Name = "Reservation ID")]
        public Int32 ReservationID { get; set; }
        //auto increment starting from 10000
        [Display(Name = "Reservation Number")]
        public Int32 ReservationNumber { get; set; }

        //round trip or one way boolean
        //multiple city option to add another flight (boolean)
        //number of passengers

        //Total fare
        [Display(Name = "Total Fare")]
        public Decimal TotalFare { get; set; }


        //navigation property for list of users
        public virtual List<AppUser> Users { get; set; }

        //navigation property for ReservationFlightDetail
        public virtual List<ReservationFlightDetail> ReservationFlightDetails { get; set; }
    }
}