using System.Web;
using System.Web.Optimization;

namespace Reporting
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.bootstrap.wizard.min.js",
                      "~/Scripts/jquery-ui-timepicker-addon.js",
                      "~/Scripts/chosen.jquery.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/jquery-ui.structure.min.css",
                      "~/Content/jquery-ui.theme.min.css",
                      "~/Content/jquery-ui-timepicker-addon.css",
                      "~/Content/gridmvc.css",
                      "~/Content/chosen.css"));

            //_Layout global scrpts and css files
            bundles.Add(new ScriptBundle("~/bundles/scripts/global").Include(
                "~/Content/assets/plugins/jquery/jquery-2.2.0.min.js",
                "~/Content/assets/plugins/materialize/js/materialize.min.js",
                "~/Content/assets/plugins/material-preloader/js/materialPreloader.min.js",
                "~/Content/assets/plugins/jquery-blockui/jquery.blockui.js",
                "~/Content/assets/plugins/google-code-prettify/prettify.js",
                "~/Content/assets/js/alpha.min.js",
                "~/Content/assets/plugins/sweetalert/sweetalert.min.js",
                "~/Content/assets/plugins/datatables/js/jquery.dataTables.min.js",
                "~/Content/assets/js/pages/table-data.js",
                "~/Content/assets/js/pages/dialogs.js"
                ));

            //_Layout global css style file
            bundles.Add(new StyleBundle("~/bundles/css/global").Include(
                "~/Content/assets/plugins/materialize/css/materialize.min.css",
                "~/Content/assets/css/alpha.min.css",
                "~/Content/assets/css/custom.css",
                "~/Content/assets/plugins/material-preloader/css/materialPreloader.min.css",
                "~/Content/assets/plugins/metrojs/MetroJs.min.css",
                "~/Content/assets/plugins/weather-icons-master/css/weather-icons.min.css",
                "~/Content/assets/plugins/sweetalert/sweetalert.css",
                "~/Content/fontFamily.css"
                ));

        }
    }
}
