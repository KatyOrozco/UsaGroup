using System.Web;
using System.Web.Optimization;

namespace TraigoApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
          
            bundles.Add(new ScriptBundle("~/bundles/traigojs").Include(
                       "~/Scripts/TraigoJs/js/lib/jquery/jquery-3.2.1.min.js",
                       //"~/Scripts/TraigoJs/Admin/comun.js",
                       "~/Scripts/TraigoJs/js/lib/popper/popper.min.js",
                       "~/Scripts/TraigoJs/js/lib/tether/tether.min.js",
                       "~/Scripts/TraigoJs/js/lib/bootstrap/bootstrap.min.js",
                       "~/Scripts/TraigoJs/js/plugins.js",
                       "~/Scripts/TraigoJs/js/lib/jqueryui/jquery-ui.min.js",
                       "~/Scripts/TraigoJs/js/lib/lobipanel/lobipanel.min.js",
                       "~/Scripts/TraigoJs/js/lib/match-height/jquery.matchHeight.min.js",
                       //"~/Scripts/TraigoJs/Admin/cotizacion.js",
                       //"~/Scripts/TraigoJs/Admin/historial.js",
                       //"~/Scripts/TraigoJs/Admin/viajero.js",
                       //"~/Scripts/TraigoJs/Admin/viajeactivo.js",
                       "~/Scripts/js-cookie/js.cookie.js",
                       "~/Scripts/TraigoJs/js/app.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/traigojs/admin").Include(
                       "~/Scripts/TraigoJs/Admin/comun.js",
                       "~/Scripts/TraigoJs/Admin/cotizacion.js",
                       "~/Scripts/TraigoJs/Admin/historial.js",
                       "~/Scripts/TraigoJs/Admin/viajero.js",
                       "~/Scripts/TraigoJs/Admin/viajeactivo.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/traigojs/traveler").Include(
                       "~/Scripts/TraigoJs/Traveler/comun.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/traigojs/dashboard").Include(
                        "~/Scripts/TraigoJs/Dashboard/comun.js",
                        "~/Scripts/TraigoJs/Dashboard/index.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/TraigoTemplate/files").Include(
                    "~/Content/TraigoTemplate/css/lib/lobipanel/lobipanel.min.css",
                    "~/Content/TraigoTemplate/css/separate/vendor/lobipanel.min.css",
                    "~/Content/TraigoTemplate/css/lib/jqueryui/jquery-ui.min.css",
                    "~/Content/TraigoTemplate/css/separate/pages/widgets.min.css",
                    "~/Content/font-awesome.min.css",
                    "~/Content/TraigoTemplate/css/lib/bootstrap/bootstrap.min.css",
                    "~/Content/TraigoTemplate/css/separate/pages/widgets.min.css",
                    "~/Content/TraigoTemplate/css/Comun.css",
                    "~/Content/TraigoTemplate/css/main.css"));

              bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/TraigoTemplate/css/main.css",
                    "~/Content/site.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}