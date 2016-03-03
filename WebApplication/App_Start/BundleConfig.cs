using System.Web;
using System.Web.Optimization;

namespace WebApplication
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*", "~/Scripts/jquery.validate.extensions.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/kendo.all.min.js",
                "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
            //    "~/Scripts/knockout-3.3.0.js",
            //    "~/Scripts/knockout-postbox.js",
            //    "~/Content/Scripts/knockout/knockout.mapping-latest.js",
            //    "~/Content/Scripts/knockout/knockout.validation.min.js",
            //    "~/Content/Scripts/knockout/knockout-server-side-validation.js",
            //    "~/Content/Scripts/knockout/knockout.validators.js",
            //    "~/Content/Scripts/knockout/knockout.binding-handlers.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                "~/Content/kendo/kendo.common.min.css",
                "~/Content/kendo/kendo.default.min.css",
                "~/Content/kendo-castrol.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/Layout.css",
                "~/Content/Site-Bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css")
                .Include("~/Content/themes/base/all.css"));
                        

            bundles.Add(new StyleBundle("~/Content/JqueryAutocomplete/css")
                .Include("~/Content/jquery.autocomplete.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-dialog").Include(
                    "~/Content/bootstrap-dialog.css"
                ));

            bundles.Add(new StyleBundle("~/Content/Notifications/css").Include(
                        "~/Content/Notifications.css",
                        "~/Content/Animate.css"
                ));
        }
    }
}
