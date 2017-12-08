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
    public class ReservationFlightDetailsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: ReservationFlightDetails
        public ActionResult Index()
        {
            return View(db.ReservationFlightDetails.ToList());
        }

        // GET: ReservationFlightDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationFlightDetail reservationFlightDetail = db.ReservationFlightDetails.Find(id);
            if (reservationFlightDetail == null)
            {
                return HttpNotFound();
            }
            return View(reservationFlightDetail);
        }

        // GET: ReservationFlightDetails/Create
        //public ActionResult Create(ReservationViewModel FlightInfo)
        public ActionResult Create(ReservationViewModel FlightInfo) //int ReservationNumber, bool RoundTrip, bool AnotherFlight, int NumberOfFliers,  int FlightID
        {
            ReservationFlightDetail flightdetail = new ReservationFlightDetail();
            
            ViewBag.ReservationNumber = FlightInfo.ReservationNumber;
            ViewBag.Fare = GetBaseFare(FlightInfo.FlightID);
            ViewBag.SelectedFlight = GetFlight(FlightInfo.FlightID);
            ViewBag.AllUsers = GetAllUsers();
            ViewBag.RemainingSeats = GetAvailableSeats(FlightInfo.FlightID);

            flightdetail.Flight = db.Flights.First(f => f.FlightID == FlightInfo.FlightID);
            flightdetail.Reservation = db.Reservations.First(r => r.ReservationNumber == FlightInfo.ReservationNumber);
            return View();
        }

        public ActionResult TicketDetails(int ReservationNumber, Seats SeatAssignment, int Fare, int FlightID, String id )
        {
            //ReservationFlightDetail ticket = new ReservationFlightDetail();
            //ticket.Reservation = db.Reservations.First(r => r.ReservationNumber == ReservationNumber);
            //ticket.Flight = db.Flights.First(f => f.FlightID == FlightID);
            //ticket.User = db.Users.First(u => u.Id == id);
            //ticket.SeatAssignment = SeatAssignment;
            
            return View();
        }

        // POST: ReservationFlightDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationFlightDetailID,SeatAssignment,Fare")] ReservationFlightDetail reservationFlightDetail)
        {
            if (ModelState.IsValid)
            {
                db.ReservationFlightDetails.Add(reservationFlightDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservationFlightDetail);
        }

        // GET: ReservationFlightDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationFlightDetail reservationFlightDetail = db.ReservationFlightDetails.Find(id);
            if (reservationFlightDetail == null)
            {
                return HttpNotFound();
            }
            return View(reservationFlightDetail);
        }

        // POST: ReservationFlightDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details tsee https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationFlightDetailID,SeatAssignment,Fare")] ReservationFlightDetail reservationFlightDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservationFlightDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservationFlightDetail);
        }

        // GET: ReservationFlightDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationFlightDetail reservationFlightDetail = db.ReservationFlightDetails.Find(id);
            if (reservationFlightDetail == null)
            {
                return HttpNotFound();
            }
            return View(reservationFlightDetail);
        }

        // POST: ReservationFlightDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReservationFlightDetail reservationFlightDetail = db.ReservationFlightDetails.Find(id);
            db.ReservationFlightDetails.Remove(reservationFlightDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public SelectList GetAllUsers()
        {
            var query = from c in db.Users
                        orderby c.AdvantageNumber
                        select c;

            List<AppUser> allUsers = query.ToList();

            SelectList allCitieslist = new SelectList(allUsers, "id", "AdvantageNumber");

            return allCitieslist;
        }

        public Decimal GetFare(Flight fareflight, AppUser fareuser)
        {
            Decimal faretochange = fareflight.BaseFare;
            DateTime now = DateTime.Today;
            int age = now.Year - fareuser.DateofBirth.Year;

            if (age > 65)
            {
                faretochange = faretochange * (.9m);
            }

            if (age >= 3 && age <= 12)
            {
                faretochange = faretochange * (.85m);
            }

            return faretochange;
        }

        public int GetAvailableSeats(int FlightID)
        {
            Flight flight = db.Flights.First(f => f.FlightID == FlightID);
            List<Seats> AvailableSeats = new List<Seats>();
            foreach (ReservationFlightDetail ticket in (dynamic)flight)
            {

            }

            int a = 1;
            return a;
        }

        public Decimal GetBaseFare(int FlightID)
        {
            Flight flight = db.Flights.First(f => f.FlightID == FlightID);
            Decimal basefare = flight.BaseFare;
            return basefare;
        }

        public SelectList GetFlight(int FlightID)
        {
            var query = from f in db.Flights
                        where f.FlightID == FlightID
                        select f;

            List<Flight> flight = query.ToList<Flight>();


            SelectList allCitieslist = new SelectList(flight, "FlightID", "FlightNumber");

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
