using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace chuipala_ws.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Creating Role we need and adding it to User
            //var b = Roles.RoleExists("Admin");
            /*Roles.CreateRole("Admin");
            Roles.CreateRole("Student");
            Roles.CreateRole("Professor");*/
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}