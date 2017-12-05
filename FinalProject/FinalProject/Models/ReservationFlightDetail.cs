using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    //enum for Seats
    public enum Seats
    {
        [Display(Name = "1A")]
        OneA,
        [Display(Name = "1B")]
        OneB,
        [Display(Name = "2A")]
        TwoA,
        [Display(Name = "2B")]
        TwoB,
        [Display(Name = "3A")]
        ThreeA,
        [Display(Name = "3B")]
        ThreeB,
        [Display(Name = "3C")]
        ThreeC,
        [Display(Name = "3D")]
        ThreeD,
        [Display(Name = "4A")]
        FourA,
        [Display(Name = "4B")]
        FourB, 
        [Display(Name = "4C")]
        FourC,
        [Display(Name = "4D")]
        FourD
    }

    public class ReservationFlightDetail
    {
        public Int32 ReservationFlightDetailID { get; set; }

        public Seats SeatAssignment { get; set; }

        //Fare
        public Decimal Fare { get; set; }

        //  NAVIGATIONAL PROPERTIES:
        //List of reservations
        public virtual Reservation Reservation { get; set; }

        //List of flights
        public virtual Flight Flight { get; set; }

        public virtual AppUser User { get; set; }

    }
}