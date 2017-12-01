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
                        Flight Dec4Flight = new Flight();
                        Dec4Flight.FlightNumber = FlightNumber;
                        Dec4Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec4Flight.DepartureCity = DepartureCity.CityName;
                        Dec4Flight.BaseFare = BaseFare;
                        Dec4Flight.DepartureTime = DepartureTime;
                        Dec4Flight.Date = new DateTime(2017, 12, 4);

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
                    if (i.Value == "Tuesday")
                    {
                        Flight Dec5Flight = new Flight();
                        Dec5Flight.FlightNumber = FlightNumber;
                        Dec5Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec5Flight.DepartureCity = DepartureCity.CityName;
                        Dec5Flight.BaseFare = BaseFare;
                        Dec5Flight.DepartureTime = DepartureTime;
                        Dec5Flight.Date = new DateTime(2017, 12, 5);

                        Flight Dec12Flight = new Flight();
                        Dec12Flight.FlightNumber = FlightNumber;
                        Dec12Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec12Flight.DepartureCity = DepartureCity.CityName;
                        Dec12Flight.BaseFare = BaseFare;
                        Dec12Flight.DepartureTime = DepartureTime;
                        Dec12Flight.Date = new DateTime(2017, 12, 12);

                        Flight Dec19Flight = new Flight();
                        Dec19Flight.FlightNumber = FlightNumber;
                        Dec19Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec19Flight.DepartureCity = DepartureCity.CityName;
                        Dec19Flight.BaseFare = BaseFare;
                        Dec19Flight.DepartureTime = DepartureTime;
                        Dec19Flight.Date = new DateTime(2017, 12, 19);

                        Flight Dec26Flight = new Flight();
                        Dec26Flight.FlightNumber = FlightNumber;
                        Dec26Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec26Flight.DepartureCity = DepartureCity.CityName;
                        Dec26Flight.BaseFare = BaseFare;
                        Dec26Flight.DepartureTime = DepartureTime;
                        Dec26Flight.Date = new DateTime(2017, 12, 26);
                    }
                    if (i.Value == "Wednesday")
                    {
                        Flight Dec6Flight = new Flight();
                        Dec6Flight.FlightNumber = FlightNumber;
                        Dec6Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec6Flight.DepartureCity = DepartureCity.CityName;
                        Dec6Flight.BaseFare = BaseFare;
                        Dec6Flight.DepartureTime = DepartureTime;
                        Dec6Flight.Date = new DateTime(2017, 12, 6);

                        Flight Dec13Flight = new Flight();
                        Dec13Flight.FlightNumber = FlightNumber;
                        Dec13Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec13Flight.DepartureCity = DepartureCity.CityName;
                        Dec13Flight.BaseFare = BaseFare;
                        Dec13Flight.DepartureTime = DepartureTime;
                        Dec13Flight.Date = new DateTime(2017, 12, 13);

                        Flight Dec20Flight = new Flight();
                        Dec20Flight.FlightNumber = FlightNumber;
                        Dec20Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec20Flight.DepartureCity = DepartureCity.CityName;
                        Dec20Flight.BaseFare = BaseFare;
                        Dec20Flight.DepartureTime = DepartureTime;
                        Dec20Flight.Date = new DateTime(2017, 12, 20);

                        Flight Dec27Flight = new Flight();
                        Dec27Flight.FlightNumber = FlightNumber;
                        Dec27Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec27Flight.DepartureCity = DepartureCity.CityName;
                        Dec27Flight.BaseFare = BaseFare;
                        Dec27Flight.DepartureTime = DepartureTime;
                        Dec27Flight.Date = new DateTime(2017, 12, 27);
                    }
                    if (i.Value == "Thursday")
                    {
                        Flight Dec7Flight = new Flight();
                        Dec7Flight.FlightNumber = FlightNumber;
                        Dec7Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec7Flight.DepartureCity = DepartureCity.CityName;
                        Dec7Flight.BaseFare = BaseFare;
                        Dec7Flight.DepartureTime = DepartureTime;
                        Dec7Flight.Date = new DateTime(2017, 12, 7);

                        Flight Dec14Flight = new Flight();
                        Dec14Flight.FlightNumber = FlightNumber;
                        Dec14Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec14Flight.DepartureCity = DepartureCity.CityName;
                        Dec14Flight.BaseFare = BaseFare;
                        Dec14Flight.DepartureTime = DepartureTime;
                        Dec14Flight.Date = new DateTime(2017, 12, 14);

                        Flight Dec21Flight = new Flight();
                        Dec21Flight.FlightNumber = FlightNumber;
                        Dec21Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec21Flight.DepartureCity = DepartureCity.CityName;
                        Dec21Flight.BaseFare = BaseFare;
                        Dec21Flight.DepartureTime = DepartureTime;
                        Dec21Flight.Date = new DateTime(2017, 12, 21);

                        Flight Dec28Flight = new Flight();
                        Dec28Flight.FlightNumber = FlightNumber;
                        Dec28Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec28Flight.DepartureCity = DepartureCity.CityName;
                        Dec28Flight.BaseFare = BaseFare;
                        Dec28Flight.DepartureTime = DepartureTime;
                        Dec28Flight.Date = new DateTime(2017, 12, 28);
                    }
                    if (i.Value == "Friday")
                    {
                        Flight Dec1Flight = new Flight();
                        Dec1Flight.FlightNumber = FlightNumber;
                        Dec1Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec1Flight.DepartureCity = DepartureCity.CityName;
                        Dec1Flight.BaseFare = BaseFare;
                        Dec1Flight.DepartureTime = DepartureTime;
                        Dec1Flight.Date = new DateTime(2017, 12, 1);

                        Flight Dec8Flight = new Flight();
                        Dec8Flight.FlightNumber = FlightNumber;
                        Dec8Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec8Flight.DepartureCity = DepartureCity.CityName;
                        Dec8Flight.BaseFare = BaseFare;
                        Dec8Flight.DepartureTime = DepartureTime;
                        Dec8Flight.Date = new DateTime(2017, 12, 8);

                        Flight Dec15Flight = new Flight();
                        Dec15Flight.FlightNumber = FlightNumber;
                        Dec15Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec15Flight.DepartureCity = DepartureCity.CityName;
                        Dec15Flight.BaseFare = BaseFare;
                        Dec15Flight.DepartureTime = DepartureTime;
                        Dec15Flight.Date = new DateTime(2017, 12, 15);

                        Flight Dec22Flight = new Flight();
                        Dec22Flight.FlightNumber = FlightNumber;
                        Dec22Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec22Flight.DepartureCity = DepartureCity.CityName;
                        Dec22Flight.BaseFare = BaseFare;
                        Dec22Flight.DepartureTime = DepartureTime;
                        Dec22Flight.Date = new DateTime(2017, 12, 22);

                        Flight Dec29Flight = new Flight();
                        Dec29Flight.FlightNumber = FlightNumber;
                        Dec29Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec29Flight.DepartureCity = DepartureCity.CityName;
                        Dec29Flight.BaseFare = BaseFare;
                        Dec29Flight.DepartureTime = DepartureTime;
                        Dec29Flight.Date = new DateTime(2017, 12, 29);
                    }
                    if (i.Value == "Saturday")
                    {
                        Flight Dec2Flight = new Flight();
                        Dec2Flight.FlightNumber = FlightNumber;
                        Dec2Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec2Flight.DepartureCity = DepartureCity.CityName;
                        Dec2Flight.BaseFare = BaseFare;
                        Dec2Flight.DepartureTime = DepartureTime;
                        Dec2Flight.Date = new DateTime(2017, 12, 2);

                        Flight Dec9Flight = new Flight();
                        Dec9Flight.FlightNumber = FlightNumber;
                        Dec9Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec9Flight.DepartureCity = DepartureCity.CityName;
                        Dec9Flight.BaseFare = BaseFare;
                        Dec9Flight.DepartureTime = DepartureTime;
                        Dec9Flight.Date = new DateTime(2017, 12, 9);

                        Flight Dec16Flight = new Flight();
                        Dec16Flight.FlightNumber = FlightNumber;
                        Dec16Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec16Flight.DepartureCity = DepartureCity.CityName;
                        Dec16Flight.BaseFare = BaseFare;
                        Dec16Flight.DepartureTime = DepartureTime;
                        Dec16Flight.Date = new DateTime(2017, 12, 16);

                        Flight Dec23Flight = new Flight();
                        Dec23Flight.FlightNumber = FlightNumber;
                        Dec23Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec23Flight.DepartureCity = DepartureCity.CityName;
                        Dec23Flight.BaseFare = BaseFare;
                        Dec23Flight.DepartureTime = DepartureTime;
                        Dec23Flight.Date = new DateTime(2017, 12, 23);

                        Flight Dec30Flight = new Flight();
                        Dec30Flight.FlightNumber = FlightNumber;
                        Dec30Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec30Flight.DepartureCity = DepartureCity.CityName;
                        Dec30Flight.BaseFare = BaseFare;
                        Dec30Flight.DepartureTime = DepartureTime;
                        Dec30Flight.Date = new DateTime(2017, 12, 30);
                    }
                    if (i.Value == "Sunday")
                    {
                        Flight Dec3Flight = new Flight();
                        Dec3Flight.FlightNumber = FlightNumber;
                        Dec3Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec3Flight.DepartureCity = DepartureCity.CityName;
                        Dec3Flight.BaseFare = BaseFare;
                        Dec3Flight.DepartureTime = DepartureTime;
                        Dec3Flight.Date = new DateTime(2017, 12, 3);

                        Flight Dec10Flight = new Flight();
                        Dec10Flight.FlightNumber = FlightNumber;
                        Dec10Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec10Flight.DepartureCity = DepartureCity.CityName;
                        Dec10Flight.BaseFare = BaseFare;
                        Dec10Flight.DepartureTime = DepartureTime;
                        Dec10Flight.Date = new DateTime(2017, 12, 10);

                        Flight Dec17Flight = new Flight();
                        Dec17Flight.FlightNumber = FlightNumber;
                        Dec17Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec17Flight.DepartureCity = DepartureCity.CityName;
                        Dec17Flight.BaseFare = BaseFare;
                        Dec17Flight.DepartureTime = DepartureTime;
                        Dec17Flight.Date = new DateTime(2017, 12, 17);

                        Flight Dec24Flight = new Flight();
                        Dec24Flight.FlightNumber = FlightNumber;
                        Dec24Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec24Flight.DepartureCity = DepartureCity.CityName;
                        Dec24Flight.BaseFare = BaseFare;
                        Dec24Flight.DepartureTime = DepartureTime;
                        Dec24Flight.Date = new DateTime(2017, 12, 24);

                        Flight Dec31Flight = new Flight();
                        Dec31Flight.FlightNumber = FlightNumber;
                        Dec31Flight.ArrivalCity = ArrivalCity.CityName;
                        Dec31Flight.DepartureCity = DepartureCity.CityName;
                        Dec31Flight.BaseFare = BaseFare;
                        Dec31Flight.DepartureTime = DepartureTime;
                        Dec31Flight.Date = new DateTime(2017, 12, 31);
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
