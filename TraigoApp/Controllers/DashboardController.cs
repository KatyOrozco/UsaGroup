using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraigoApp.Models;

namespace TraigoApp.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;
        public DashboardController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            HttpCookie aCookie = Request.Cookies["ua"];

            if (aCookie != null)
            {
                IList<Category> categorias = (from i in _context.Category
                                              orderby i.Nombre
                                              select i).ToList<Category>();
                ViewData.Add("categorias", categorias);
                return View();
            }
            else
            {
                return View("~/Views/User/Login.cshtml");
            }
        }
    }
}