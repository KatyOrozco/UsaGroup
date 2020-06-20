using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TraigoApp.Controllers
{
    public class TravelerController : Controller
    {
        // GET: Traveler
        public ActionResult Index()
        {
            return View();
        }
    }
}