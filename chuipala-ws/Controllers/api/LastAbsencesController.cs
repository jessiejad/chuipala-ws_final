using chuipala_ws.Models;
using Microsoft.AspNet.Identity;
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
    public class LastAbsencesController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/LastAbsences
        public IEnumerable<AbsenceDTO> Get()
        {
            var UserID = User.Identity.GetUserId().ToString();
            //var UserID = "726efadb-0295-4e5d-863c-1e9ff5b304a4";


            // Date format : ToString("dd/MM/yyy")
            // Heure format : ToString("H:mm")
            List<AbsenceDTO> cedto = new List<AbsenceDTO>();

            var user = db.Users.Find(UserID);

            if(user == null)
            {
                return cedto;
            }

            var absences = db.Absences.Where(x => x.UserID == UserID).OrderByDescending(x => x.EventID).Take(15);

            foreach(Absence absence in absences)
            {
                cedto.Add(new AbsenceDTO
                {
                    Reason = absence.Reason,
                    StartDate = absence.StartDateTime.ToString("dd/MM/yyy"),
                    StartTime = absence.StartDateTime.ToString("H:mm"),
                    StopDate = absence.StopDateTime.ToString("dd/MM/yyy"),
                    StopTime = absence.StopDateTime.ToString("H:mm"),
                    EventID = absence.EventID
                });
            }
            return cedto;

        }
    }
}
