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
    public class ClassEventsController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ClassEvents/5
        public ClassEventsDTO Get(int id)
        {
            ClassEventsDTO cedto = new ClassEventsDTO();

            var delays = db.Delays.Where(x => x.ClassID == id).ToList();

            var @class = db.Classes.Find(id);
            if (@class == null)
            {
                return cedto;
            }
            //var absences = db.Absences.Where(x => x.ConcernedClasses.Contains(@class)).ToList();

            var absences = (from a in db.Absences
                            join ac in db.AbsenceClasses on a.EventID equals ac.EventID
                            where ac.ClassID == id
                            select a).ToList();

            foreach (Delay delay in delays.Where(x => x.User.IsStudent))
            {
                cedto.Delays.Add(
                    new DelayDTO
                    {
                        PersonFullName = delay.User.DisplayFullName,
                        Reason = delay.Reason,
                        Value = delay.Value,
                        ValueUnit = delay.ValueUnit
                    }
                    );
            }

            foreach (Absence absence in absences.Where(x => x.User.IsStudent))
            {
                cedto.Absences.Add(
                    new AbsenceDTO
                    {
                        PersonFullName = absence.User.DisplayFullName,
                        Reason = absence.Reason
                    }
                    );
            }

            cedto.CalculateNbs();
            return cedto;

        }
    }
}
