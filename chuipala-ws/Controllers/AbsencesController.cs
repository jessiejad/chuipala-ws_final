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
    public class AbsencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Absences
        public ActionResult Index()
        {
            var Absences = db.Absences.Include(a => a.User);
            return View(Absences.ToList());
        }

        // GET: Absences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = db.Absences.Find(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }

        // GET: Absences/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "Id", "DisplayFullName");
            return View();
        }

        // POST: Absences/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Reason,UserID,StartDateTime,StopDateTime")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                TimeSpan value = absence.StopDateTime - absence.StartDateTime;
                if (value.Days <= 0)
                {
                    absence.Value = value.Hours;
                    absence.ValueUnit = "heure";
                    if (value.Hours > 1) absence.ValueUnit += "s";
                } else
                {
                    absence.Value = value.Days;
                    absence.ValueUnit = "jour";
                    if (value.Days > 1) absence.ValueUnit += "s";
                }

                var dbAbsence = db.Absences.Add(absence);

                IEnumerable<int> concernedClasses;

                var user = db.Users.Find(absence.UserID);
                if(user.IsProfessor)
                {
                    concernedClasses = (from @class in db.Classes
                                        where @class.ProfessorID == user.Id
                                        where (@class.StartDateTime.CompareTo(absence.StartDateTime) >= 0 && @class.StartDateTime.CompareTo(absence.StopDateTime) <= 0)
                                        select @class.ClassID).ToList();
                } else
                {
                    concernedClasses = (from @class in db.Classes
                                        join gc in db.GroupClasses on @class.ClassID equals gc.ClassID
                                        join @group in db.Groups on gc.GroupID equals @group.GroupID
                                        join gs in db.GroupStudents on @group.GroupID equals gs.GroupID
                                        where (gs.StudentID == user.Id)
                                        where (@class.StartDateTime.CompareTo(absence.StartDateTime) >= 0 && @class.StartDateTime.CompareTo(absence.StopDateTime) <= 0)
                                        select @class.ClassID).ToList();
                }
                

                foreach (int ClassID in concernedClasses)
                {
                    db.AbsenceClasses.Add(new AbsenceClasses { ClassID = ClassID, EventID = dbAbsence.EventID });
                };

               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "Id", "DisplayFullName", absence.UserID);
            return View(absence);
        }

        // GET: Absences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = db.Absences.Find(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "DisplayFullName", absence.UserID);
            return View(absence);
        }

        // POST: Absences/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,Reason,UserID,StartDateTime,StopDateTime")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                TimeSpan value = absence.StopDateTime - absence.StartDateTime;
                if (value.Days <= 0)
                {
                    absence.Value = value.Hours;
                    absence.ValueUnit = "heure";
                    if (value.Hours > 1) absence.ValueUnit += "s";
                }
                else
                {
                    absence.Value = value.Days;
                    absence.ValueUnit = "jour";
                    if (value.Days > 1) absence.ValueUnit += "s";
                }
                db.Entry(absence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "DisplayFullName", absence.UserID);
            return View(absence);
        }

        // GET: Absences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = db.Absences.Find(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }

        // POST: Absences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.AbsenceClasses.RemoveRange(db.AbsenceClasses.Where(x => x.EventID == id));
            Absence absence = db.Absences.Find(id);
            db.Absences.Remove(absence);
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


        // GET: /Absences/Classes/5
        public ActionResult Classes(int id)
        {
            // Get the list of Classes           
            Absence absence = db.Absences.Find(id);

            var absenceClasses = (from ac in db.AbsenceClasses
                                  where ac.EventID.Equals(id)
                                  select ac.ClassID).ToList();

            ViewBag.Classes = db.Classes.ToList().Select(x => new SelectListItem()
            {
                Selected = absenceClasses.Contains(x.ClassID),
                Text = x.ClassIdentity,
                Value = x.ClassID.ToString()
            });
            return View(absence);
        }


        // POST: /Absences/Classes/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Classes([Bind(Include = "EventID,selectedClasses")]int EventID, string[] selectedClasses)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Absence absence = db.Absences.Find(EventID);
                    if (absence == null)
                    {
                        return HttpNotFound();
                    }

                    IEnumerable<AbsenceClasses> PreviousClasses = db.AbsenceClasses.Where(x => x.EventID.Equals(EventID));
                    foreach (var item in PreviousClasses)
                    {
                        db.AbsenceClasses.Remove(item);
                    }
                    db.SaveChanges();

                    if (selectedClasses != null)
                    {
                        foreach (string ClassID in selectedClasses)
                        {
                            db.AbsenceClasses.Add(new AbsenceClasses { ClassID = int.Parse(ClassID), EventID = EventID });
                        };
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View();
        }


    }
}
