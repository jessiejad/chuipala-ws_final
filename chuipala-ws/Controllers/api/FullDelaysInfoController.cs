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
    public class FullDelaysInfoController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FullDelaysInfo
        public FullDelayDTO Get(int id)
        {
            // Date format : ToString("dd/MM/yyy")
            // Heure format : ToString("H:mm")
            
            var delay = db.Delays.Find(id);

            if(delay == null)
            {
                return null;
            }
            
            return new FullDelayDTO
            {
                Reason = delay.Reason,
                Value = delay.Value,
                ValueUnit = delay.ValueUnit,
                EventID = delay.EventID,
                ConcernedDate = delay.ConcernedDate(),
                ConcernedClass = new ClassDTO
                {
                    ClassID = delay.Class.ClassID,
                    StartDate = delay.Class.StartDateTime.ToString("dd/MM/yyy"),
                    EndDate = delay.Class.EndDateTime.ToString("dd/MM/yyy"),
                    StartTime = delay.Class.StartDateTime.ToString("H:mm"),
                    EndTime = delay.Class.EndDateTime.ToString("H:mm"),
                    SubjectLabel = delay.Class.SubjectLabel,
                    ProfessorFullName = delay.Class.ProfessorIdentity,
                    Groups = delay.Class.GroupsNames
                }
            };

        }
    }
}
