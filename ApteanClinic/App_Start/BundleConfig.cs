using System.Web;
using System.Web.Optimization;

namespace ApteanClinic
{
    public class BundleConfig
    {
      
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.color-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.color.svg-names-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
               "~/Scripts/MainScript.js",
               "~/Scripts/TableJavaScript.js",
               "~/Scripts/dataTables.min.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/HomePageStyle.css",
                      "~/Content/BootstrapView.css",
                      "~/Content/dataTables.min.css",
                      "~/Content/jquerry-ui.css"
                      ));

           
        }
    }
}

