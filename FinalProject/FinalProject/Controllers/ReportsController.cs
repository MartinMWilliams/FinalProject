using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.DAL;
using FinalProject.Models;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class ReportsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Reports
        public ActionResult Index()
        {
            return View(db.ReservationFlightDetails.ToList());
        }

        //detailed search method
        public ActionResult DetailedSearch()
        {
            ViewBag.AllCities = GetAllCities();
            return View();
        }

        //search results
        public ActionResult SearchResults(int SelectedDepartureCity, int SelectedArrivalCity, DateTime? SelectedStartDate, DateTime? SelectedEndDate)
        {
            var query = from r in db.ReservationFlightDetails
                        select r;

            //Drop down list for Departure City
            if (SelectedDepartureCity == 0) //they chose all departure cities
            {
                ViewBag.SelectedDepartureCity = "No departure city was selected";
            }
            else //city was chosen
            {
                //Set the AllCities list from the GetCities method that is in the City model?
                List<City> AllCities = db.Cities.ToList();
                City CityToDisplay = AllCities.Find(c => c.CityID == SelectedDepartureCity);
                ViewBag.SelectedDepartureCity = "The selected departure city is " + CityToDisplay.CityName;

                //Query the results based on the selected departure city
                query = query.Where(r => r.Flight.DepartureCity == CityToDisplay.CityName);
            }

            //Drope down list for Arrival Citty
            if (SelectedArrivalCity == 0) //they chose all arrival cities
            {
                ViewBag.SelectedArrivalCity = "No arrival city was selected";
            }
            else //city was chosen
            {
                //Set the AllCities list from the GetCities method that is in the City model?
                List<City> AllCities = db.Cities.ToList();
                City CityToDisplay = AllCities.Find(c => c.CityID == SelectedArrivalCity);
                ViewBag.SelectedArrivalCity = "The selected arrival city is " + CityToDisplay.CityName;

                //Query the results based on the selected Arrival City
                query = query.Where(r => r.Flight.ArrivalCity == CityToDisplay.CityName);
            }

            return View();
        }

        public SelectList GetAllCities()
        {
            var query = from c in db.Cities
                        orderby c.CityName
                        select c;

            List<City> allCities = query.ToList();

            SelectList allCitiesList = new SelectList(allCities, "CityID", "CityName");
            return allCitiesList;
        }
    }
}