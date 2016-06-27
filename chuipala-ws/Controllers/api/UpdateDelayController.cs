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
    public class UpdateDelayController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: api/UpdateDelay
        public void Post(JObject data)
        {
            if(data == null)
            {
                return;
            }

            int id = data["id"].ToObject<int>();
            DateTime arrivalUNI = data["arrival"].ToObject<DateTime>();
            string reason = data["reason"].ToString();

            string zoneId = "Romance Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime arrival = TimeZoneInfo.ConvertTime(arrivalUNI, zone);

            var UserID = User.Identity.GetUserId().ToString();
            //var UserID = "726efadb-0295-4e5d-863c-1e9ff5b304a4";

            var delay = db.Delays.Find(id);

            if (delay == null)
            {
                return;
            }
            
            if (delay.Class != null)
            {
                int v;
                string vUnit;

                TimeSpan value = arrival - delay.Class.StartDateTime;
                if (value.Days <= 0)
                {
                    if (value.Hours <= 0)
                    {
                        v = value.Minutes;
                        vUnit = "m";
                    }
                    else
                    {
                        v = value.Hours;
                        vUnit = "h";
                    }
                } else
                {
                    v = value.Days;
                    vUnit = "j";
                }
                

                if(delay.Value == v && delay.ValueUnit == vUnit)
                {
                    delay.Reason = reason;
                    db.Entry(delay).State = EntityState.Modified;
                    db.SaveChanges();
                    return;
                }
            }


            int concernedClasses;
            // Penser à faire en fonction des cours auquel user participe
            var user = db.Users.Find(UserID);

            if (user == null)
            {
                return;
            }

            if (user.IsProfessor)
            {
                concernedClasses = (from c in db.Classes
                                    where c.ProfessorID == user.Id
                                    where (c.StartDateTime.CompareTo(arrival) <= 0 && c.EndDateTime.CompareTo(arrival) >= 0)
                                    select c.ClassID).First();
            }
            else
            {
                concernedClasses = (from c in db.Classes
                                    join gc in db.GroupClasses on c.ClassID equals gc.ClassID
                                    join @group in db.Groups on gc.GroupID equals @group.GroupID
                                    join gs in db.GroupStudents on @group.GroupID equals gs.GroupID
                                    where (gs.StudentID == user.Id)
                                    where (c.StartDateTime.CompareTo(arrival) <= 0 && c.EndDateTime.CompareTo(arrival) >= 0)
                                    select c.ClassID).First();
            }

            var @class = db.Classes.Find(concernedClasses);

            if (@class == null)
            {
                return;
            }

            int v2;
            string vUnit2;
            TimeSpan value2 = arrival - @class.StartDateTime;
            if (value2.Hours <= 0)
            {
                v2 = value2.Minutes;
                vUnit2 = "m";
            }
            else
            {
                v2 = value2.Hours;
                vUnit2 = "h";
            }

            delay.Value = v2;
            delay.ValueUnit = vUnit2;
            delay.Reason = reason;
            delay.ClassID = @class.ClassID;
            db.Entry(delay).State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
