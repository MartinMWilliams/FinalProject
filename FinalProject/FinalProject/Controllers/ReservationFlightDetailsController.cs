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
            ViewBag.AvailableSeats = GetAvailableSeats(FlightInfo.FlightID);

            flightdetail.Flight = db.Flights.First(f => f.FlightID == FlightInfo.FlightID);
            flightdetail.Reservation = db.Reservations.First(r => r.ReservationNumber == FlightInfo.ReservationNumber);
            return View();
        }

        public ActionResult TicketDetails(int ReservationNumber, Seats SeatAssignment, int Fare, int FlightID, String id )
        {
            ReservationFlightDetail ticket = new ReservationFlightDetail();

            ticket.Reservation = db.Reservations.First(r => r.ReservationNumber == ReservationNumber);
            ticket.Flight = db.Flights.First(f => f.FlightID == FlightID);
            ticket.User = db.Users.First(u => u.Id == id);
            ticket.SeatAssignment = SeatAssignment;
            ticket.Fare = CalculateFare(FlightID, id, SeatAssignment, Fare);
            Convert.ToDecimal(Fare);
            

            ViewBag.Reservation = GetReservation(ReservationNumber);
            ViewBag.Flight = GetFlight(FlightID);
            ViewBag.User = GetUser(id);
            return View(ticket);
        }

        // POST: ReservationFlightDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
       // public ActionResult Create([Bind(Include = "ReservationFlightDetailID,SeatAssignment,Fare,Reservation,Flight,User")] ReservationFlightDetail reservationFlightDetail)
        public ActionResult Create(Seats SeatAssignment, Decimal Fare, int ReservationID, int FlightID, String id)
        {
            int fare = (int)Fare;
            Fare = CalculateFare(FlightID, id, SeatAssignment, fare);
            ReservationFlightDetail reservationFlightDetail = new ReservationFlightDetail();
            reservationFlightDetail.SeatAssignment = SeatAssignment;
            reservationFlightDetail.Fare = Fare;
            reservationFlightDetail.Reservation = db.Reservations.First(r => r.ReservationID == ReservationID);
            reservationFlightDetail.Flight = db.Flights.First(f => f.FlightID == FlightID);
            reservationFlightDetail.User = db.Users.First(u => u.Id == id);


            ReservationViewModel FlightInfo = new ReservationViewModel();
            FlightInfo.ReservationNumber = db.Reservations.First(r=>r.ReservationID == ReservationID).ReservationNumber;
            //FlightInfo.RoundTrip = RoundTrip;
            //FlightInfo.AnotherFlight = AnotherFlight;
            //FlightInfo.NumberOfFliers = NumberOfFliers;
            FlightInfo.FlightID = FlightID;

            if (ModelState.IsValid)
            {
                db.ReservationFlightDetails.Add(reservationFlightDetail);
                db.SaveChanges();
                return RedirectToAction("Create",FlightInfo);
            }

            //return View(reservationFlightDetail);
            return View();
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

            SelectList allCitieslist = new SelectList(allUsers, "id", "AdvantageNumberName");

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

        public SelectList GetAvailableSeats(int FlightID)
        {
            Flight flight = db.Flights.First(f => f.FlightID == FlightID);

            //List<Seats> AvailableSeats = new List<Seats>();

            List<Seats> AllSeats = Seats.GetValues(typeof(Seats)).Cast<Seats>().ToList();

            List<Seats> TakenSeats = new List<Seats>();
            foreach (ReservationFlightDetail ticket in flight.ReservationFlightDetails)
            {
                TakenSeats.Add(ticket.SeatAssignment);
            }

            List<Seats> AvailableSeats = AllSeats.Except(TakenSeats).ToList();

            SelectList allCitieslist = new SelectList(AvailableSeats);// "value", "text"

            return allCitieslist;
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

        public SelectList GetUser(String id)
        {
            var query = from f in db.Users
                        where f.Id == id
                        select f;

            List<AppUser> flight = query.ToList<AppUser>();


            SelectList allCitieslist = new SelectList(flight, "id", "AdvantageNumber");

            return allCitieslist;
        }

        public SelectList GetReservation(int ReservationNumber)
        {
            var query = from f in db.Reservations
                        where f.ReservationNumber == ReservationNumber
                        select f;
           
            List<Reservation> flight = query.ToList<Reservation>();


            SelectList allCitieslist = new SelectList(flight, "ReservationID", "ReservationNumber");

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

        public Decimal CalculateFare(int FlightID, String id, Seats SeatAssignment, int fare)
        {
            Flight flight = db.Flights.First(f => f.FlightID == FlightID);
            AppUser flier = db.Users.First(u => u.Id == id);
            Seats seatassignment = SeatAssignment;
            var today = DateTime.Today;
            var age = today.Year - flier.DateofBirth.Year;
            Decimal Fare = Convert.ToDecimal(fare);

            if (((flight.Date - today).TotalDays / 7) >= 2)
            {
                
                Fare = fare * .9m;
            }

            Decimal newfare = new Decimal();



            if (age >= 65 && seatassignment != Seats.OneA && seatassignment != Seats.OneB && seatassignment != Seats.TwoA && seatassignment != Seats.TwoB)
            {
                newfare = Fare * .9m; //m is for magic!!!!
            }
            if (age >= 3 && age <= 12 && seatassignment != Seats.OneA && seatassignment != Seats.OneB && seatassignment != Seats.TwoA && seatassignment != Seats.TwoB)
            {
                newfare = Fare * .85m;
            }
            if (seatassignment == Seats.OneA || seatassignment == Seats.OneB || seatassignment == Seats.TwoA || seatassignment == Seats.TwoB)
            {
                newfare = Fare * 1.2m;
            }
            else
            {
                newfare = Fare;
            }
            return newfare;
        }
    }
}
