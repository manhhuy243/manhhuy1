using System.Web;
using System.Web.Optimization;

namespace DienMayws
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //--- obaju:
            bundles.Add(new StyleBundle("~/obaju/css").Include(
                      "~/obajuStyle/css/bootstrap.css",
                      "~/obajuStyle/css/animate.min.css",
                      "~/obajuStyle/css/owl.carousel.css",
                      "~/obajuStyle/css/owl.theme.css",
                      "~/obajuStyle/css/style.default.css"

                ));
            bundles.Add(new ScriptBundle("~/obaju/js").Include(
                     "~/obajuStyle/js/jquery.cookie.js",
                     "~/obajuStyle/js/waypoints.min.js",
                     "~/obajuStyle/js/modernizr.js",
                     "~/obajuStyle/js/bootstrap-hover-dropdown.js",
                     "~/obajuStyle/js/owl.carousel.min.js",
                     "~/obajuStyle/js/front.js"
                ));
          //  BundleTable.EnableOptimizations = true;
        }
    }
}
