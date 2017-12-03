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
                foreach (Flight f in db.Flights.ToList())
                {
                    if (f.FlightNumber >= BiggestFlight)
                    {
                        BiggestFlight = f.FlightNumber;
                        ViewBag.FlightNumber = BiggestFlight + 1;
                    }
                }
            }
            if (ViewBag.FlightNumber == null)
            {
                ViewBag.FlightNumber = 100;
            }

            return View(flight);
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //(Int32 BaseFare, List<ListItem> SelectedDays, DateTime DepTime, int SelectedDepCity, int SelectedArrivalCity)
        //public ActionResult Create([Bind(Include = "CityID, FlightNumber,DepartureCity,ArrivalCity,BaseFare,DepartureDate")]Flight flight, List<ListItem> SelectedDays)
        public ActionResult Create(FlightCreateViewModel flight)
        {
            if (ModelState.IsValid)
            {

                //Array values = Enum.GetValues(typeof(Days));
                //List<ListItem> allDays = new List<ListItem>(values.Length);

                foreach (var i in flight.SelectedDays)
                {
                    if (i.Value == "Monday")
                    {
                        Flight Dec4Flight = new Flight();
                        Dec4Flight.FlightNumber = flight.FlightNumber;
                        Dec4Flight.ArrivalCity = flight.ArrivalCity;
                        Dec4Flight.DepartureCity = flight.DepartureCity;
                        Dec4Flight.BaseFare = flight.BaseFare;
                        Dec4Flight.DepartureTime = flight.DepartureTime;
                        Dec4Flight.Date = new DateTime(2017, 12, 4);

                        Flight Dec11Flight = new Flight();
                        Dec11Flight.FlightNumber = flight.FlightNumber;
                        Dec11Flight.ArrivalCity = flight.ArrivalCity;
                        Dec11Flight.DepartureCity = flight.DepartureCity;
                        Dec11Flight.BaseFare = flight.BaseFare;
                        Dec11Flight.DepartureTime = flight.DepartureTime;
                        Dec11Flight.Date = new DateTime(2017, 12, 11);

                        Flight Dec18Flight = new Flight();
                        Dec11Flight.FlightNumber = flight.FlightNumber;
                        Dec11Flight.ArrivalCity = flight.ArrivalCity;
                        Dec11Flight.DepartureCity = flight.DepartureCity;
                        Dec11Flight.BaseFare = flight.BaseFare;
                        Dec11Flight.DepartureTime = flight.DepartureTime;
                        Dec11Flight.Date = new DateTime(2017, 12, 18);

                        Flight Dec25Flight = new Flight();
                        Dec11Flight.FlightNumber = flight.FlightNumber;
                        Dec11Flight.ArrivalCity = flight.ArrivalCity;
                        Dec11Flight.DepartureCity = flight.DepartureCity;
                        Dec11Flight.BaseFare = flight.BaseFare;
                        Dec11Flight.DepartureTime = flight.DepartureTime;
                        Dec11Flight.Date = new DateTime(2017, 12, 25);
                    }
                    if (i.Value == "Tuesday")
                    {
                        Flight Dec5Flight = new Flight();
                        Dec5Flight.FlightNumber = flight.FlightNumber;
                        Dec5Flight.ArrivalCity = flight.ArrivalCity;
                        Dec5Flight.DepartureCity = flight.DepartureCity;
                        Dec5Flight.BaseFare = flight.BaseFare;
                        Dec5Flight.DepartureTime = flight.DepartureTime;
                        Dec5Flight.Date = new DateTime(2017, 12, 5);

                        Flight Dec12Flight = new Flight();
                        Dec12Flight.FlightNumber = flight.FlightNumber;
                        Dec12Flight.ArrivalCity = flight.ArrivalCity;
                        Dec12Flight.DepartureCity = flight.DepartureCity;
                        Dec12Flight.BaseFare = flight.BaseFare;
                        Dec12Flight.DepartureTime = flight.DepartureTime;
                        Dec12Flight.Date = new DateTime(2017, 12, 12);

                        Flight Dec19Flight = new Flight();
                        Dec19Flight.FlightNumber = flight.FlightNumber;
                        Dec19Flight.ArrivalCity = flight.ArrivalCity;
                        Dec19Flight.DepartureCity = flight.DepartureCity;
                        Dec19Flight.BaseFare = flight.BaseFare;
                        Dec19Flight.DepartureTime = flight.DepartureTime;
                        Dec19Flight.Date = new DateTime(2017, 12, 19);

                        Flight Dec26Flight = new Flight();
                        Dec26Flight.FlightNumber = flight.FlightNumber;
                        Dec26Flight.ArrivalCity = flight.ArrivalCity;
                        Dec26Flight.DepartureCity = flight.DepartureCity;
                        Dec26Flight.BaseFare = flight.BaseFare;
                        Dec26Flight.DepartureTime = flight.DepartureTime;
                        Dec26Flight.Date = new DateTime(2017, 12, 26);
                    }
                    if (i.Value == "Wednesday")
                    {
                        Flight Dec6Flight = new Flight();
                        Dec6Flight.FlightNumber = flight.FlightNumber;
                        Dec6Flight.ArrivalCity = flight.ArrivalCity;
                        Dec6Flight.DepartureCity = flight.DepartureCity;
                        Dec6Flight.BaseFare = flight.BaseFare;
                        Dec6Flight.DepartureTime = flight.DepartureTime;
                        Dec6Flight.Date = new DateTime(2017, 12, 6);

                        Flight Dec13Flight = new Flight();
                        Dec13Flight.FlightNumber = flight.FlightNumber;
                        Dec13Flight.ArrivalCity = flight.ArrivalCity;
                        Dec13Flight.DepartureCity = flight.DepartureCity;
                        Dec13Flight.BaseFare = flight.BaseFare;
                        Dec13Flight.DepartureTime = flight.DepartureTime;
                        Dec13Flight.Date = new DateTime(2017, 12, 13);

                        Flight Dec20Flight = new Flight();
                        Dec20Flight.FlightNumber = flight.FlightNumber;
                        Dec20Flight.ArrivalCity = flight.ArrivalCity;
                        Dec20Flight.DepartureCity = flight.DepartureCity;
                        Dec20Flight.BaseFare = flight.BaseFare;
                        Dec20Flight.DepartureTime = flight.DepartureTime;
                        Dec20Flight.Date = new DateTime(2017, 12, 20);

                        Flight Dec27Flight = new Flight();
                        Dec27Flight.FlightNumber = flight.FlightNumber;
                        Dec27Flight.ArrivalCity = flight.ArrivalCity;
                        Dec27Flight.DepartureCity = flight.DepartureCity;
                        Dec27Flight.BaseFare = flight.BaseFare;
                        Dec27Flight.DepartureTime = flight.DepartureTime;
                        Dec27Flight.Date = new DateTime(2017, 12, 27);
                    }
                    if (i.Value == "Thursday")
                    {
                        Flight Dec7Flight = new Flight();
                        Dec7Flight.FlightNumber = flight.FlightNumber;
                        Dec7Flight.ArrivalCity = flight.ArrivalCity;
                        Dec7Flight.DepartureCity = flight.DepartureCity;
                        Dec7Flight.BaseFare = flight.BaseFare;
                        Dec7Flight.DepartureTime = flight.DepartureTime;
                        Dec7Flight.Date = new DateTime(2017, 12, 7);

                        Flight Dec14Flight = new Flight();
                        Dec14Flight.FlightNumber = flight.FlightNumber;
                        Dec14Flight.ArrivalCity = flight.ArrivalCity;
                        Dec14Flight.DepartureCity = flight.DepartureCity;
                        Dec14Flight.BaseFare = flight.BaseFare;
                        Dec14Flight.DepartureTime = flight.DepartureTime;
                        Dec14Flight.Date = new DateTime(2017, 12, 14);

                        Flight Dec21Flight = new Flight();
                        Dec21Flight.FlightNumber = flight.FlightNumber;
                        Dec21Flight.ArrivalCity = flight.ArrivalCity;
                        Dec21Flight.DepartureCity = flight.DepartureCity;
                        Dec21Flight.BaseFare = flight.BaseFare;
                        Dec21Flight.DepartureTime = flight.DepartureTime;
                        Dec21Flight.Date = new DateTime(2017, 12, 21);

                        Flight Dec28Flight = new Flight();
                        Dec28Flight.FlightNumber = flight.FlightNumber;
                        Dec28Flight.ArrivalCity = flight.ArrivalCity;
                        Dec28Flight.DepartureCity = flight.DepartureCity;
                        Dec28Flight.BaseFare = flight.BaseFare;
                        Dec28Flight.DepartureTime = flight.DepartureTime;
                        Dec28Flight.Date = new DateTime(2017, 12, 28);
                    }
                    if (i.Value == "Friday")
                    {
                        Flight Dec1Flight = new Flight();
                        Dec1Flight.FlightNumber = flight.FlightNumber;
                        Dec1Flight.ArrivalCity = flight.ArrivalCity;
                        Dec1Flight.DepartureCity = flight.DepartureCity;
                        Dec1Flight.BaseFare = flight.BaseFare;
                        Dec1Flight.DepartureTime = flight.DepartureTime;
                        Dec1Flight.Date = new DateTime(2017, 12, 1);

                        Flight Dec8Flight = new Flight();
                        Dec8Flight.FlightNumber = flight.FlightNumber;
                        Dec8Flight.ArrivalCity = flight.ArrivalCity;
                        Dec8Flight.DepartureCity = flight.DepartureCity;
                        Dec8Flight.BaseFare = flight.BaseFare;
                        Dec8Flight.DepartureTime = flight.DepartureTime;
                        Dec8Flight.Date = new DateTime(2017, 12, 8);

                        Flight Dec15Flight = new Flight();
                        Dec15Flight.FlightNumber = flight.FlightNumber;
                        Dec15Flight.ArrivalCity = flight.ArrivalCity;
                        Dec15Flight.DepartureCity = flight.DepartureCity;
                        Dec15Flight.BaseFare = flight.BaseFare;
                        Dec15Flight.DepartureTime = flight.DepartureTime;
                        Dec15Flight.Date = new DateTime(2017, 12, 15);

                        Flight Dec22Flight = new Flight();
                        Dec22Flight.FlightNumber = flight.FlightNumber;
                        Dec22Flight.ArrivalCity = flight.ArrivalCity;
                        Dec22Flight.DepartureCity = flight.DepartureCity;
                        Dec22Flight.BaseFare = flight.BaseFare;
                        Dec22Flight.DepartureTime = flight.DepartureTime;
                        Dec22Flight.Date = new DateTime(2017, 12, 22);

                        Flight Dec29Flight = new Flight();
                        Dec29Flight.FlightNumber = flight.FlightNumber;
                        Dec29Flight.ArrivalCity = flight.ArrivalCity;
                        Dec29Flight.DepartureCity = flight.DepartureCity;
                        Dec29Flight.BaseFare = flight.BaseFare;
                        Dec29Flight.DepartureTime = flight.DepartureTime;
                        Dec29Flight.Date = new DateTime(2017, 12, 29);
                    }
                    if (i.Value == "Saturday")
                    {
                        Flight Dec2Flight = new Flight();
                        Dec2Flight.FlightNumber = flight.FlightNumber;
                        Dec2Flight.ArrivalCity = flight.ArrivalCity;
                        Dec2Flight.DepartureCity = flight.DepartureCity;
                        Dec2Flight.BaseFare = flight.BaseFare;
                        Dec2Flight.DepartureTime = flight.DepartureTime;
                        Dec2Flight.Date = new DateTime(2017, 12, 2);

                        Flight Dec9Flight = new Flight();
                        Dec9Flight.FlightNumber = flight.FlightNumber;
                        Dec9Flight.ArrivalCity = flight.ArrivalCity;
                        Dec9Flight.DepartureCity = flight.DepartureCity;
                        Dec9Flight.BaseFare = flight.BaseFare;
                        Dec9Flight.DepartureTime = flight.DepartureTime;
                        Dec9Flight.Date = new DateTime(2017, 12, 9);

                        Flight Dec16Flight = new Flight();
                        Dec16Flight.FlightNumber = flight.FlightNumber;
                        Dec16Flight.ArrivalCity = flight.ArrivalCity;
                        Dec16Flight.DepartureCity = flight.DepartureCity;
                        Dec16Flight.BaseFare = flight.BaseFare;
                        Dec16Flight.DepartureTime = flight.DepartureTime;
                        Dec16Flight.Date = new DateTime(2017, 12, 16);

                        Flight Dec23Flight = new Flight();
                        Dec23Flight.FlightNumber = flight.FlightNumber;
                        Dec23Flight.ArrivalCity = flight.ArrivalCity;
                        Dec23Flight.DepartureCity = flight.DepartureCity;
                        Dec23Flight.BaseFare = flight.BaseFare;
                        Dec23Flight.DepartureTime = flight.DepartureTime;
                        Dec23Flight.Date = new DateTime(2017, 12, 23);

                        Flight Dec30Flight = new Flight();
                        Dec30Flight.FlightNumber = flight.FlightNumber;
                        Dec30Flight.ArrivalCity = flight.ArrivalCity;
                        Dec30Flight.DepartureCity = flight.DepartureCity;
                        Dec30Flight.BaseFare = flight.BaseFare;
                        Dec30Flight.DepartureTime = flight.DepartureTime;
                        Dec30Flight.Date = new DateTime(2017, 12, 30);
                    }
                    if (i.Value == "Sunday")
                    {
                        Flight Dec3Flight = new Flight();
                        Dec3Flight.FlightNumber = flight.FlightNumber;
                        Dec3Flight.ArrivalCity = flight.ArrivalCity;
                        Dec3Flight.DepartureCity = flight.DepartureCity;
                        Dec3Flight.BaseFare = flight.BaseFare;
                        Dec3Flight.DepartureTime = flight.DepartureTime;
                        Dec3Flight.Date = new DateTime(2017, 12, 3);

                        Flight Dec10Flight = new Flight();
                        Dec10Flight.FlightNumber = flight.FlightNumber;
                        Dec10Flight.ArrivalCity = flight.ArrivalCity;
                        Dec10Flight.DepartureCity = flight.DepartureCity;
                        Dec10Flight.BaseFare = flight.BaseFare;
                        Dec10Flight.DepartureTime = flight.DepartureTime;
                        Dec10Flight.Date = new DateTime(2017, 12, 10);

                        Flight Dec17Flight = new Flight();
                        Dec17Flight.FlightNumber = flight.FlightNumber;
                        Dec17Flight.ArrivalCity = flight.ArrivalCity;
                        Dec17Flight.DepartureCity = flight.DepartureCity;
                        Dec17Flight.BaseFare = flight.BaseFare;
                        Dec17Flight.DepartureTime = flight.DepartureTime;
                        Dec17Flight.Date = new DateTime(2017, 12, 17);

                        Flight Dec24Flight = new Flight();
                        Dec24Flight.FlightNumber = flight.FlightNumber;
                        Dec24Flight.ArrivalCity = flight.ArrivalCity;
                        Dec24Flight.DepartureCity = flight.DepartureCity;
                        Dec24Flight.BaseFare = flight.BaseFare;
                        Dec24Flight.DepartureTime = flight.DepartureTime;
                        Dec24Flight.Date = new DateTime(2017, 12, 24);

                        Flight Dec31Flight = new Flight();
                        Dec31Flight.FlightNumber = flight.FlightNumber;
                        Dec31Flight.ArrivalCity = flight.ArrivalCity;
                        Dec31Flight.DepartureCity = flight.DepartureCity;
                        Dec31Flight.BaseFare = flight.BaseFare;
                        Dec31Flight.DepartureTime = flight.DepartureTime;
                        Dec31Flight.Date = new DateTime(2017, 12, 31);
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

        public SelectList GetAllCities()
        {
            var query = from c in db.Cities
                        orderby c.CityName
                        select c;

            List<City> allCities = query.ToList();

            SelectList allCitieslist = new SelectList(allCities, "CityID", "CityName");

            return allCitieslist;
        }
    }
}
