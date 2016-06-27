using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using chuipala_ws.Models;

namespace chuipala_ws.Controllers
{
    public class ApiDelaysController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ApiDelays
        public IEnumerable<ClassDTO> GetDelays()
        {
            DateTime now = new DateTime();
            List<ClassDTO> ldto = new List<ClassDTO>();
            IEnumerable<Class> l = db.Classes.Where(x => x.StartDateTime >= now).Where(x => x.EndDateTime <= now).ToList();
            foreach (Class @class in l)
            {
                ldto.Add(new ClassDTO { ClassID = @class.ClassID, SubjectLabel = @class.Subject.Label });
            }

            return ldto;
        }

        // GET: api/ApiDelays/5
        [ResponseType(typeof(Delay))]
        public IHttpActionResult GetDelay(int id)
        {
            Delay delay = db.Delays.Find(id);
            if (delay == null)
            {
                return NotFound();
            }

            return Ok(delay);
        }

        // PUT: api/ApiDelays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDelay(int id, Delay delay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != delay.EventID)
            {
                return BadRequest();
            }

            db.Entry(delay).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DelayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ApiDelays
        [ResponseType(typeof(Delay))]
        public IHttpActionResult PostDelay(Delay delay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Delays.Add(delay);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = delay.EventID }, delay);
        }

        // DELETE: api/ApiDelays/5
        [ResponseType(typeof(Delay))]
        public IHttpActionResult DeleteDelay(int id)
        {
            Delay delay = db.Delays.Find(id);
            if (delay == null)
            {
                return NotFound();
            }

            db.Delays.Remove(delay);
            db.SaveChanges();

            return Ok(delay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DelayExists(int id)
        {
            return db.Delays.Count(e => e.EventID == id) > 0;
        }
    }
}