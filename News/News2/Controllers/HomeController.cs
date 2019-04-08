using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using News2.Models;

namespace News2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JournalistRedirect()
        {
            ViewBag.Message = "Journalist View";

            return View();
        }

        public ActionResult Documentation()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Forum()
        {
            return View();
        }

        public ActionResult Book()
        {
            return View();
        }

        public ActionResult GoogleMap()
        {
            return View();
        }
    }
}