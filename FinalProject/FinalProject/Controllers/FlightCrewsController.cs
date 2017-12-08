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
using System.ComponentModel.DataAnnotations;


namespace FinalProject.Controllers
{
    public class FlightCrewsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: FlightCrews
        public ActionResult Index()
        {
            return View(db.FlightCrews.ToList());
        }

        // GET: FlightCrews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightCrew flightCrew = db.FlightCrews.Find(id);
            if (flightCrew == null)
            {
                return HttpNotFound();
            }
            return View(flightCrew);
        }

        // GET: FlightCrews/Create
        public ActionResult Create()
        {
            ViewBag.AllPilots = GetAllPilots();
            ViewBag.AllCoPilots = GetAllCoPilots();
            ViewBag.AllCabinCrew = GetAllCabin();
        ViewBag.AllFlights = GetAllFlights();
            return View();
        }

        // POST: FlightCrews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FlightCrewID,Pilot,CoPilot,CabinCrew")] FlightCrew flightCrew, Int32 FlightID)
        {
            Flight SelectedFlight = db.Flights.Find(FlightID);

            flightCrew.Flight = SelectedFlight;

            if (ModelState.IsValid)
            {
                db.FlightCrews.Add(flightCrew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllFlights = GetAllFlights(flightCrew);

            return View(flightCrew);
        }

        // GET: FlightCrews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightCrew flightCrew = db.FlightCrews.Find(id);
            if (flightCrew == null)
            {
                return HttpNotFound();
            }
            return View(flightCrew);
        }

        // POST: FlightCrews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlightCrewID,Pilot,CoPilot,CabinCrew")] FlightCrew flightCrew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flightCrew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flightCrew);
        }

        // GET: FlightCrews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightCrew flightCrew = db.FlightCrews.Find(id);
            if (flightCrew == null)
            {
                return HttpNotFound();
            }
            return View(flightCrew);
        }

        // POST: FlightCrews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FlightCrew flightCrew = db.FlightCrews.Find(id);
            db.FlightCrews.Remove(flightCrew);
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
        public SelectList GetAllPilots(FlightCrew pilot)  //COMMITTEE ALREADY CHOSEN
        {
            //populate list of committees
            var query = from c in db.Users
                        where c.EmployeeType == EmpType.Pilot
                        orderby c.Email
                        select c;

            //create list and execute query
            List<AppUser> allPilots = query.ToList();

            //convert to select list
            SelectList list = new SelectList(allPilots, "Email", "Email", pilot.Pilot);
            return list;
        }
        public SelectList GetAllPilots()
        {
            var query = from c in db.Users
                        where c.EmployeeType == EmpType.Pilot
                        orderby c.Email
                        select c;

            List<AppUser> allPilots = query.ToList();

            SelectList allPilotslist = new SelectList(allPilots, "Email", "Email");

            return allPilotslist;
        }
        public SelectList GetAllCoPilots(FlightCrew copilot)  //COMMITTEE ALREADY CHOSEN
        {
            //populate list of committees
            var query = from c in db.Users
                        where c.EmployeeType == EmpType.CoPilot
                        orderby c.Email
                        select c;

            //create list and execute query
            List<AppUser> allCoPilots = query.ToList();

            //convert to select list
            SelectList list = new SelectList(allCoPilots, "Email", "Email", copilot.CoPilot);
            return list;
        }
        public SelectList GetAllCoPilots()
        {
            var query = from c in db.Users
                        where c.EmployeeType == EmpType.CoPilot
                        orderby c.Email
                        select c;

            List<AppUser> allCoPilots = query.ToList();

            SelectList allCoPilotslist = new SelectList(allCoPilots, "Email", "Email");

            return allCoPilotslist;
        }
        public SelectList GetAllCabin(FlightCrew cabin)  //COMMITTEE ALREADY CHOSEN
        {
            //populate list of committees
            var query = from c in db.Users
                        where c.EmployeeType == EmpType.Cabin
                        orderby c.Email
                        select c;

            //create list and execute query
            List<AppUser> allCabins = query.ToList();

            //convert to select list
            SelectList list = new SelectList(allCabins, "Email", "Email", cabin.CabinCrew);
            return list;
        }
        public SelectList GetAllCabin()
        {
            var query = from c in db.Users
                        where c.EmployeeType == EmpType.Cabin
                        orderby c.Email
                        select c;

            List<AppUser> allCabins = query.ToList();

            SelectList allCabinslist = new SelectList(allCabins, "Email", "Email");

            return allCabinslist;
        }

        public SelectList GetAllFlights(FlightCrew flight)  //COMMITTEE ALREADY CHOSEN
        {
            //populate list of committees
            var query = from c in db.Flights
                        orderby c.FlightID
                        select c;
            //create list and execute query
            List<Flight> allFlights = query.ToList();

            //convert to select list
            SelectList list = new SelectList(allFlights, "FLightID", "FlightNumberDate", flight.Flight.FlightID);
            return list;
        }
        public SelectList GetAllFlights()  //NO COMMITTEE CHOOSEN
        {
            //create query to find all committees
            var query = from c in db.Flights
                        orderby c.FlightID
                        select c;
            //execute query and store in list
            List<Flight> allFlights = query.ToList();

            //convert list to select list format needed for HTML
            SelectList allFlightslist = new SelectList(allFlights,"FlightID", "FlightNumberDate", "Date");

            return allFlightslist;
        }

    }
    }


