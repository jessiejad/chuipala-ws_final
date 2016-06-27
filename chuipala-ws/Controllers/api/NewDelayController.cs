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
    public class NewDelayController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: api/NewDelay
        public void Post(JObject data)
        {

            if(data == null)
            {
                return;
            }
            
            DateTime arrivalUNI = data["arrival"].ToObject<DateTime>();
            string reason = data["reason"].ToString();

            string zoneId = "Romance Standard Time";
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime arrival = TimeZoneInfo.ConvertTime(arrivalUNI, zone);

            var UserID = User.Identity.GetUserId().ToString();
            //var UserID = "726efadb-0295-4e5d-863c-1e9ff5b304a4";

            // Searching for the class concerned


            IEnumerable<int> concernedClasses;
            // Penser à faire en fonction des cours auquel user participe
            var user = db.Users.Find(UserID);

            if(user == null)
            {
                return;
            }

            if (user.IsProfessor)
            {
                concernedClasses = (from c in db.Classes
                                    where c.ProfessorID == user.Id
                                    where (c.StartDateTime.CompareTo(arrival) <= 0 && c.EndDateTime.CompareTo(arrival) >= 0)
                                    select c.ClassID);
            }
            else
            {
                concernedClasses = (from c in db.Classes
                                    join gc in db.GroupClasses on c.ClassID equals gc.ClassID
                                    join @group in db.Groups on gc.GroupID equals @group.GroupID
                                    join gs in db.GroupStudents on @group.GroupID equals gs.GroupID
                                    where (gs.StudentID == user.Id)
                                    where (c.StartDateTime.CompareTo(arrival) <= 0 && c.EndDateTime.CompareTo(arrival) >= 0)
                                    select c.ClassID);
            }
            if(!concernedClasses.Any())
            {
                return;
            }

            var concernedClass = concernedClasses.First();
            var @class = db.Classes.Find(concernedClass);

            if (@class == null)
            {
                return;
            }
            int v;
            string vUnit;
            TimeSpan value = arrival - @class.StartDateTime;
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

            Delay delay = new Delay
            {
                Value = v,
                ValueUnit = vUnit,
                Reason = reason,
                UserID = UserID,
                ClassID = @class.ClassID
            };

            db.Delays.Add(delay);
            db.SaveChanges();
        }
    }
}
