using System.Web;
using System.Web.Optimization;
using System.Web.Optimization.React;

namespace MCC
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/assets/global/plugins/globalTemplate").Include(
                       "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                       "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                       "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                       "~/Content/assets/global/plugins/uniform/css/uniform.default.css",
                       "~/Content/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css",
                       "~/Content/assets/global/plugins/bootstrap-toastr/toastr.min.css",
                       "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                       "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css",
                       "~/Content/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css",
                       "~/Content/assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css",
                       "~/Content/assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css",
                       "~/Content/assets/global/plugins/clockface/css/clockface.css",
                       "~/Content/assets/global/plugins/jquery-multi-select/css/multi-select.css",
                       "~/Content/assets/global/plugins/dropzone/dropzone.min.css",
                       "~/Content/assets/global/plugins/dropzone/basic.min.css",
                       "~/Content/assets/global/plugins/cubeportfolio/css/cubeportfolio.css",
                       "~/Content/assets/global/plugins/select2/css/select2.min.css",
                       "~/Content/assets/global/plugins/select2/css/select2-bootstrap.min.css",
                       "~/Content/assets/global/plugins/bootstrap-editable/bootstrap-editable/css/bootstrap-editable.css",
                       "~/Content/assets/global/plugins/bootstrap-tagsinput/bootstrap-tagsinput.css",
                       "~/Content/assets/global/plugins/typeahead/typeahead.css",
                       "~/Content/assets/global/plugins/icheck/skins/all.css",
                       "~/Content/assets/apps/css/todo-2.min.css",
                       "~/Content/assets/pages/css/about.min.css",
                       "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.css"
                      // "~/Content/Site.css"
                       ));

            bundles.Add(new StyleBundle("~/Content/assets/global/css/themeTemplate").Include(
                      "~/Content/assets/global/css/components.min.css",
                      "~/Content/assets/global/css/plugins.min.css"));

            bundles.Add(new StyleBundle("~/Content/assets/layouts/layout/css/layoutStyleTemplate").Include(
                      "~/Content/assets/layouts/layout/css/layout.min.css",
                      "~/Content/assets/layouts/layout/css/themes/default.min.css",
                      "~/Content/assets/layouts/layout/css/custom.min.css",
                      "~/Content/assets/pages/css/profile.min.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/kendo/2014.3.1411/kendo").Include(
                      "~/Content/kendo/2014.3.1411/kendo.common-bootstrap.min.css",
                      "~/Content/kendo/2014.3.1411/kendo.mobile.all.min.css",
                      "~/Content/kendo/2014.3.1411/kendo.dataviz.min.css",
                      "~/Content/kendo/2014.3.1411/kendo.bootstrap.min.css",
                      "~/Content/kendo/2014.3.1411/kendo.dataviz.default.min.css"));

            bundles.Add(new ScriptBundle("~/Content/assets/global/plugins/corePluginTemplate").Include(
                      // "~/Content/assets/global/plugins/jquery.min.js",
                      "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                      "~/Content/assets/global/plugins/js.cookie.min.js",
                      "~/Content/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                      "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/Content/assets/global/plugins/jquery.blockui.min.js",
                      "~/Content/assets/global/plugins/uniform/jquery.uniform.min.js",
                      "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/Content/assets/global/plugins/pagePluginTemplate").Include(
                      "~/Content/assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js",
                      "~/Content/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                      "~/Content/assets/global/plugins/jquery-validation/js/additional-methods.min.js",
                      "~/Content/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js",
                      "~/Content/assets/global/plugins/bootstrap-toastr/toastr.min.js",
                      "~/Scripts/jquery.form.min.js",
                      "~/Scripts/underscore-min.js",
                      "~/Scripts/autoNumeric-min.js",
                      "~/Scripts/firebase.js",
                      "~/Scripts/reactfire.min.js",
                      "~/Content/assets/global/plugins/bootstrap-daterangepicker/moment.min.js",
                      "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.js",
                      "~/Content/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                      "~/Content/assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js",
                      "~/Content/assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js",
                      "~/Content/assets/global/plugins/clockface/js/clockface.js",
                      "~/Content/assets/global/plugins/bootbox/bootbox.min.js",
                      "~/Content/assets/global/plugins/jquery-multi-select/js/jquery.multi-select.js",
                      "~/Content/assets/global/plugins/dropzone/dropzone.min.js",
                      "~/Content/assets/global/plugins/select2/js/select2.full.min.js",
                      "~/Content/assets/global/plugins/bootstrap-editable/bootstrap-editable/js/bootstrap-editable.js",
                      "~/Content/assets/global/plugins/bootstrap-tagsinput/bootstrap-tagsinput.min.js",
                      "~/Content/assets/global/plugins/typeahead/handlebars.min.js",
                      "~/Content/assets/global/plugins/typeahead/typeahead.bundle.min.js",
                      "~/Content/assets/global/plugins/jquery-easypiechart/jquery.easypiechart.min.js",
                      "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.js",
                      "~/Content/assets/global/plugins/jquery.sparkline.min.js"

                      ));
            bundles.Add(new ScriptBundle("~/Content/assets/global/plugins/pagePlugin/chart").Include(
                        "~/Content/assets/global/plugins/morris/morris.min.js",
                        "~/Content/assets/global/plugins/morris/raphael-min.js",
                        "~/Content/assets/global/plugins/counterup/jquery.waypoints.min.js",
                        "~/Content/assets/global/plugins/counterup/jquery.counterup.min.js",
                        "~/Content/assets/global/plugins/amcharts/amcharts/amcharts.js",
                        "~/Content/assets/global/plugins/amcharts/amcharts/serial.js",
                        "~/Content/assets/global/plugins/amcharts/amcharts/pie.js",
                        "~/Content/assets/global/plugins/amcharts/amcharts/radar.js",
                        "~/Content/assets/global/plugins/amcharts/amcharts/themes/light.js",
                        "~/Content/assets/global/plugins/amcharts/amcharts/themes/patterns.js",
                        "~/Content/assets/global/plugins/amcharts/amcharts/themes/chalk.js",
                        "~/Content/assets/global/plugins/amcharts/ammap/ammap.js",
                        "~/Content/assets/global/plugins/amcharts/ammap/maps/js/worldLow.js",
                        "~/Content/assets/global/plugins/amcharts/amstockcharts/amstock.js",
                        "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.js",
                        "~/Content/assets/global/plugins/flot/jquery.flot.min.js",
                        "~/Content/assets/global/plugins/flot/jquery.flot.resize.min.js",
                        "~/Content/assets/global/plugins/flot/jquery.flot.categories.min.js",
                        "~/Content/assets/global/plugins/jquery-easypiechart/jquery.easypiechart.min.js",
                        "~/Content/assets/global/plugins/jquery.sparkline.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/Content/assets/global/scripts/globalScriptTemplate").Include(
                      "~/Content/assets/global/scripts/app.min.js",
                      "~/Content/assets/pages/scripts/form-dropzone.min.js"));

            //bundles.Add(new ScriptBundle("~/Content/assets/pages/scripts/pagePlugin").Include(
            //          "~/Content/assets/pages/scripts/dashboard.min.js"));

            bundles.Add(new ScriptBundle("~/Content/assets/layouts/themeLayoutTemplate").Include(
                      "~/Content/assets/layouts/layout/scripts/layout.min.js",
                      "~/Content/assets/layouts/layout/scripts/demo.min.js",
                      "~/Content/assets/layouts/global/scripts/quick-sidebar.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/kendo/2014.3.1411/kendo").Include(
                      "~/Scripts/kendo/2014.3.1411/jquery.min.js",
                      "~/Scripts/kendo/2014.3.1411/jszip.min.js",
                      "~/Scripts/kendo/2014.3.1411/kendo.all.min.js",
                      "~/Scripts/kendo/2014.3.1411/kendo.aspnetmvc.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/firebaseclient").Include(
                        "~/Scripts/firebase.client.js"));

            bundles.IgnoreList.Clear();
            BundleTable.EnableOptimizations = true;
        }
    }
}
