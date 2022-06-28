using System.Configuration;
using System.Web.Optimization;

namespace KTB.DNet.Interface.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bool enableMinification = bool.Parse(ConfigurationManager.AppSettings["EnableMinification"]);

            BundleTable.EnableOptimizations = enableMinification;


            #region Javascript bundles

            bundles.Add(new ScriptBundle("~/bundles/js").Include(

            #region Bower Components
                //Bower Components
                "~/bower_components/jquery/dist/jquery.js",
                "~/bower_components/jquery-ui/jquery-ui.js",
                "~/bower_components/jquery.easing/js/jquery.easing.js",
                "~/bower_components/jquery.easy-pie-chart/dist/jquery.easypiechart.js",
                "~/bower_components/chart.js/dist/Chart.js",
                "~/bower_components/amcharts/dist/amcharts/amcharts.js",
                "~/bower_components/amcharts/dist/amcharts/plugins/responsive/responsive.min.js",
                "~/bower_components/amcharts/dist/amcharts/serial.js",
                "~/bower_components/amcharts/dist/amcharts/funnel.js",
                "~/bower_components/amcharts/dist/amcharts/pie.js",
                "~/bower_components/amcharts/dist/amcharts/gantt.js",
                "~/bower_components/amcharts-stock/dist/amcharts/amstock.js",
                "~/bower_components/ammap/dist/ammap/ammap.js",
                "~/bower_components/ammap/dist/ammap/maps/js/worldLow.js",
                "~/bower_components/angular/angular.min.js",
                "~/bower_components/angular-route/angular-route.min.js",
                "~/bower_components/slimScroll/jquery.slimscroll.js",
                "~/bower_components/angular-slimscroll/angular-slimscroll.js",
                "~/bower_components/angular-smart-table/dist/smart-table.js",
                "~/bower_components/angular-toastr/dist/angular-toastr.tpls.js",
                "~/bower_components/angular-touch/angular-touch.js",
                "~/bower_components/angular-ui-sortable/sortable.js",
                "~/bower_components/angular-cookies/angular-cookies.min.js",
                "~/bower_components/bootstrap/js/dropdown.js",
                "~/bower_components/bootstrap-select/dist/js/bootstrap-select.min.js",
                "~/bower_components/bootstrap-switch/dist/js/bootstrap-switch.min.js",
                "~/bower_components/bootstrap-tagsinput/dist/bootstrap-tagsinput.min.js",
                "~/bower_components/moment/moment.js",
                "~/bower_components/fullcalendar/dist/fullcalendar.js",
                "~/bower_components/leaflet/dist/leaflet-src.js",
                "~/bower_components/angular-progress-button-styles/dist/angular-progress-button-styles.min.js",
                "~/bower_components/angular-ui-router/release/angular-ui-router.js",
                "~/bower_components/angular-chart.js/dist/angular-chart.js",
                "~/bower_components/chartist/dist/chartist.min.js",
                "~/bower_components/angular-chartist.js/dist/angular-chartist.js",
                "~/bower_components/eve-raphael/eve.js",
                "~/bower_components/raphael/raphael.min.js",
                "~/bower_components/mocha/mocha.js",
                "~/bower_components/morris.js/morris.js",
                "~/bower_components/angular-morris-chart/src/angular-morris-chart.min.js",
                "~/bower_components/ionrangeslider/js/ion.rangeSlider.js",
                "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.js",
                "~/bower_components/angular-animate/angular-animate.js",
                "~/bower_components/rangy/rangy-core.js",
                "~/bower_components/rangy/rangy-classapplier.js",
                "~/bower_components/rangy/rangy-highlighter.js",
                "~/bower_components/rangy/rangy-selectionsaverestore.js",
                "~/bower_components/rangy/rangy-serializer.js",
                "~/bower_components/rangy/rangy-textrange.js",
                "~/bower_components/textAngular/dist/textAngular.js",
                "~/bower_components/textAngular/dist/textAngular-sanitize.js",
                "~/bower_components/textAngular/dist/textAngularSetup.js",
                "~/bower_components/angular-xeditable/dist/js/xeditable.js",
                "~/bower_components/jstree/dist/jstree.js",
                "~/bower_components/ng-js-tree/dist/ngJsTree.js",
                "~/bower_components/angular-ui-select/dist/select.js",
                "~/bower_components/angular-loading-bar/build/loading-bar.min.js",
                "~/bower_components/angular-moment-picker/dist/angular-moment-picker.min.js",
                "~/bower_components/ng-file-upload/FileAPI.min.js",
                "~/bower_components/ng-file-upload/ng-file-upload-all.min.js",
            #endregion

            #region Themes
                // Themes
                "~/src/app/theme/theme.module.js",
                "~/src/app/theme/components/components.module.js",
                "~/src/app/theme/inputs/inputs.module.js",
                "~/src/app/theme/theme.config.js",
                "~/src/app/theme/theme.configProvider.js",
                "~/src/app/theme/theme.constants.js",
                "~/src/app/theme/theme.run.js",
                "~/src/app/theme/theme.service.js",
                "~/src/app/theme/components/toastrLibConfig.js",
                "~/src/app/theme/directives/animatedChange.js",
                "~/src/app/theme/directives/autoExpand.js",
                "~/src/app/theme/directives/autoFocus.js",
                "~/src/app/theme/directives/includeWithScope.js",
                "~/src/app/theme/directives/ionSlider.js",
                "~/src/app/theme/directives/ngFileSelect.js",
                "~/src/app/theme/directives/scrollPosition.js",
                "~/src/app/theme/directives/trackWidth.js",
                "~/src/app/theme/directives/windowFocus.js",
                "~/src/app/theme/directives/zoomIn.js",
                "~/src/app/theme/services/baProgressModal.js",
                "~/src/app/theme/services/baNotification.js",
                "~/src/app/theme/services/baUtil.js",
                "~/src/app/theme/services/fileReader.js",
                "~/src/app/theme/services/preloader.js",
                "~/src/app/theme/services/stopableInterval.js",
                "~/src/app/theme/components/backTop/backTop.directive.js",
                "~/src/app/theme/components/baPanel/baPanel.directive.js",
                "~/src/app/theme/components/baPanel/baPanel.service.js",
                "~/src/app/theme/components/baPanel/baPanelBlur.directive.js",
                "~/src/app/theme/components/baPanel/baPanelBlurHelper.service.js",
                "~/src/app/theme/components/baPanel/baPanelSelf.directive.js",
                "~/src/app/theme/components/baSidebar/baSidebar.directive.js",
                "~/src/app/theme/components/baSidebar/baSidebar.service.js",
                "~/src/app/theme/components/baSidebar/BaSidebarCtrl.js",
                "~/src/app/theme/components/baSidebar/baSidebarHelpers.directive.js",
                "~/src/app/theme/components/baWizard/baWizard.directive.js",
                "~/src/app/theme/components/baWizard/baWizardCtrl.js",
                "~/src/app/theme/components/baWizard/baWizardStep.directive.js",
                "~/src/app/theme/components/contentTop/contentTop.directive.js",
                "~/src/app/theme/components/msgCenter/msgCenter.directive.js",
                "~/src/app/theme/components/msgCenter/MsgCenterCtrl.js",
                "~/src/app/theme/components/pageTop/pageTop.directive.js",
                "~/src/app/theme/components/progressBarRound/progressBarRound.directive.js",
                "~/src/app/theme/components/widgets/widgets.directive.js",
                "~/src/app/theme/filters/image/appImage.js",
                "~/src/app/theme/filters/image/kameleonImg.js",
                "~/src/app/theme/filters/image/profilePicture.js",
                "~/src/app/theme/filters/text/removeHtml.js",
                "~/src/app/theme/filters/datetime/fromNow.js",
                "~/src/app/theme/inputs/baSwitcher/baSwitcher.js",
                "~/src/app/theme/components/backTop/lib/jquery.backTop.min.js",
                "~/src/app/theme/components/multiSelect/jquery.multi-select.js",
                "~/src/app/theme/components/multiSelect/jquery.quicksearch.js",
                "~/src/app/theme/components/multiSelect/angular.multi-select.js",
            #endregion

            #region App Js
                // App js
                "~/src/app/pages/pages.module.js",
                "~/src/app/app.js",
                "~/src/app/endpoints.js",
            #endregion

            #region Login Js
                // Login js
                "~/src/app/pages/auth/auth.module.js",
                "~/src/app/pages/auth/loginCtrl.js",
            #endregion

            #region Dashboard
                // Dashboard
                "~/src/app/pages/dashboard/dashboard.module.js",
                "~/src/app/pages/dashboard/dashboardCtrl.js",
                "~/src/app/pages/dashboard/failedTransactionSummary/failedTransactionSummary.directive.js",
                "~/src/app/pages/dashboard/transactionSummary/transactionSummary.directive.js",
                //dashboard by Malik
                "~/src/app/pages/dashboard/dashboardMainInfo/dashboardMainInfo.directive.js",
                "~/src/app/pages/dashboard/dashboardMainInfo/DashboardMainInfoCtrl.js",
                "~/src/app/pages/dashboard/dashboardTransactionList/dashboardTransactionList.directive.js",
                "~/src/app/pages/dashboard/dashboardTransactionList/DashboardTransactionListCtrl.js",
                "~/src/app/pages/dashboard/dashboardTopRankedAPI/dashboardTopRankedApi.directive.js",
                "~/src/app/pages/dashboard/dashboardTopRankedAPI/DashboardTopRankedApiCtrl.js",
                "~/src/app/pages/dashboard/dashboardErrorList/dashboardErrorList.directive.js",
                "~/src/app/pages/dashboard/dashboardErrorList/DashboardErrorListCtrl.js",
                "~/src/app/pages/dashboard/transactionSummaryPerDealer/transactionSummaryPerDealer.directive.js",
                //end dashboard
                "~/src/app/pages/dashboard/dashboardTodo/dashboardTodo.directive.js",
                "~/src/app/pages/dashboard/dashboardTodo/DashboardTodoCtrl.js",
                "~/src/app/pages/dashboard/pieCharts/dashboardPieChart.js",
                "~/src/app/pages/dashboard/popularApp/popularApp.directive.js",
                "~/src/app/pages/dashboard/trafficChart/trafficChart.directive.js",
                "~/src/app/pages/dashboard/trafficChart/TrafficChartCtrl.js",
                "~/src/app/pages/dashboard/weather/weather.directive.js",
                "~/src/app/pages/dashboard/weather/WeatherCtrl.js",
            #endregion

            #region Access Control
                // Access control
                "~/src/app/pages/accessControl/accessControl.module.js",
                "~/src/app/pages/accessControl/application/applicationCtrl.js",
                "~/src/app/pages/accessControl/application/applicationFormCtrl.js",
                "~/src/app/pages/accessControl/client/clientCtrl.js",
                "~/src/app/pages/accessControl/client/clientFormCtrl.js",
                "~/src/app/pages/accessControl/role/roleCtrl.js",
                "~/src/app/pages/accessControl/role/roleFormCtrl.js",
                "~/src/app/pages/accessControl/user/userCtrl.js",
                "~/src/app/pages/accessControl/user/userFormCtrl.js",
                "~/src/app/pages/accessControl/clientRolePermissions/clientRolePermissionsCtrl.js",
                "~/src/app/pages/accessControl/clientUser/clientUserCtrl.js",
            #endregion

            #region Audit Log
                // Audit Log
                "~/src/app/pages/auditLog/auditLog.module.js",
                "~/src/app/pages/auditLog/threadLog/threadLogCtrl.js",
                "~/src/app/pages/auditLog/elmahErrorLogs/elmahCtrl.js",
                "~/src/app/pages/auditLog/allTransactionLog/allTransactionLogCtrl.js",
                "~/src/app/pages/auditLog/topApi/allRankedApiListCtrl.js",
                "~/src/app/pages/auditLog/allTransactionLog/failedTransactionLogCtrl.js",
                "~/src/app/pages/auditLog/allTransactionLog/transactionLogDetailCtrl.js",
                "~/src/app/pages/auditLog/cleanLog/cleanLogCtrl.js",
                "~/src/app/pages/auditLog/userActivity/userActivityCtrl.js",
                "~/src/app/pages/auditLog/elmahErrorLogs/errorLogSummaryPerApplication/errorLogSummaryPerApplication.directive.js",
                "~/src/app/pages/auditLog/elmahErrorLogs/latestErrorLog/latestErrorLog.directive.js",
                "~/src/app/pages/auditLog/elmahErrorLogs/errorLogMainInfo/errorLogMainInfo.directive.js",
                "~/src/app/pages/auditLog/elmahErrorLogs/errorLogMainInfo/errorLogMainInfoCtrl.js",
                "~/src/app/pages/auditLog/elmahErrorLogs/errorLogList/errorLogListCtrl.js",
                "~/src/app/pages/auditLog/elmahErrorLogs/errorLogDetail/errorLogDetailCtrl.js",


            #endregion

            #region Deployment
                // Deployment
                "~/src/app/pages/deployment/deployment.module.js",
                "~/src/app/pages/deployment/deploy/deployCtrl.js",
                "~/src/app/pages/deployment/rollback/rollbackCtrl.js",
                "~/src/app/pages/deployment/appVersion/appVersionCtrl.js",
                "~/src/app/pages/deployment/appVersion/appVersionFormCtrl.js",
            #endregion

            #region EndPoint
                // Endpoint
                "~/src/app/pages/endpoint/endpoint.module.js",
                "~/src/app/pages/endpoint/endpointCtrl.js",
                "~/src/app/pages/endpoint/endpointFormCtrl.js",
                "~/src/app/pages/endpoint/endpointGroupCtrl.js",
                "~/src/app/pages/endpoint/endpointTypeCtrl.js",
                "~/src/app/pages/endpoint/operationTypeCtrl.js",
            #endregion

            #region Scheduler
                // Scheduler
                "~/src/app/pages/scheduler/scheduler.module.js",
                "~/src/app/pages/scheduler/schedule/scheduleCtrl.js",
                "~/src/app/pages/scheduler/schedule/scheduleFormCtrl.js",
                "~/src/app/pages/scheduler/endpointSchedules/endpointScheduleCtrl.js",
            #endregion

            #region Throttler
                // Throtter
                "~/src/app/pages/throttler/throttler.module.js",
                "~/src/app/pages/throttler/throttlerCtrl.js",
                "~/src/app/pages/throttler/throttlerFormCtrl.js",
            #endregion

            #region Configuration
                // Configuration
                "~/src/app/pages/configuration/configuration.module.js",
                "~/src/app/pages/configuration/systemConfig/systemConfigCtrl.js",
                "~/src/app/pages/configuration/systemConfig/systemConfigFormCtrl.js",
                "~/src/app/pages/configuration/standardCode/standardCodeCtrl.js",
                "~/src/app/pages/configuration/standardCode/standardCodeFormCtrl.js",
                "~/src/app/pages/configuration/standardCodeChar/standardCodeCharCtrl.js",
                "~/src/app/pages/configuration/standardCodeChar/standardCodeCharFormCtrl.js",
            #endregion

            #region User Profile
                // User profile
                "~/src/app/pages/userProfile/userProfile.module.js",
                "~/src/app/pages/userProfile/updateUserProfileFormCtrl.js",
            #endregion

            #region Services
                // Services
                "~/src/app/services/common/modalHelper.js",
                "~/src/app/services/common/arrayHelper.js",
                "~/src/app/services/uiHelper.js",
                "~/src/app/services/responseHandler.js",
                "~/src/app/services/requestHandler.js",
                "~/src/app/services/pendingRequest.js",
                "~/src/app/services/services.module.js",
                "~/src/app/services/baseHttp.service.js",
                "~/src/app/services/auth.service.js",
                "~/src/app/services/user.service.js",
                "~/src/app/services/client.service.js",
                "~/src/app/services/clientUser.service.js",
                "~/src/app/services/application.service.js",
                "~/src/app/services/appVersion.service.js",
                "~/src/app/services/clientRolePermission.service.js",
                "~/src/app/services/endpointPermission.service.js",
                "~/src/app/services/throttler.service.js",
                "~/src/app/services/threadLog.service.js",
                "~/src/app/services/endpointSchedule.service.js",
                "~/src/app/services/permission.service.js",
                "~/src/app/services/role.service.js",
                "~/src/app/services/schedule.service.js",
                "~/src/app/services/transaction.service.js",
                "~/src/app/services/userActivity.service.js",
                "~/src/app/services/deployment.service.js",
                "~/src/app/services/rollback.service.js",
                "~/src/app/services/errorLog.service.js",
                "~/src/app/services/systemConfig.service.js",
                "~/src/app/services/dashboard.service.js",
                "~/src/app/services/standardCode.service.js",
                "~/src/app/services/standardCodeChar.service.js",
            #endregion

            #region Directives
                // Directives
                "~/src/app/pages/directives/directives.module.js",
                "~/src/app/pages/directives/forms/basicSelect/basicSelect.directive.js",
                "~/src/app/pages/directives/forms/inputText/inputText.directive.js",
                "~/src/app/pages/directives/forms/multipleSelect/multipleSelect.directive.js",
                "~/src/app/pages/directives/forms/multiSelectJquery/multiSelectJquery.directive.js",
                "~/src/app/pages/directives/forms/standardSelect/standardSelect.directive.js",
                "~/src/app/pages/directives/forms/switcher/switcher.directive.js",
                "~/src/app/pages/directives/forms/textArea/textArea.directive.js",
                "~/src/app/pages/directives/tables/tables.directive.js",
                "~/src/app/pages/directives/tables/transactionLogTables.directive.js"
            #endregion

                // end bundles
            ));

            #endregion

            #region Style bundles

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/bower_components/Ionicons/css/ionicons.css",
                "~/bower_components/angular-toastr/dist/angular-toastr.css",
                "~/bower_components/animate.css/animate.min.css",
                "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
                "~/bower_components/bootstrap-select/dist/css/bootstrap-select.min.css",
                "~/bower_components/bootstrap-switch/dist/css/bootstrap3/bootstrap-switch.css",
                "~/bower_components/bootstrap-tagsinput/dist/bootstrap-tagsinput.css",
                "~/bower_components/font-awesome/css/font-awesome.css",
                "~/bower_components/fullcalendar/dist/fullcalendar.css",
                "~/bower_components/leaflet/dist/leaflet.css",
                "~/bower_components/angular-progress-button-styles/dist/angular-progress-button-styles.min.css",
                "~/bower_components/chartist/dist/chartist.min.css",
                "~/bower_components/morris.js/morris.css",
                "~/bower_components/ionrangeslider/css/ion.rangeSlider.css",
                "~/bower_components/ionrangeslider/css/ion.rangeSlider.skinFlat.css",
                "~/bower_components/textAngular/dist/textAngular.css",
                "~/bower_components/angular-xeditable/dist/css/xeditable.css",
                "~/bower_components/jstree/dist/themes/default/style.css",
                "~/bower_components/angular-ui-select/dist/select.css",
                "~/bower_components/angular-loading-bar/build/loading-bar.min.css",
                "~/bower_components/angular-moment-picker/dist/angular-moment-picker.min.css",
                "~/src/assets/main.css",
                "~/src/app/theme/components/multiSelect/multi-select.css",
                "~/src/assets/style.css"
            ));

            #endregion


        }
    }
}
