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

            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Int32 BaseFare, int[] SelectedDays, DateTime DepTime, int SelectedDepCity, int SelectedArrivalCity)
        {
            if (ModelState.IsValid)
            {
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
