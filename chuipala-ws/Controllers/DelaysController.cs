using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using chuipala_ws.Models;

namespace chuipala_ws.Controllers
{
    public class DelaysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Delays
        public ActionResult Index()
        {
            var Delays = db.Delays.Include(d => d.User).Include(d => d.Class);
            return View(Delays.ToList());
        }

        // GET: Delays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delay delay = db.Delays.Find(id);
            if (delay == null)
            {
                return HttpNotFound();
            }
            return View(delay);
        }

        // GET: Delays/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "Id", "DisplayFullName");
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassIdentity");
            return View();
        }

        // POST: Delays/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,Reason,Value,ValueUnit,UserID,ClassID")] Delay delay)
        {
            if (ModelState.IsValid)
            {
                db.Delays.Add(delay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "Id", "DisplayFullName", delay.UserID);
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassIdentity", delay.ClassID);
            return View(delay);
        }

        // GET: Delays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delay delay = db.Delays.Find(id);
            if (delay == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "DisplayFullName", delay.UserID);
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassIdentity", delay.ClassID);
            return View(delay);
        }

        // POST: Delays/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,Reason,Value,ValueUnit,UserID,ClassID")] Delay delay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(delay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "DisplayFullName", delay.UserID);
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassIdentity", delay.ClassID);
            return View(delay);
        }

        // GET: Delays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delay delay = db.Delays.Find(id);
            if (delay == null)
            {
                return HttpNotFound();
            }
            return View(delay);
        }

        // POST: Delays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Delay delay = db.Delays.Find(id);
            db.Delays.Remove(delay);
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
