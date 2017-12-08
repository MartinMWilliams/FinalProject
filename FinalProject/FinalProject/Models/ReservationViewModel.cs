using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class ReservationViewModel
    {
        public Int32 ReservationNumber { get; set; }

        public Int32 FlightID { get; set; }
        public bool RoundTrip { get; set; }

        public bool AnotherFlight { get; set; }

        public String DepartureCity { get; set; }

        public String ArrivalCity { get; set; }

        public Int32 NumberOfFliers { get; set; }

        public DateTime DepartureDay { get; set; }

        public Flight SelectedFlight { get; set; }

        public List<AppUser> Fliers { get; set; }
    }

    
    
}