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
        public ActionResult Create()
        {
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
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
