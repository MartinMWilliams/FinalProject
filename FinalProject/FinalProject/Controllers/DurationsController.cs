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
            ViewBag.SelectedCity = GetSelectedCity(city);
            ViewBag.AllCities = GetAllCities();
            //ViewBag.RemainingCities = GetRemainingCities(city);
            return View();
        }


        // POST: Durations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DurationID,Mileage,FlightTime")] Duration duration1, Int32 City1ID, Int32 City2ID)
        {
            List<City> allCities = db.Cities.ToList();
            
            City SelectedCity1 = db.Cities.Find(City1ID);
            duration1.City1 = SelectedCity1;
            

            City SelectedCity2 = db.Cities.Find(City2ID);
            duration1.City2 = SelectedCity2;

            List<City> selectedCities = db.Durations.Select(c => c.City2).ToList();
            if (selectedCities.Contains(SelectedCity1) == false)
            {
                selectedCities.Add(SelectedCity1);
            }

            Duration duration2 = new Duration();
            duration2.City1 = SelectedCity2;
            duration2.City2 = SelectedCity1;
            duration2.Mileage = duration1.Mileage;
            duration2.FlightTime = duration1.FlightTime;


            if (ModelState.IsValid)
            {
                db.Durations.Add(duration1);
                db.Durations.Add(duration2);
                db.SaveChanges();

                //var set = new HashSet<City>(allCities);
                //var equals = set.SetEquals(selectedCities);
                //if (equals == true)
                //{

                //    return RedirectToAction("Index", "Cities");
                //}

                //if (GetRemainingCities(SelectedCity1).ToList().Any() == false)
                //{
                //    return RedirectToAction("Index", "Cities");
                //}

                return RedirectToAction("Create", "Durations",SelectedCity1);
            }

            ViewBag.AllCities = GetAllCities();
            return View(duration1);
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

        public SelectList GetRemainingCities(City SelectedCity1)
        {
            List<City> AllCities = db.Cities.ToList();

            List<City> SelectedCities = db.Durations.Select(c => c.City2).ToList();
            if (SelectedCities.Contains(SelectedCity1) == false)
            {
                SelectedCities.Add(SelectedCity1);
            }


            List<City> citylist = AllCities.Except(SelectedCities).ToList();

            SelectList RemainingCities = new SelectList(citylist, "CityID", "CityName");

            return RemainingCities;
        }

        
    }
}
