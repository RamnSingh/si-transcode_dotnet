using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transcode.Controllers
{
    public class StaticController : Controller
    {
        // GET: Static

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Prices()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}