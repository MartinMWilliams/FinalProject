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

namespace FinalProject.Controllers
{
    public class ReservationsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            return View(db.Reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.AllCities = GetAllCities();
            ReservationViewModel reservation = new ReservationViewModel();

            int BiggestRes = 10000;


            if (db.Reservations.ToList().Any() == false)
            {
                BiggestRes = 10000;
                reservation.ReservationNumber = BiggestRes;
            }
            else
            {
                foreach (Reservation r in db.Reservations.ToList())
                {
                    if (r.ReservationNumber >= BiggestRes)
                    {
                        BiggestRes = r.ReservationNumber + 1;
                        reservation.ReservationNumber = BiggestRes;
                    }
                }
            }
            if (reservation.ReservationNumber == 0)
            {
                reservation.ReservationNumber = BiggestRes;
            }
            return View(reservation);
        }

        public ActionResult ReservationSearchResults( int SelectedDepartureCity, int SelectedArrivalCity, DateTime SelectedDate)
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

            //if (SelectedDate is null) //they chose all arrival cities
            //{
            //    ViewBag.SelectedDate = "No date was selected";
            //}
            
            
                query = query.Where(f => f.Date == SelectedDate);
            

            //Set up selected flights list based on query results
            List<Flight> SelectedFlights = query.ToList();

            SelectList FlightDropDown = new SelectList(SelectedFlights, "FlightID", "FlightNumber");
            ViewBag.AvailableFlights = FlightDropDown;
            //send to view
            return View("ReservationSearchResult", SelectedFlights.OrderBy(f => f.FlightNumber));
        }

        public ActionResult ReservationDetails(int FlightID)
        {
            ReservationViewModel reservation = new ReservationViewModel();
            reservation.SelectedFlight = db.Flights.Find(FlightID);
            ViewBag.AllUsers = GetAllUsers();
            int BiggestRes = 10000;


            if (db.Reservations.ToList().Any() == false)
            {
                BiggestRes = 10000;
                reservation.ReservationNumber = BiggestRes;
            }
            else
            {
                foreach (Reservation r in db.Reservations.ToList())
                {
                    if (r.ReservationNumber >= BiggestRes)
                    {
                        BiggestRes = r.ReservationNumber + 1;
                        reservation.ReservationNumber = BiggestRes;
                    }
                }
            }
            if (reservation.ReservationNumber == 0)
            {
                reservation.ReservationNumber = BiggestRes;
            }
            return View(reservation);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ReservationID")] Reservation reservation)
        public ActionResult Create(int ReservationNumber, bool RoundTrip, bool AnotherFlight, int NumberOfFliers, Flight SelectedFlight)
        {
            ReservationViewModel FlightInfo = new ReservationViewModel();
            FlightInfo.ReservationNumber = ReservationNumber;
            FlightInfo.RoundTrip = RoundTrip;
            FlightInfo.AnotherFlight = AnotherFlight;
            FlightInfo.NumberOfFliers = NumberOfFliers;
            FlightInfo.SelectedFlight = SelectedFlight;

            return RedirectToAction("Create", "ReservationFlightDetails", FlightInfo);
            //if (ModelState.IsValid)
            //{
            //    db.Reservations.Add(reservation);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View();
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
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

        public SelectList GetAllUsers()
        {
            var query = from c in db.Users
                        orderby c.AdvantageNumber
                        select c;

            List<AppUser> allUsers= query.ToList();

            SelectList allCitieslist = new SelectList(allUsers, "id", "AdvantageNumber");

            return allCitieslist;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
