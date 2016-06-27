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
    public class FullAbsencesInfoController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FullAbsencesInfo
        public FullAbsenceDTO Get(int id)
        {
            // Date format : ToString("dd/MM/yyy")
            // Heure format : ToString("H:mm")
            
            var absence = db.Absences.Find(id);

            if(absence == null)
            {
                return null;
            }
            
            FullAbsenceDTO fullAbsence = new FullAbsenceDTO
            {
                Reason = absence.Reason,
                StartDate = absence.StartDateTime.ToString("dd/MM/yyy"),
                StartTime = absence.StartDateTime.ToString("H:mm"),
                StopDate = absence.StopDateTime.ToString("dd/MM/yyy"),
                StopTime = absence.StopDateTime.ToString("H:mm"),
                EventID = absence.EventID
            };

            foreach(Class @class in absence.ConcernedClasses)
            {
                fullAbsence.ConcernedClasses.Add(new ClassDTO
                {
                    ClassID = @class.ClassID,
                    StartDate = @class.StartDateTime.ToString("dd/MM/yyy"),
                    EndDate = @class.EndDateTime.ToString("dd/MM/yyy"),
                    StartTime = @class.StartDateTime.ToString("H:mm"),
                    EndTime = @class.EndDateTime.ToString("H:mm"),
                    SubjectLabel = @class.SubjectLabel,
                    ProfessorFullName = @class.ProfessorIdentity,
                    Groups = @class.GroupsNames
                });
            }
            
            return fullAbsence;

        }
    }
}
