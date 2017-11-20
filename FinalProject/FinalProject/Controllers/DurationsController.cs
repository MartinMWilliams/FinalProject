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
    public class DurationsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Durations
        public ActionResult Index()
        {
            return View(db.Durations.ToList());
        }

        // GET: Durations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Duration duration = db.Durations.Find(id);
            if (duration == null)
            {
                return HttpNotFound();
            }
            return View(duration);
        }

        // GET: Durations/Create
        public ActionResult Create(City city)
        {
            City DepartureCity = city;
            ViewBag.SelectedCity = GetSelectedCity(DepartureCity);
            ViewBag.AllCities = GetAllCities();
            return View();
        }

        // POST: Durations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DurationID,Mileage,FlightTime")] Duration duration, Int32 City1ID, Int32 City2ID)
        {
            City SelectedCity1 = db.Cities.Find(City1ID);
            SelectedCity1.Durations.Add(duration);
            duration.City1 = SelectedCity1;

            City SelectedCity2 = db.Cities.Find(City2ID);
            SelectedCity2.Durations.Add(duration);
            duration.City2 = SelectedCity2;

            if (ModelState.IsValid)
            {
                db.Durations.Add(duration);
                db.SaveChanges();
                return RedirectToAction("Index","Cities");
            }

            ViewBag.AllCities = GetAllCities();
            return View(duration);
        }

        // GET: Durations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Duration duration = db.Durations.Find(id);
            if (duration == null)
            {
                return HttpNotFound();
            }
            return View(duration);
        }

        // POST: Durations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DurationID,Mileage,FlightTime")] Duration duration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(duration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(duration);
        }

        // GET: Durations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Duration duration = db.Durations.Find(id);
            if (duration == null)
            {
                return HttpNotFound();
            }
            return View(duration);
        }

        // POST: Durations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Duration duration = db.Durations.Find(id);
            db.Durations.Remove(duration);
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

        public SelectList GetAllCities()
        {
            var query = from c in db.Cities
                        orderby c.CityName
                        select c;

            List<City> allCities = query.ToList();

            SelectList allCitiesList = new SelectList(allCities, "CityID", "CityName");
            return allCitiesList;
        }

        public SelectList GetSelectedCity(City city)
        {
            var query = from c in db.Cities
                        where c.CityName == city.CityName
                        select c;

            List<City> cityList = query.ToList();

            SelectList SelectedCity = new SelectList(cityList, "CityID", "CityName");
            return SelectedCity;

        }
    }
}
