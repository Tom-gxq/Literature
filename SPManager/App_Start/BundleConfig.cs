using System.Web;
using System.Web.Optimization;

namespace SPManager
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/plug/jquery/jquery-1.11.1.js",
                     "~/Scripts/plug/jquery/jquery-ui-1.10.2.js",
                     "~/Scripts/global.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/lib-min.js",
                      "~/Scripts/jquery.jbox-min.js",
                      "~/Scripts/common_login_reg.js",
                      "~/Scripts/component-min.js",
                      "~/Scripts/login.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/component-min.css",
                      "~/Content/jbox/jbox-min.css",
                      "~/Content/common_login_reg.css",
                      "~/Content/style.css"));
        }
    }
}
