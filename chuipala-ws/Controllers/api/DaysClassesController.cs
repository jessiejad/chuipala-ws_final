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
    public class DaysClassesController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DaysClasses
        public IEnumerable<ClassDTO> Get()
        {
            // En rapport avec l'user qui demande
            var UserID = User.Identity.GetUserId().ToString();
            //var UserID = "4e5eb817-f51a-4c62-a5fe-650c08032cb2";

            List<ClassDTO> ldto = new List<ClassDTO>();
            
            var user = db.Users.Find(UserID);

            if (user == null)
            {
                return ldto;
            }

            IEnumerable<Class> classes;

            if (user.IsProfessor)
            {
                //classes = db.Classes.Where(x => x.ProfessorID == UserID);
                classes = (from @class in db.Classes
                           where @class.ProfessorID == user.Id
                           select @class).ToList().OrderBy(x => x.StartDateTime);
            } else
            {
                classes = (from @class in db.Classes
                           join gc in db.GroupClasses on @class.ClassID equals gc.ClassID
                           join @group in db.Groups on gc.GroupID equals @group.GroupID
                           join gs in db.GroupStudents on @group.GroupID equals gs.GroupID
                           where (gs.StudentID == user.Id)
                           select @class).ToList().OrderBy(x => x.StartDateTime);
            }

            foreach (Class @class in classes.Where(x => x.StartDateTime.Date == DateTime.Now.Date))
            {
                /*
                if(@class.StartDateTime.Date != DateTime.Now.Date)
                {
                    continue;
                }*/
                var abs = db.AbsenceClasses.Where(x => x.ClassID == @class.ClassID).ToList();
                var rts = db.Delays.Where(x => x.ClassID == @class.ClassID).ToList();
                var nbAbsences =  abs.Where(x => x.Absence.User.IsStudent).Count();
                var nbDelays = rts.Where(x => x.User.IsStudent).Count();

                var isProfessorAbsent = db.AbsenceClasses.Where(x => x.ClassID == @class.ClassID && x.Absence.UserID == @class.ProfessorID).Any();
                var isProfessorLate = db.Delays.Where(x => x.ClassID == @class.ClassID && x.UserID == @class.ProfessorID).Any();

                var isUserAbsent = db.AbsenceClasses.Where(x => x.ClassID == @class.ClassID && x.Absence.UserID == user.Id).Any();
                var isUserLate = db.Delays.Where(x => x.ClassID == @class.ClassID && x.UserID == user.Id).Any();

                ldto.Add(new ClassDTO {
                    ClassID = @class.ClassID,
                    PreSetDelayDate = @class.StartDateTime.AddMinutes(15),
                    FullStartDate = @class.StartDateTime,
                    FullEndDate = @class.EndDateTime,
                    StartTime = @class.StartDateTime.ToString("H:mm"),
                    EndTime = @class.EndDateTime.ToString("H:mm"),
                    SubjectLabel = @class.SubjectLabel,
                    ProfessorFullName = @class.ProfessorIdentity,
                    NbAbsences = nbAbsences,
                    NbDelays = nbDelays,
                    IsProfessorAbsent = isProfessorAbsent,
                    IsProfessorLate = isProfessorLate,
                    IsUserAbsent = isUserAbsent,
                    IsUserLate = isUserLate
                });
            }
           
            return ldto;

        }
    }
}
