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
    public class FlightsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Flights
        public ActionResult Index()
        {
            return View(db.Flights.ToList());
        }

        //Detailed Search method
        public ActionResult DetailedSearch()
        {
            ViewBag.AllCities = GetAllCities();
            return View();
        }

        //Search results method
        public ActionResult SearchResults(int SelectedDepartureCity, int SelectedArrivalCity)
        {
            var query = from f in db.Flights
                        select f;

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
                query = query.Where(f => f.DepartureCity == CityToDisplay.CityName);
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
                query = query.Where(f => f.ArrivalCity == CityToDisplay.CityName);
            }

            //Set up selected flights list based on query results
            List<Flight> SelectedFlights = query.ToList();

            //send to view
            return View("Index", SelectedFlights.OrderBy(f => f.DepartureCity));
        }



        





        // GET: Flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flights/Create
        public ActionResult Create()
        {
            FlightCreateViewModel flight = new FlightCreateViewModel();
            ViewBag.AllDepartureCities = GetDepartureCities();
            ViewBag.AllArrivalCities = GetArrivalCities();
            ViewBag.AllDays = GetAllDays();
            

            int BiggestFlight = 100;
            
            if (db.Cities.ToList().Any() == false)
            {
                BiggestFlight = 100;
                //ViewBag.FlightNumber = BiggestFlight;
                flight.FlightNumber = BiggestFlight;
            }
            else
            {
                foreach (Flight f in db.Flights.ToList())
                {
                    if (f.FlightNumber >= BiggestFlight)
                    {
                        BiggestFlight = f.FlightNumber + 1;
                        //ViewBag.FlightNumber = BiggestFlight + 1;
                        flight.FlightNumber = BiggestFlight;
                    }
                }
            }
            if (ViewBag.FlightNumber == null)
            {
                //ViewBag.FlightNumber = 100;
                flight.FlightNumber = BiggestFlight;
            }

            return View(flight);
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlightCreateViewModel flight) //int DepartureCityID, int ArrivalCityID
        {
            flight.DepartureCityName = db.Cities.FirstOrDefault(c=>c.CityID == flight.DepartureCityID).CityName;

            flight.ArrivalCityName = db.Cities.FirstOrDefault(c => c.CityID == flight.ArrivalCityID).CityName;
            if (ModelState.IsValid)
            {
                foreach (var i in flight.SelectedDays)
                {
                    if (i == "Monday")
                    {
                        Flight Dec4Flight = new Flight();
                        Dec4Flight.FlightNumber = flight.FlightNumber;
                        Dec4Flight.ArrivalCity = flight.DepartureCityName;
                        Dec4Flight.DepartureCity = flight.ArrivalCityName;
                        Dec4Flight.BaseFare = flight.BaseFare;
                        Dec4Flight.DepartureTime = flight.DepartureTime;
                        //Dec4Flight.ArrivalTime = Dec4Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec4Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec4Flight.Date = new DateTime(2017, 12, 4);

                        Flight Dec11Flight = new Flight();
                        Dec11Flight.FlightNumber = flight.FlightNumber;
                        Dec11Flight.ArrivalCity = flight.DepartureCityName;
                        Dec11Flight.DepartureCity = flight.ArrivalCityName;
                        Dec11Flight.BaseFare = flight.BaseFare;
                        Dec11Flight.DepartureTime = flight.DepartureTime;
                        //Dec11Flight.ArrivalTime = Dec11Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec11Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec11Flight.Date = new DateTime(2017, 12, 11);

                        Flight Dec18Flight = new Flight();
                        Dec18Flight.FlightNumber = flight.FlightNumber;
                        Dec18Flight.ArrivalCity = flight.DepartureCityName;
                        Dec18Flight.DepartureCity = flight.ArrivalCityName;
                        Dec18Flight.BaseFare = flight.BaseFare;
                        Dec18Flight.DepartureTime = flight.DepartureTime;
                        //Dec18Flight.ArrivalTime = Dec18Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec18Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec18Flight.Date = new DateTime(2017, 12, 18);

                        Flight Dec25Flight = new Flight();
                        Dec25Flight.FlightNumber = flight.FlightNumber;
                        Dec25Flight.ArrivalCity = flight.DepartureCityName;
                        Dec25Flight.DepartureCity = flight.ArrivalCityName;
                        Dec25Flight.BaseFare = flight.BaseFare;
                        Dec25Flight.DepartureTime = flight.DepartureTime;
                        //Dec25Flight.ArrivalTime = Dec25Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec25Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec25Flight.Date = new DateTime(2017, 12, 25);

                        db.Flights.Add(Dec4Flight);
                        db.Flights.Add(Dec11Flight);
                        db.Flights.Add(Dec18Flight);
                        db.Flights.Add(Dec25Flight);
                    }
                    if (i == "Tuesday")
                    {
                        Flight Dec5Flight = new Flight();
                        Dec5Flight.FlightNumber = flight.FlightNumber;
                        Dec5Flight.ArrivalCity = flight.DepartureCityName;
                        Dec5Flight.DepartureCity = flight.ArrivalCityName;
                        Dec5Flight.BaseFare = flight.BaseFare;
                        Dec5Flight.DepartureTime = flight.DepartureTime;
                        //Dec5Flight.ArrivalTime = Dec5Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec5Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec5Flight.Date = new DateTime(2017, 12, 5);

                        Flight Dec12Flight = new Flight();
                        Dec12Flight.FlightNumber = flight.FlightNumber;
                        Dec12Flight.ArrivalCity = flight.DepartureCityName;
                        Dec12Flight.DepartureCity = flight.ArrivalCityName;
                        Dec12Flight.BaseFare = flight.BaseFare;
                        Dec12Flight.DepartureTime = flight.DepartureTime;
                        //Dec12Flight.ArrivalTime = Dec12Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec12Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec12Flight.Date = new DateTime(2017, 12, 12);

                        Flight Dec19Flight = new Flight();
                        Dec19Flight.FlightNumber = flight.FlightNumber;
                        Dec19Flight.ArrivalCity = flight.DepartureCityName;
                        Dec19Flight.DepartureCity = flight.ArrivalCityName;
                        Dec19Flight.BaseFare = flight.BaseFare;
                        Dec19Flight.DepartureTime = flight.DepartureTime;
                        //Dec19Flight.ArrivalTime = Dec19Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec19Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec19Flight.Date = new DateTime(2017, 12, 19);

                        Flight Dec26Flight = new Flight();
                        Dec26Flight.FlightNumber = flight.FlightNumber;
                        Dec26Flight.ArrivalCity = flight.DepartureCityName;
                        Dec26Flight.DepartureCity = flight.ArrivalCityName;
                        Dec26Flight.BaseFare = flight.BaseFare;
                        Dec26Flight.DepartureTime = flight.DepartureTime;
                        //Dec26Flight.ArrivalTime = Dec26Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec26Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec26Flight.Date = new DateTime(2017, 12, 26);

                        db.Flights.Add(Dec5Flight);
                        db.Flights.Add(Dec12Flight);
                        db.Flights.Add(Dec19Flight);
                        db.Flights.Add(Dec26Flight);
                    }
                    if (i == "Wednesday")
                    {
                        Flight Dec6Flight = new Flight();
                        Dec6Flight.FlightNumber = flight.FlightNumber;
                        Dec6Flight.ArrivalCity = flight.DepartureCityName;
                        Dec6Flight.DepartureCity = flight.ArrivalCityName;
                        Dec6Flight.BaseFare = flight.BaseFare;
                        Dec6Flight.DepartureTime = flight.DepartureTime;
                        //Dec6Flight.ArrivalTime = Dec6Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec6Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec6Flight.Date = new DateTime(2017, 12, 6);

                        Flight Dec13Flight = new Flight();
                        Dec13Flight.FlightNumber = flight.FlightNumber;
                        Dec13Flight.ArrivalCity = flight.DepartureCityName;
                        Dec13Flight.DepartureCity = flight.ArrivalCityName;
                        Dec13Flight.BaseFare = flight.BaseFare;
                        Dec13Flight.DepartureTime = flight.DepartureTime;
                        //Dec13Flight.ArrivalTime = Dec13Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec13Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec13Flight.Date = new DateTime(2017, 12, 13);

                        Flight Dec20Flight = new Flight();
                        Dec20Flight.FlightNumber = flight.FlightNumber;
                        Dec20Flight.ArrivalCity = flight.DepartureCityName;
                        Dec20Flight.DepartureCity = flight.ArrivalCityName;
                        Dec20Flight.BaseFare = flight.BaseFare;
                        Dec20Flight.DepartureTime = flight.DepartureTime;
                        //Dec20Flight.ArrivalTime = Dec20Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec20Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec20Flight.Date = new DateTime(2017, 12, 20);

                        Flight Dec27Flight = new Flight();
                        Dec27Flight.FlightNumber = flight.FlightNumber;
                        Dec27Flight.ArrivalCity = flight.DepartureCityName;
                        Dec27Flight.DepartureCity = flight.ArrivalCityName;
                        Dec27Flight.BaseFare = flight.BaseFare;
                        Dec27Flight.DepartureTime = flight.DepartureTime;
                        //Dec27Flight.ArrivalTime = Dec27Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec27Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec27Flight.Date = new DateTime(2017, 12, 27);

                        db.Flights.Add(Dec6Flight);
                        db.Flights.Add(Dec13Flight);
                        db.Flights.Add(Dec20Flight);
                        db.Flights.Add(Dec27Flight);
                    }
                    if (i == "Thursday")
                    {
                        Flight Dec7Flight = new Flight();
                        Dec7Flight.FlightNumber = flight.FlightNumber;
                        Dec7Flight.ArrivalCity = flight.DepartureCityName;
                        Dec7Flight.DepartureCity = flight.ArrivalCityName;
                        Dec7Flight.BaseFare = flight.BaseFare;
                        Dec7Flight.DepartureTime = flight.DepartureTime;
                        //Dec7Flight.ArrivalTime = Dec7Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec7Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec7Flight.Date = new DateTime(2017, 12, 7);

                        Flight Dec14Flight = new Flight();
                        Dec14Flight.FlightNumber = flight.FlightNumber;
                        Dec14Flight.ArrivalCity = flight.DepartureCityName;
                        Dec14Flight.DepartureCity = flight.ArrivalCityName;
                        Dec14Flight.BaseFare = flight.BaseFare;
                        Dec14Flight.DepartureTime = flight.DepartureTime;
                        //Dec14Flight.ArrivalTime = Dec14Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec14Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec14Flight.Date = new DateTime(2017, 12, 14);

                        Flight Dec21Flight = new Flight();
                        Dec21Flight.FlightNumber = flight.FlightNumber;
                        Dec21Flight.ArrivalCity = flight.DepartureCityName;
                        Dec21Flight.DepartureCity = flight.ArrivalCityName;
                        Dec21Flight.BaseFare = flight.BaseFare;
                        Dec21Flight.DepartureTime = flight.DepartureTime;
                        //Dec21Flight.ArrivalTime = Dec21Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec21Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec21Flight.Date = new DateTime(2017, 12, 21);

                        Flight Dec28Flight = new Flight();
                        Dec28Flight.FlightNumber = flight.FlightNumber;
                        Dec28Flight.ArrivalCity = flight.DepartureCityName;
                        Dec28Flight.DepartureCity = flight.ArrivalCityName;
                        Dec28Flight.BaseFare = flight.BaseFare;
                        Dec28Flight.DepartureTime = flight.DepartureTime;
                        //Dec28Flight.ArrivalTime = Dec28Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec28Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec28Flight.Date = new DateTime(2017, 12, 28);

                        db.Flights.Add(Dec7Flight);
                        db.Flights.Add(Dec14Flight);
                        db.Flights.Add(Dec21Flight);
                        db.Flights.Add(Dec28Flight);
                    }
                    if (i == "Friday")
                    {
                        Flight Dec1Flight = new Flight();
                        Dec1Flight.FlightNumber = flight.FlightNumber;
                        Dec1Flight.ArrivalCity = flight.DepartureCityName;
                        Dec1Flight.DepartureCity = flight.ArrivalCityName;
                        Dec1Flight.BaseFare = flight.BaseFare;
                        Dec1Flight.DepartureTime = flight.DepartureTime;
                        //Dec1Flight.ArrivalTime = Dec1Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec1Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec1Flight.Date = new DateTime(2017, 12, 1);

                        Flight Dec8Flight = new Flight();
                        Dec8Flight.FlightNumber = flight.FlightNumber;
                        Dec8Flight.ArrivalCity = flight.DepartureCityName;
                        Dec8Flight.DepartureCity = flight.ArrivalCityName;
                        Dec8Flight.BaseFare = flight.BaseFare;
                        Dec8Flight.DepartureTime = flight.DepartureTime;
                        //Dec8Flight.ArrivalTime = Dec8Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec8Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec8Flight.Date = new DateTime(2017, 12, 8);

                        Flight Dec15Flight = new Flight();
                        Dec15Flight.FlightNumber = flight.FlightNumber;
                        Dec15Flight.ArrivalCity = flight.DepartureCityName;
                        Dec15Flight.DepartureCity = flight.ArrivalCityName;
                        Dec15Flight.BaseFare = flight.BaseFare;
                        Dec15Flight.DepartureTime = flight.DepartureTime;
                        //Dec15Flight.ArrivalTime = Dec15Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec15Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec15Flight.Date = new DateTime(2017, 12, 15);

                        Flight Dec22Flight = new Flight();
                        Dec22Flight.FlightNumber = flight.FlightNumber;
                        Dec22Flight.ArrivalCity = flight.DepartureCityName;
                        Dec22Flight.DepartureCity = flight.ArrivalCityName;
                        Dec22Flight.BaseFare = flight.BaseFare;
                        Dec22Flight.DepartureTime = flight.DepartureTime;
                        //Dec22Flight.ArrivalTime = Dec22Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec22Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec22Flight.Date = new DateTime(2017, 12, 22);

                        Flight Dec29Flight = new Flight();
                        Dec29Flight.FlightNumber = flight.FlightNumber;
                        Dec29Flight.ArrivalCity = flight.DepartureCityName;
                        Dec29Flight.DepartureCity = flight.ArrivalCityName;
                        Dec29Flight.BaseFare = flight.BaseFare;
                        Dec29Flight.DepartureTime = flight.DepartureTime;
                        //Dec29Flight.ArrivalTime = Dec29Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec29Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec29Flight.Date = new DateTime(2017, 12, 29);

                        db.Flights.Add(Dec1Flight);
                        db.Flights.Add(Dec8Flight);
                        db.Flights.Add(Dec15Flight);
                        db.Flights.Add(Dec22Flight);
                        db.Flights.Add(Dec29Flight);
                    }
                    if (i == "Saturday")
                    {
                        Flight Dec2Flight = new Flight();
                        Dec2Flight.FlightNumber = flight.FlightNumber;
                        Dec2Flight.ArrivalCity = flight.DepartureCityName;
                        Dec2Flight.DepartureCity = flight.ArrivalCityName;
                        Dec2Flight.BaseFare = flight.BaseFare;
                        Dec2Flight.DepartureTime = flight.DepartureTime;
                        //Dec2Flight.ArrivalTime = Dec2Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec2Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec2Flight.Date = new DateTime(2017, 12, 2);

                        Flight Dec9Flight = new Flight();
                        Dec9Flight.FlightNumber = flight.FlightNumber;
                        Dec9Flight.ArrivalCity = flight.DepartureCityName;
                        Dec9Flight.DepartureCity = flight.ArrivalCityName;
                        Dec9Flight.BaseFare = flight.BaseFare;
                        Dec9Flight.DepartureTime = flight.DepartureTime;
                        //Dec9Flight.ArrivalTime = Dec9Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec9Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec9Flight.Date = new DateTime(2017, 12, 9);

                        Flight Dec16Flight = new Flight();
                        Dec16Flight.FlightNumber = flight.FlightNumber;
                        Dec16Flight.ArrivalCity = flight.DepartureCityName;
                        Dec16Flight.DepartureCity = flight.ArrivalCityName;
                        Dec16Flight.BaseFare = flight.BaseFare;
                        Dec16Flight.DepartureTime = flight.DepartureTime;
                        //Dec16Flight.ArrivalTime = Dec16Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec16Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec16Flight.Date = new DateTime(2017, 12, 16);

                        Flight Dec23Flight = new Flight();
                        Dec23Flight.FlightNumber = flight.FlightNumber;
                        Dec23Flight.ArrivalCity = flight.DepartureCityName;
                        Dec23Flight.DepartureCity = flight.ArrivalCityName;
                        Dec23Flight.BaseFare = flight.BaseFare;
                        Dec23Flight.DepartureTime = flight.DepartureTime;
                        //Dec23Flight.ArrivalTime = Dec23Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec23Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec23Flight.Date = new DateTime(2017, 12, 23);

                        Flight Dec30Flight = new Flight();
                        Dec30Flight.FlightNumber = flight.FlightNumber;
                        Dec30Flight.ArrivalCity = flight.DepartureCityName;
                        Dec30Flight.DepartureCity = flight.ArrivalCityName;
                        Dec30Flight.BaseFare = flight.BaseFare;
                        Dec30Flight.DepartureTime = flight.DepartureTime;
                        //Dec30Flight.ArrivalTime = Dec30Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec30Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec30Flight.Date = new DateTime(2017, 12, 30);

                        db.Flights.Add(Dec2Flight);
                        db.Flights.Add(Dec9Flight);
                        db.Flights.Add(Dec16Flight);
                        db.Flights.Add(Dec23Flight);
                        db.Flights.Add(Dec30Flight);
                    }
                    if (i == "Sunday")
                    {
                        Flight Dec3Flight = new Flight();
                        Dec3Flight.FlightNumber = flight.FlightNumber;
                        Dec3Flight.ArrivalCity = flight.DepartureCityName;
                        Dec3Flight.DepartureCity = flight.ArrivalCityName;
                        Dec3Flight.BaseFare = flight.BaseFare;
                        Dec3Flight.DepartureTime = flight.DepartureTime;
                        //Dec3Flight.ArrivalTime = Dec3Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec3Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec3Flight.Date = new DateTime(2017, 12, 3);

                        Flight Dec10Flight = new Flight();
                        Dec10Flight.FlightNumber = flight.FlightNumber;
                        Dec10Flight.ArrivalCity = flight.DepartureCityName;
                        Dec10Flight.DepartureCity = flight.ArrivalCityName;
                        Dec10Flight.BaseFare = flight.BaseFare;
                        Dec10Flight.DepartureTime = flight.DepartureTime;
                        //Dec10Flight.ArrivalTime = Dec10Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec10Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec10Flight.Date = new DateTime(2017, 12, 10);

                        Flight Dec17Flight = new Flight();
                        Dec17Flight.FlightNumber = flight.FlightNumber;
                        Dec17Flight.ArrivalCity = flight.DepartureCityName;
                        Dec17Flight.DepartureCity = flight.ArrivalCityName;
                        Dec17Flight.BaseFare = flight.BaseFare;
                        Dec17Flight.DepartureTime = flight.DepartureTime;
                        //Dec17Flight.ArrivalTime = Dec17Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec17Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec17Flight.Date = new DateTime(2017, 12, 17);

                        Flight Dec24Flight = new Flight();
                        Dec24Flight.FlightNumber = flight.FlightNumber;
                        Dec24Flight.ArrivalCity = flight.DepartureCityName;
                        Dec24Flight.DepartureCity = flight.ArrivalCityName;
                        Dec24Flight.BaseFare = flight.BaseFare;
                        Dec24Flight.DepartureTime = flight.DepartureTime;
                        //Dec24Flight.ArrivalTime = Dec24Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec24Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec24Flight.Date = new DateTime(2017, 12, 24);

                        Flight Dec31Flight = new Flight();
                        Dec31Flight.FlightNumber = flight.FlightNumber;
                        Dec31Flight.ArrivalCity = flight.DepartureCityName;
                        Dec31Flight.DepartureCity = flight.ArrivalCityName;
                        Dec31Flight.BaseFare = flight.BaseFare;
                        Dec31Flight.DepartureTime = flight.DepartureTime;
                        //Dec31Flight.ArrivalTime = Dec31Flight.DepartureTime.Add(GetFlightTime(flight.DepartureCityID, flight.ArrivalCityID));
                        Dec31Flight.ArrivalTime = flight.DepartureTime.AddHours(10);
                        Dec31Flight.Date = new DateTime(2017, 12, 31);

                        db.Flights.Add(Dec3Flight);
                        db.Flights.Add(Dec10Flight);
                        db.Flights.Add(Dec17Flight);
                        db.Flights.Add(Dec24Flight);
                        db.Flights.Add(Dec31Flight);
                    }


                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Flights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlightID,FlightNumber,DepartureCity,ArrivalCity,Day,BaseFare,DepartureTime,ArrivalTime")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: Flights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public MultiSelectList GetAllDays()
        {

            Array values = Enum.GetValues(typeof(Days));
            List<ListItem> allDays = new List<ListItem>(values.Length);

            foreach (var i in values)
            {
                allDays.Add(new ListItem
                {
                    Text = Enum.GetName(typeof(Days), i),
                    Value = ((int)i).ToString()
                });
            }
            MultiSelectList mulitselectDays = new MultiSelectList(allDays);
            return mulitselectDays;
        }

        //public SelectList GetAllArrCities()
        //{
        //    var query = from c in db.Cities
        //                orderby c.CityName
        //                select c;

        //    List<City> allCities = query.ToList();

        //    SelectList allCitieslist = new SelectList(allCities, "ArrivalCityID", "CityName");

        //    return allCitieslist;
        //}

        //public SelectList GetAllDepCities()
        //{
        //    var query = from c in db.Cities
        //                orderby c.CityName
        //                select c;

        //    List<City> allCities = query.ToList();

        //    SelectList allCitieslist = new SelectList(allCities, "DepartureCityID", "CityName");

        //    return allCitieslist;
        //}

        public SelectList GetAllCities()
        {
            var query = from c in db.Cities
                        orderby c.CityName
                        select c;

            List<City> allCities = query.ToList();

            SelectList allCitieslist = new SelectList(allCities, "CityID", "CityName");

            return allCitieslist;
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

        public TimeSpan GetFlightTime(int DepartureCityID, int ArrivalCityID)
        {
            TimeSpan FlightTime = new TimeSpan();


            FlightTime = db.Durations.FirstOrDefault(a => a.City1.CityID == DepartureCityID && a.City2.CityID == ArrivalCityID).FlightTime;

            return FlightTime;

        }
    }
}
