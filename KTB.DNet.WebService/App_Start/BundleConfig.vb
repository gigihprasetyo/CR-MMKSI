﻿Imports System.Web
Imports System.Web.Optimization

Public Class BundleConfig
    ' For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    Public Shared Sub RegisterBundles(ByVal bundles As BundleCollection)
        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                   "~/Scripts/jquery-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryui").Include(
                    "~/Scripts/jquery-ui-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate*"))
        bundles.Add(New ScriptBundle("~/bundles/plugin").Include(
                    "~/Scripts/jsonFormatter/jsonFormatter.js"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"))

        bundles.Add(New StyleBundle("~/Content/css").Include("~/Content/site.css"))

        bundles.Add(New StyleBundle("~/Content/themes/base/css").Include(
                    "~/Content/themes/base/jquery.ui.core.css",
                    "~/Content/themes/base/jquery.ui.button.css",
                    "~/Content/themes/base/jquery.ui.datepicker.css",
                    "~/Content/themes/base/jquery.ui.progressbar.css",
                    "~/Content/themes/base/jquery.ui.theme.css"))

        ' move to inlude scope if want o use this utility
        '"~/Content/themes/base/jquery.ui.resizable.css",
        '"~/Content/themes/base/jquery.ui.selectable.css",
        '"~/Content/themes/base/jquery.ui.accordion.css",
        '"~/Content/themes/base/jquery.ui.autocomplete.css",
        '"~/Content/themes/base/jquery.ui.dialog.css",
        '"~/Content/themes/base/jquery.ui.slider.css",
        '"~/Content/themes/base/jquery.ui.tabs.css",


        bundles.Add(New StyleBundle("~/Content/plugin").Include(
                    "~/Content/jsonFormatter/jsonFormatter.css",
                    "~/Content/jsonFormatter/jsonFormatter-darkTheme.css"))
    End Sub
End Class