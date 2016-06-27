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
    public class LastDelaysController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/LastDelays
        public IEnumerable<DelayDTO> Get()
        {
            var UserID = User.Identity.GetUserId().ToString();
            //var UserID = "726efadb-0295-4e5d-863c-1e9ff5b304a4";


            // Date format : ToString("dd/MM/yyy")
            // Heure format : ToString("H:mm")
            List<DelayDTO> cedto = new List<DelayDTO>();

            var user = db.Users.Find(UserID);

            if(user == null)
            {
                return cedto;
            }

            var delays = db.Delays.Where(x => x.UserID == UserID).OrderByDescending(x => x.EventID).Take(15);

            foreach(Delay delay in delays)
            {
                cedto.Add(new DelayDTO
                {
                    Reason = delay.Reason,
                    Value = delay.Value,
                    ValueUnit = delay.ValueUnit,
                    EventID = delay.EventID,
                    ConcernedDate = delay.ConcernedDate()
                });
            }
            return cedto;

        }
    }
}
