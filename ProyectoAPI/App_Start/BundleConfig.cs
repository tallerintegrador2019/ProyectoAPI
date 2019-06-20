using System.Web;
using System.Web.Optimization;

namespace ProyectoAPI
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Scripts/node_modules/materialize-css/dist/css/materialize.min.css",
                      "~/Scripts/node_modules/materialize-css/dist/css/materialize.css",
                      "~/Scripts/node_modules/materialize-css/dist/css/style.css"));

            bundles.Add(new StyleBundle("~/bundles/materializer").Include(
                "~/Scripts/node_modules/materialize-css/dist/css/materialize.min.css",
                "~/Scripts/node_modules/materialize-css/dist/css/materialize.css",
                "~/Scripts/node_modules/materialize-css/dist/css/style.css"
               ));

            //"~/Scripts/node_modules/materialize-css/sass/components/*.scss",
            //    "~/Scripts/node_modules/materialize-css/sass/components/forms/*.scss"
            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                   "~/Scripts/node_modules/materialize-css/dist/js/init.js",
                   "~/Scripts/node_modules/materialize-css/dist/js/materialize.js"));

            bundles.Add(new ScriptBundle("~/bundles/materializerjs").Include(
                      "~/Scripts/node_modules/materialize-css/dist/js/materialize.*",
                      "~/Scripts/node_modules/materialize-css/js/*.js",
                      "~/Scripts/node_modules/materialize-css/*.js"));
            BundleTable.EnableOptimizations = true;

        }
    }
}
