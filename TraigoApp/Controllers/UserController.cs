using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraigoApp.Models;
using PagedList;


namespace TraigoApp.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        public UserController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
           
                return View("~/Views/User/Login.cshtml");
          
        }

        // GET: Login
        public ActionResult Login()
        {
           
                return View();
            
        }


        //
        public ActionResult ResetPassword()
        {
            return View();
        }
        public ActionResult Cotizacion()
        {
            
                IList<Category> categorias = (from i in _context.Category
                                              orderby i.Nombre
                                              select i).ToList<Category>();

                IList<City> ciudades = (from i in _context.City
                                        orderby i.Id
                                        select i).ToList<City>();

                ViewData.Add("categorias", categorias);
                ViewData.Add("ciudades", ciudades);
                return View();

        }
        public ActionResult Travelers()
        {
            
                DateTime hoy = DateTime.Now;
                IList<Trip> viajesActivos = (from i in _context.Trip
                                             where i.FechaHasta > hoy
                                             orderby i.FechaHasta ascending
                                             select i).ToList<Trip>();

                ViewData.Add("viajesActivos", viajesActivos);
                return View();
            

        }

        public ActionResult Historial(string sortOrder, string currentFilter, string searchString, int? page)
        {
         
                ViewBag.CurrentSort = sortOrder;
                ViewBag.EstadoSortParm = String.IsNullOrEmpty(sortOrder) ? "estado_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var cotizaciones = from i in _context.Quotation
                                   select i;

                //realizo la busqueda de email y de nombre
                if (!String.IsNullOrEmpty(searchString))
                {
                    //cotizaciones = (from i in _context.Quotation
                    //                where i.User.Nombre.Contains(keyword) || i.User.Email.Contains(keyword)
                    //                orderby i.FechaCotizacion descending
                    //                select i).ToList<Quotation>();
                    cotizaciones = cotizaciones.Where(i => i.User.Nombre.Contains(searchString) || i.User.Email.Contains(searchString) || i.QuotationStatus.Nombre.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "estado_desc":
                        cotizaciones = cotizaciones.OrderByDescending(s => s.QuotationStatus.Nombre);
                        break;
                    case "Date":
                        cotizaciones = cotizaciones.OrderBy(s => s.Trip.Vuelta);
                        break;
                    case "date_desc":
                        cotizaciones = cotizaciones.OrderByDescending(s => s.Trip.Vuelta);
                        break;
                    default:  // Name ascending 
                        cotizaciones = cotizaciones.OrderByDescending(s => s.FechaCotizacion);
                        break;
                }

                //ViewData.Add("cotizaciones", cotizaciones);

                DateTime hoy = DateTime.Now;
                IList<Trip> viajesActivos = (from i in _context.Trip
                                             where i.FechaHasta > hoy
                                             orderby i.FechaHasta descending
                                             select i).ToList<Trip>();

                ViewData.Add("viajesActivos", viajesActivos);

                IList<QuotationStatus> estadoCotizaciones = (from i in _context.QuotationStatus
                                                             orderby i.Id ascending
                                                             select i).ToList<QuotationStatus>();

                ViewData.Add("estadoCotizaciones", estadoCotizaciones);
                int pageSize = 30;
                int pageNumber = (page ?? 1);
                return View(cotizaciones.ToPagedList(pageNumber, pageSize));
        }

 

        //página de cambio de contraseña
        public ActionResult Changepassword() {
            return View();
        }

        // Regsiter
        public ActionResult SignUp()
        {
            IList<City> ciudades = (from i in _context.City
                                    orderby i.Nombre
                                    select i).ToList<City>();
            ViewData.Add("ciudades", ciudades);
            return View();
        }
    }
}