using System.Web.Optimization;

namespace ERPAPD
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Scripts/assets/js/uncompressed/jquery-2.0.3.js",
                     "~/Scripts/toastr.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/assets/js/uncompressed/jquery-ui-1.10.3.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                "~/Scripts/jquery.numeric.js",
                "~/Scripts/jquery.number.js",
                "~/Scripts/jquery.blockUI.js",
                "~/Scripts/assets/js/uncompressed/fuelux/fuelux.spinner.js",
                "~/Scripts/assets/js/uncompressed/date-time/bootstrap-datepicker.js",
                "~/Scripts/assets/js/uncompressed/date-time/bootstrap-timepicker.js",
                "~/Scripts/assets/js/uncompressed/date-time/daterangepicker.js",
                "~/Scripts/assets/js/uncompressed/date-time/moment.js",
                "~/Scripts/assets/js/uncompressed/bootstrap-colorpicker.js",
                "~/Scripts/assets/js/uncompressed/jquery.knob.js",
                "~/Scripts/assets/js/uncompressed/jquery.autosize.js",
                "~/Scripts/assets/js/uncompressed/jquery.inputlimiter.1.3.1.js",
                "~/Scripts/assets/js/uncompressed/jquery.maskedinput.js",
                "~/Scripts/assets/js/uncompressed/bootstrap-tag.js",
                "~/Scripts/assets/js/jquery.form.js",
                //"~/ckeditor/ckeditor.js",
                //"~/ckfinder/ckfinder.js",
                "~/Scripts/autoNumeric.js",
                "~/Scripts/assets/js/uncompressed/bootbox.js",
                "~/Scripts/angular.js",
                "~/Scripts/app.js",
                "~/Scripts/chosen.js",
                "~/Scripts/jquery.datetimepicker.js",
                "~/Scripts/jquery.textchange.js",
                "~/Scripts/assets/js/uncompressed/fullcalendar.js",
                //"~/Scripts/assets/js/uncompressed/jquery.ui.touch-punch.js",
                "~/Scripts/assets/js/uncompressed/jquery.slimscroll.js",
                "~/Scripts/assets/js/uncompressed/jquery.easy-pie-chart.js",
                "~/Scripts/assets/js/uncompressed/jquery.sparkline.js",
                "~/Scripts/assets/js/uncompressed/flot/jquery.flot.js",
                "~/Scripts/assets/js/uncompressed/flot/jquery.flot.pie.js",
                "~/Scripts/assets/js/uncompressed/flot/jquery.flot.resize.js",
                "~/Scripts/assets/js/uncompressed/jquery.gritter.js",
                "~/Scripts/assets/js/uncompressed/chosen.jquery.js",
                "~/Scripts/assets/js/uncompressed/lodash.underscore.js",
                "~/Scripts/assets/js/uncompressed/bootstrap-wysiwyg.js",
                "~/Scripts/assets/js/uncompressed/markdown/markdown.js",
                "~/Scripts/assets/js/jquery.blockui.min.js",
                "~/Scripts/assets/js/jquery.number.js",
                "~/Scripts/assets/js/uncompressed/markdown/bootstrap-markdown.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/setting").Include(
                "~/Scripts/assets/js/uncompressed/ace-extra.js"));

            bundles.Add(new ScriptBundle("~/bundles/ace").Include(
                "~/Scripts/assets/js/uncompressed/ace-elements.js",
                "~/Scripts/assets/js/uncompressed/ace.js", "~/Scripts/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/assets/js/uncompressed/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
               "~/Scripts/layout.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
               "~/ckeditor/lang/en.js",
               "~/ckeditor/config.js"
               ));

            bundles.IgnoreList.Clear();

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            //bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
            //    "~/Scripts/assets/css/uncompressed/bootstrap.css",
            //    "~/Scripts/assets/css/uncompressed/bootstrap-responsive.css",
            //    "~/Scripts/assets/css/uncompressed/font-awesome.css"));

            //bundles.Add(new StyleBundle("~/Content/font").Include(
            //    "~/Scripts/assets/css/ace-fonts.css"));

            bundles.Add(new StyleBundle("~/Content/template").Include(
                "~/Scripts/assets/css/uncompressed/jquery-ui-1.10.3.custom.css",
                "~/Scripts/assets/css/chosen.css",
                "~/Scripts/assets/css/datepicker.css",
                "~/Scripts/assets/css/bootstrap-timepicker.css",
                "~/Scripts/assets/css/daterangepicker.css",
                "~/Scripts/assets/css/colorpicker.css",
                "~/Scripts/assets/css/jquery.gritter.css",
                "~/Scripts/assets/css/toastr.css",
                "~/Scripts/assets/css/fullcalendar.css",
                "~/Scripts/jquery.datetimepicker.css"));

            bundles.Add(new StyleBundle("~/Content/ace").Include(
                "~/Scripts/assets/css/uncompressed/ace.css",
                "~/Scripts/assets/css/uncompressed/ace-responsive.css",
                "~/Scripts/assets/css/uncompressed/ace-skins.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                      "~/Scripts/kendo/2014.3.1411/jszip.min.js",
                      "~/Scripts/kendo/2014.3.1411/kendo.all.min.js",
                      "~/Scripts/kendo/2014.3.1411/kendo.aspnetmvc.min.js",
                      "~/Scripts/kendo.modernizr.custom.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/2014.3.1411/kendo").Include(
                     "~/Content/kendo/2014.3.1411/kendo.common.min.css",
                     "~/Content/kendo/2014.3.1411/kendo.dataviz.min.css",
                     "~/Content/kendo/2014.3.1411/kendo.dataviz.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/ckeditor").Include(
                     "~/ckeditor/skins/mono/editor.css"
                     ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
