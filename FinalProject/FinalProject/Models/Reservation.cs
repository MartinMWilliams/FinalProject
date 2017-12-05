using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Reservation
    {
        //auto increment starting from 10000
        public Int32 ReservationID { get; set; }

        //round trip or one way boolean
        //multiple city option to add another flight (boolean)
        //number of passengers

        //Total fare
        public Decimal TotalFare { get; set; }


        //navigation property for list of users
        public virtual List<AppUser> Users { get; set; }

        //navigation property for ReservationFlightDetail
        public virtual List<ReservationFlightDetail> ReservationFlightDetails { get; set; }
    }
}