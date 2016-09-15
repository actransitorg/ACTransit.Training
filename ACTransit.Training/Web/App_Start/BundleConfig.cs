using System.Web.Optimization;

namespace ACTransit.Training.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
        

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.min-{version}.js",
                        "~/Scripts/jquery.datetimepicker-2.4.3.js",
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/sammy-{version}.js",                        
                        "~/Scripts/AjaxHelper.js",
                        "~/Scripts/modalBox-1.0.0.js",
                        "~/Scripts/multiCheckCombo-1.0.1.js",
                        "~/Scripts/dateTimeExtender-1.0.0.js",
                        "~/Scripts/numeric-1.0.0.js",
                        "~/Scripts/General.js",
                        "~/Scripts/site*",
                        "~/Scripts/Pages.js",
                        "~/Scripts/Pages/*.js"
                        ));

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
                      "~/Content/site.css",
                      "~/Content/navbar.css",
                      "~/Content/Media768.css",
                      "~/Content/Media480.css",
                      "~/Content/modelBox.css",
                      "~/Content/Validation.css",
                      "~/Content/multiCheckCombo.css",
                      "~/Content/Grid*",
                      "~/Content/jquery*"
                      ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                   "~/Content/themes/base/core.css",
                    "~/Content/themes/base/accordion.css",
                    "~/Content/themes/base/autocomplete.css",
                    "~/Content/themes/base/button.css",
                    "~/Content/themes/base/datepicker.css",
                    "~/Content/themes/base/dialog.css",
                    "~/Content/themes/base/draggable.css",
                    "~/Content/themes/base/menu.css",
                    "~/Content/themes/base/progressbar.css",
                    "~/Content/themes/base/resizable.css",
                    "~/Content/themes/base/selectable.css",
                    "~/Content/themes/base/selectmenu.css",
                    "~/Content/themes/base/sortable.css",
                    "~/Content/themes/base/slider.css",
                    "~/Content/themes/base/spinner.css",
                    "~/Content/themes/base/tabs.css",
                    "~/Content/themes/base/tooltip.css",
                   "~/Content/themes/base/theme.css"));


#if DEBUG
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;

#else
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;

#endif

        }
    }
}
