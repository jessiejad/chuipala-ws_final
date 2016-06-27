using chuipala_ws.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace chuipala_ws.Controllers
{
    [Authorize]
    public class UpdateAbsenceController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: api/UpdateAbsence
        public void Post(JObject data)
        {
            if(data == null)
            {
                return;
            }

            int id = data["id"].ToObject<int>();
            DateTime beginUNI = data["begin"].ToObject<DateTime>();
            DateTime endUNI = data["end"].ToObject<DateTime>();
            string reason = data["reason"].ToString();

            string zoneId = "Romance Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);

            DateTime begin = TimeZoneInfo.ConvertTime(beginUNI, zone);
            DateTime end = TimeZoneInfo.ConvertTime(endUNI, zone);

            var UserID = User.Identity.GetUserId().ToString();
            //var UserID = "726efadb-0295-4e5d-863c-1e9ff5b304a4";

            var absence = db.Absences.Find(id);

            if (absence == null)
            {
                return;
            }

            if(absence.StartDateTime == begin && absence.StopDateTime == end)
            {
                absence.Reason = reason;
                db.Entry(absence).State = EntityState.Modified;
                db.SaveChanges();
                return;
            }
            
            TimeSpan value = end - begin;
            if (value.Days <= 0)
            {
                absence.Value = value.Hours;
                absence.ValueUnit = "h";
            }
            else
            {
                absence.Value = value.Days;
                absence.ValueUnit = "j";
            }
            
            IEnumerable<Class> concernedClasses;
            // Penser à faire en fonction des cours auquel user participe
            var user = db.Users.Find(UserID);

            if (user == null)
            {
                return;
            }
            
            db.AbsenceClasses.RemoveRange(absence.AbsenceClasses);

            if (user.IsProfessor)
            {
                concernedClasses = (from @class in db.Classes
                                    where @class.ProfessorID == user.Id
                                    where (@class.StartDateTime.CompareTo(absence.StartDateTime) >= 0 && @class.StartDateTime.CompareTo(absence.StopDateTime) <= 0)
                                    select @class).ToList();
            }
            else
            {
                concernedClasses = (from @class in db.Classes
                                    join gc in db.GroupClasses on @class.ClassID equals gc.ClassID
                                    join @group in db.Groups on gc.GroupID equals @group.GroupID
                                    join gs in db.GroupStudents on @group.GroupID equals gs.GroupID
                                    where (gs.StudentID == user.Id)
                                    where (@class.StartDateTime.CompareTo(absence.StartDateTime) >= 0 && @class.StartDateTime.CompareTo(absence.StopDateTime) <= 0)
                                    select @class).ToList();
            }
            
            foreach (Class @class in concernedClasses)
            {
                db.AbsenceClasses.Add(new AbsenceClasses { ClassID = @class.ClassID, EventID = absence.EventID });
            };

            absence.StartDateTime = begin;
            absence.StopDateTime = end;
            absence.Reason = reason;
            db.Entry(absence).State = EntityState.Modified;
            db.SaveChanges();
            
        }
    }
}
