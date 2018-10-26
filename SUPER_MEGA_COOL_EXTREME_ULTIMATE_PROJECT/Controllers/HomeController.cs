using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SUPER_MEGA_COOL_EXTREME_ULTIMATE_PROJECT.Controllers
{
    public class HomeController : Controller
    {
        List<SUPER_MEGA_COOL_EXTREME_ULTIMATE_PROJECT.Models.Poem> /*ochen' lol*/ Poems = new List<Models.Poem>();

        public ActionResult Index()
        {
            ViewBag.Test1 = "This is a test message that demostrates capabilites of a ViewBag.";
            ViewBag.Test0 = "This shouldn't exist..";
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