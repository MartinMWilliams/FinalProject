using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class ReportsViewModel
    {
        //seats
        public Int32 TotalSeatsSold { get; set; }

        //revenue
        public Decimal TotalRevenue { get; set; }

        //date range
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //cities
        public String CityID { get; set; }
        public String CityName { get; set; }

        //routes
        public Int32 DepartureCityID { get; set; }
        public String DepartureCityName { get; set; }
        public Int32 ArrivalCityID { get; set; }
        public String ArrivalCityName { get; set; }

        //first class v economy
        public String SeatType { get; set; }
    }
}