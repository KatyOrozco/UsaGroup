using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TraigoApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //login path
            routes.MapRoute(
               name: "Login",
               url: "login",
               defaults: new { controller = "Admin", action = "Login" }
           );



            //reset password path
            routes.MapRoute(
               name: "reset-password",
               url: "resetpassword",
               defaults: new { controller = "User", action = "resetpassword" }
           );
            routes.MapRoute(
          name: "cotizar",
          url: "cotizar",
          defaults: new { controller = "User", action = "Cotizacion" }
      );

            //sign up path
            routes.MapRoute(
               name: "Register",
               url: "signup",
               defaults: new { controller = "User", action = "SignUp" }
           );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
