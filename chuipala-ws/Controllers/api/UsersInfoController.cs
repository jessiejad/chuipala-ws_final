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
    public class UsersInfoController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DaysClasses
        public UserDTO Get()
        {
            // En rapport avec l'user qui demande
            var UserID = User.Identity.GetUserId().ToString();
            //var UserID = "4e5eb817-f51a-4c62-a5fe-650c08032cb2";
            
            var user = db.Users.Find(UserID);

            if (user == null)
            {
                return null;
            }

            return new UserDTO
            {
                Name = user.Name,
                FirstName = user.FirstName,
                IsProfessor = user.IsProfessor,
                IsStudent = user.IsStudent
            };

        }
    }
}
