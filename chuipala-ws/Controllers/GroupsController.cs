using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using chuipala_ws.Models;
using Newtonsoft.Json;
using System.Net;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data;

namespace chuipala_ws.Controllers
{
    public class GroupsController : Controller
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();
        /*
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public GroupsController()
        {
        }

        public GroupsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }*/



        // GET: Groups
        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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

        // GET: /Groups/Students/5
        public ActionResult Students(int id)
        {
            // Get the list of Students            
            Group group = db.Groups.Find(id);

            var groupStudents = (from gs in db.GroupStudents
                                    where gs.GroupID.Equals(id)
                                    select gs.StudentID).ToList();

            //ViewBag.Group = group;
            ViewBag.Students = db.Students.ToList().Select(x => new SelectListItem()
            {
                Selected = groupStudents.Contains(x.Id),
                Text = x.DisplayFullName,
                Value = x.Id.ToString()
            });
            return View(group);
        }

        
        // POST: /Groups/Students/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Students([Bind(Include = "GroupID,selectedStudents")]int GroupID, string[] selectedStudents)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Group group = db.Groups.Find(GroupID);
                    if (group == null)
                    {
                        return HttpNotFound();
                    }

                    IEnumerable<GroupStudents> PreviousStudents = db.GroupStudents.Where(x => x.GroupID.Equals(GroupID));
                    foreach (var item in PreviousStudents)
                    {
                        db.GroupStudents.Remove(item);
                    }
                    db.SaveChanges(); 

                    if (selectedStudents != null)
                    {
                        foreach (string StudentID in selectedStudents)
                        {
                            db.GroupStudents.Add(new GroupStudents { GroupID = GroupID, StudentID = StudentID });
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
