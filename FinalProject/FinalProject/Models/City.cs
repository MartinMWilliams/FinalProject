using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public enum States
    {
        AL,
        AK,
        AZ,
        AR,
        CA,
        CO,
        CT,
        DE,
        FL,
        GA,
        HI,
        ID,
        IL,
        IN,
        IA,
        KS,
        KY,
        LA,
        ME,
        MD,
        MA,
        MI,
        MN,
        MS,
        MO,
        MT,
        NE,
        NV,
        NH,
        NJ,
        NM,
        NY,
        NC,
        ND,
        OH,
        OK,
        OR,
        PA,
        RI,
        SC,
        SD,
        TN,
        TX,
        UT,
        VT,
        VA,
        WA,
        WV,
        WI,
        WY
    }

    public class City
    {

        [Display(Name = "City ID")]
        public Int32 CityID { get; set; }

        [Display(Name = "City Number")]
        public Int32 CityNumber { get; set; }
        
        [Display(Name = "City Name")]
        public String CityName { get; set; }

        [Display(Name = "Airport Code")]
        [StringLength(3, ErrorMessage = "Airport Code must be 3 characters")]
        public String AirportCode { get; set; }
        
        public States State { get; set; }

        public virtual List<Duration> Durations { get; set; }
    }
}