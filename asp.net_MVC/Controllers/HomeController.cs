using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp.net_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
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

        public ActionResult Advise()
        {
            ViewBag.Message = "Your advise page.";

            return View();
        }

        public ActionResult Find()
        {
            ViewBag.Message = "Find a pet page.";

            return View();
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Report a missing pet page.";

            return View();
        }
    }
}