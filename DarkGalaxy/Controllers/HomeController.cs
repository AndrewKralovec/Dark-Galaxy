using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DarkDarkGalaxy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
         public ActionResult Calendar()
        {
           return View();// the controll is going to call the view 
        }
        public ActionResult Schedule() // Visual studio does not want to add, cant fix 
        {
            return View();
        }
        public ActionResult Database()
        {
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