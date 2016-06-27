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
    //[Authorize]
    public class ClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Classes
        public ActionResult Index()
        {
            return View(db.Classes.ToList());
        }

        // GET: Classes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
            // Creation of select lists for Subjects and Professors
            // We give them the PK column names of our Group table so that it will be easy to bind
            ViewBag.SubjectID = new SelectList(db.Subjects.OrderBy(x => x.Reference), "SubjectID", "FullName");
            ViewBag.ProfessorID = new SelectList(db.Professors.OrderBy(x => x.Name), "Id", "DisplayFullName");
            return View();
        }

        // POST: Classes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassID,StartDateTime,EndDateTime,SubjectID,ProfessorID")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(@class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.Subjects.OrderBy(x => x.Reference), "SubjectID", "FullName", @class.Subject);
            ViewBag.ProfessorID = new SelectList(db.Professors.OrderBy(x => x.Name), "Id", "DisplayFullName", @class.Professor);
            return View(@class);
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            //@class.StartDateTime = @class.StartDateTime.ToUniversalTime("yyy-MM-dd hh:mm");
            ViewBag.SubjectID = new SelectList(db.Subjects.OrderBy(x => x.Reference), "SubjectID", "FullName", @class.SubjectID);
            ViewBag.ProfessorID = new SelectList(db.Professors.OrderBy(x => x.Name), "Id", "DisplayFullName", @class.ProfessorID);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,StartDateTime,EndDateTime, SubjectID, ProfessorID")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@class);
        }

        // GET: Classes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Classes.Find(id);
            db.Classes.Remove(@class);
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


        // GET: /Classes/Groups/5
        public ActionResult Groups(int id)
        {
            // Get the list of Groups            
            Class @class = db.Classes.Find(id);

            var groupClasses = (from gc in db.GroupClasses
                                 where gc.ClassID.Equals(id)
                                 select gc.GroupID).ToList();
            
            ViewBag.Groups = db.Groups.ToList().Select(x => new SelectListItem()
            {
                Selected = groupClasses.Contains(x.GroupID),
                Text = x.Name,
                Value = x.GroupID.ToString()
            });
            return View(@class);
        }


        // POST: /Classes/Groups/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Groups([Bind(Include = "ClassID,selectedGroups")]int ClassID, string[] selectedGroups)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Class @class = db.Classes.Find(ClassID);
                    if (@class == null)
                    {
                        return HttpNotFound();
                    }

                    IEnumerable<GroupClasses> PreviousGroups = db.GroupClasses.Where(x => x.ClassID.Equals(ClassID));
                    foreach (var item in PreviousGroups)
                    {
                        db.GroupClasses.Remove(item);
                    }
                    db.SaveChanges();

                    if (selectedGroups != null)
                    {
                        foreach (string GroupID in selectedGroups)
                        {
                            db.GroupClasses.Add(new GroupClasses { GroupID = int.Parse(GroupID), ClassID = ClassID });
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
