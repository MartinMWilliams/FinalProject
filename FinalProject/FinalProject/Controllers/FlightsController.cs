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

            //Drop down list for departure city
            if (SelectedDepartureCity == 0) //they chose all departure cities
            {
                ViewBag.SelectedDepartureCity = "No departure city was selected";
            }
            else //city was chosen
            {
                //Set the AllCities list from the GetCities method that is in the city model?
                List<City> AllCities = db.Cities.ToList();
                City CityToDisplay = AllCities.Find(c => c.CityID == SelectedDepartureCity);
                ViewBag.SelectedDepartureCity = "The selected departure city is " + CityToDisplay.CityName;

                //Query the results based on the selected departure city
                query = query.Where(f => f.DepartureCity == CityToDisplay.CityName);
            }

            //Set up selected flights list based on query results
            List<Flight> SelectedFlights = query.ToList();

            //send to view
            return View("Index", SelectedFlights.OrderBy(f => f.DepartureCity));
        }



        public SelectList GetAllCities()
        {
            var query = from c in db.Cities
                        orderby c.CityName
                        select c;

            List<City> allCities = query.ToList();

            SelectList allCitieslist = new SelectList(allCities, "CityID", "CityName");

            return allCitieslist;
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
            ViewBag.AllCities = GetAllCities();
            ViewBag.AllDays = GetAllDays();

            int BiggestFlight = 100;

            if (db.Cities.ToList().Any() == false)
            {
                BiggestFlight = 100;
                ViewBag.FlightNumber = BiggestFlight;
            }
            else
            {
                foreach (Flight flight in db.Flights.ToList())
                {
                    if (flight.FlightNumber >= BiggestFlight)
                    {
                        BiggestFlight = flight.FlightNumber;
                        ViewBag.FlightNumber = BiggestFlight + 1;
                    }
                }
            }
            if (ViewBag.FlightNumber == null)
            {
                ViewBag.FlightNumber = 100;
            }
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //(Int32 BaseFare, List<ListItem> SelectedDays, DateTime DepTime, int SelectedDepCity, int SelectedArrivalCity)
        public ActionResult Create(int FlightNumber, int SelectedDepCity, int SelectedArrivalCity, List<ListItem> SelectedDays, Decimal BaseFare, DateTime DepartureTime)
        {
            City DepartureCity = db.Cities.Find(SelectedDepCity);
            City ArrivalCity = db.Cities.Find(SelectedArrivalCity);
            if (ModelState.IsValid)
            {
                //Array values = Enum.GetValues(typeof(Days));
                //List<ListItem> allDays = new List<ListItem>(values.Length);

                foreach (var i in SelectedDays)
                {
                    if (i.Value == "Monday")
                    {
                        Flight Dec11Flight = new Flight();
                        Dec11Flight.FlightNumber = FlightNumber;
                        Dec11Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec11Flight.DepartureCity = DepartureCity.CityName;
                        Dec11Flight.BaseFare = BaseFare;
                        Dec11Flight.DepartureTime = DepartureTime;
                        Dec11Flight.Date = new DateTime(2017,12,11);

                        Flight Dec18Flight = new Flight();
                        Dec11Flight.FlightNumber = FlightNumber;
                        Dec11Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec11Flight.DepartureCity = DepartureCity.CityName;
                        Dec11Flight.BaseFare = BaseFare;
                        Dec11Flight.DepartureTime = DepartureTime;
                        Dec11Flight.Date = new DateTime(2017, 12, 18);

                        Flight Dec25Flight = new Flight();
                        Dec11Flight.FlightNumber = FlightNumber;
                        Dec11Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec11Flight.DepartureCity = DepartureCity.CityName;
                        Dec11Flight.BaseFare = BaseFare;
                        Dec11Flight.DepartureTime = DepartureTime;
                        Dec11Flight.Date = new DateTime(2017, 12, 25);
                    }
                    //if (i.Value == "Tuesday")
                    //{
                    //    Flight Dec12Flight = new Flight;
                    //    Dec12Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec12Flight.ArrivalCity = ArrivalCity;
                    //    Dec12Flight.DepartureCity = DepartureCity;
                    //    Dec12Flight.BaseFare = BaseFare;
                    //    Dec12Flight.Date = "2017-12-12";

                    //    Flight Dec19Flight = new Flight;
                    //    Dec19Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec19Flight.ArrivalCity = ArrivalCity;
                    //    Dec19Flight.DepartureCity = DepartureCity;
                    //    Dec19Flight.BaseFare = BaseFare;
                    //    Dec19Flight.Date = "2017-12-19";
                    //}
                    //if (DayOfWeek == "Wednesday")
                    //{
                    //    Flight Dec12Flight = new Flight;
                    //    Dec12Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec12Flight.ArrivalCity = ArrivalCity;
                    //    Dec12Flight.DepartureCity = DepartureCity;
                    //    Dec12Flight.BaseFare = BaseFare;
                    //    Dec12Flight.Date = "2017-12-12";

                    //    Flight Dec19Flight = new Flight;
                    //    Dec19Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec19Flight.ArrivalCity = ArrivalCity;
                    //    Dec19Flight.DepartureCity = DepartureCity;
                    //    Dec19Flight.BaseFare = BaseFare;
                    //    Dec19Flight.Date = "2017-12-19";
                    //}
                    //if (DayOfWeek == "Wednesday")
                    //{
                    //    Flight Dec12Flight = new Flight;
                    //    Dec12Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec12Flight.ArrivalCity = ArrivalCity;
                    //    Dec12Flight.DepartureCity = DepartureCity;
                    //    Dec12Flight.BaseFare = BaseFare;
                    //    Dec12Flight.Date = "2017-12-12";

                    //    Flight Dec19Flight = new Flight;
                    //    Dec19Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec19Flight.ArrivalCity = ArrivalCity;
                    //    Dec19Flight.DepartureCity = DepartureCity;
                    //    Dec19Flight.BaseFare = BaseFare;
                    //    Dec19Flight.Date = "2017-12-19";
                    //}
                    //if (DayOfWeek == "Wednesday")
                    //{
                    //    Flight Dec12Flight = new Flight;
                    //    Dec12Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec12Flight.ArrivalCity = ArrivalCity;
                    //    Dec12Flight.DepartureCity = DepartureCity;
                    //    Dec12Flight.BaseFare = BaseFare;
                    //    Dec12Flight.Date = "2017-12-12";

                    //    Flight Dec19Flight = new Flight;
                    //    Dec19Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec19Flight.ArrivalCity = ArrivalCity;
                    //    Dec19Flight.DepartureCity = DepartureCity;
                    //    Dec19Flight.BaseFare = BaseFare;
                    //    Dec19Flight.Date = "2017-12-19";
                    //}
                    //if (DayOfWeek == "Wednesday")
                    //{
                    //    Flight Dec12Flight = new Flight;
                    //    Dec12Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec12Flight.ArrivalCity = ArrivalCity;
                    //    Dec12Flight.DepartureCity = DepartureCity;
                    //    Dec12Flight.BaseFare = BaseFare;
                    //    Dec12Flight.Date = "2017-12-12";

                    //    Flight Dec19Flight = new Flight;
                    //    Dec19Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec19Flight.ArrivalCity = ArrivalCity;
                    //    Dec19Flight.DepartureCity = DepartureCity;
                    //    Dec19Flight.BaseFare = BaseFare;
                    //    Dec19Flight.Date = "2017-12-19";
                    //}
                    //if (DayOfWeek == "Wednesday")
                    //{
                    //    Flight Dec12Flight = new Flight;
                    //    Dec12Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec12Flight.ArrivalCity = ArrivalCity;
                    //    Dec12Flight.DepartureCity = DepartureCity;
                    //    Dec12Flight.BaseFare = BaseFare;
                    //    Dec12Flight.Date = "2017-12-12";

                    //    Flight Dec19Flight = new Flight;
                    //    Dec19Flight.FlightNumber = NewTopFlightNumber;
                    //    Dec19Flight.ArrivalCity = ArrivalCity;
                    //    Dec19Flight.DepartureCity = DepartureCity;
                    //    Dec19Flight.BaseFare = BaseFare;
                    //    Dec19Flight.Date = "2017-12-19";
                    //}

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
    }
}
