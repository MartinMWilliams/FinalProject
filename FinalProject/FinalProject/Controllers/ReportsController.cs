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
            ViewBag.DepartureCities = GetDepartureCities();
            ViewBag.ArrivalCities = GetArrivalCities();
            ViewBag.AllCities = GetAllCities();

            return View();
        }

        //detailed search method
        public ActionResult DetailedSearch()
        {
            ViewBag.AllCities = GetAllCities();
            return View();
        }

        //search results
        public ActionResult SearchResults(int DepartureCityID, int ArrivalCityID, DateTime? SelectedStartDate, DateTime? SelectedEndDate, int CityID)
        {
            var query = from r in db.ReservationFlightDetails
                        select r;

            //Drop down list for Departure City
            if (DepartureCityID == 0) //they chose all departure cities
            {
                ViewBag.SelectedDepartureCity = "No departure city was selected";
            }
            else //city was chosen
            {
                //Set the AllCities list from the GetCities method that is in the City model?
                List<City> AllCities = db.Cities.ToList();
                City CityToDisplay = AllCities.Find(c => c.CityID == DepartureCityID);
                ViewBag.SelectedDepartureCity = "The selected departure city is " + CityToDisplay.CityName;

                //Query the results based on the selected departure city
                query = query.Where(r => r.Flight.DepartureCity == CityToDisplay.CityName);
            }

            //Drope down list for Arrival Citty
            if (ArrivalCityID == 0) //they chose all arrival cities
            {
                ViewBag.SelectedArrivalCity = "No arrival city was selected";
            }
            else //city was chosen
            {
                //Set the AllCities list from the GetCities method that is in the City model?
                List<City> AllCities = db.Cities.ToList();
                City CityToDisplay = AllCities.Find(c => c.CityID == ArrivalCityID);
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

        public SelectList GetDepartureCities()
        {
            List<BruteForce1> fuckthis = new List<BruteForce1>();

            var query = from c in db.Cities
                        orderby c.CityName
                        select c;

            foreach (City item in query)
            {
                BruteForce1 newcity = new BruteForce1();
                newcity.DepartureCityID = item.CityID;
                newcity.DepartureCityName = item.CityName;
                fuckthis.Add(newcity);
            }

            SelectList DepartureCitiesList = new SelectList(fuckthis, "DepartureCityID", "DepartureCityName");

            return DepartureCitiesList;
        }

        public SelectList GetArrivalCities()
        {
            List<BruteForce2> fuckthis = new List<BruteForce2>();

            var query = from c in db.Cities
                        orderby c.CityName
                        select c;

            foreach (City item in query)
            {
                BruteForce2 newcity = new BruteForce2();
                newcity.ArrivalCityID = item.CityID;
                newcity.ArrivalCityName = item.CityName;
                fuckthis.Add(newcity);
            }

            SelectList ArrivalCitiesList = new SelectList(fuckthis, "ArrivalCityID", "ArrivalCityName");

            return ArrivalCitiesList;
        }


    }
}