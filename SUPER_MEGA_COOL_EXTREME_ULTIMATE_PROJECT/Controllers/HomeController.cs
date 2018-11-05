using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace SUPER_MEGA_COOL_EXTREME_ULTIMATE_PROJECT.Controllers
{
    public class HomeController : Controller
    {
        List<SUPER_MEGA_COOL_EXTREME_ULTIMATE_PROJECT.Models.Poem> /*ochen' lol*/ Poems = new List<Models.Poem>();

        public ActionResult Index()
        {
            // tipo
            ViewBag.Poem = Models.Mein_net_<string>.Start();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            string gitVersion = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "version.txt"));
            ViewBag.Version = gitVersion;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}