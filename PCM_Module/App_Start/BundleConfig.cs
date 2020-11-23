using System.Web;
using System.Web.Optimization;

namespace PCM_Module
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery-ui-timepicker-addon.js",
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.bootstrap.wizard.min.js",
                      "~/Scripts/bootstrap-multiselect.js",
                      "~/Scripts/jquery.mloading.js",
                      "~/Scripts/jquery.numeric.js",
                      "~/Scripts/moment.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/jquery-ui.structure.min.css",
                      "~/Content/jquery-ui.theme.min.css",
                      "~/Content/jquery-ui-timepicker-addon.css",
                      "~/Content/jquery.mloading.css",
                      "~/Content/gridmvc.css"));
        }
    }
}
