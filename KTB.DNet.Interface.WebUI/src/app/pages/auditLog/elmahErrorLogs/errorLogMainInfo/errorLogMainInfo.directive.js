(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')
        .directive('dashboardPieChart', dashboardPieChart);

    /** @ngInject */
    function dashboardPieChart() {
        var DashboardPieChartCtrl = ['$scope', '$timeout', 'baConfig', 'baUtil', 'ErrorLogService',
            function ($scope, $timeout, baConfig, baUtil, ErrorLogService) {
            }];

        return {
            restrict: 'E',
            controller: 'DashboardPieChartCtrl',
            templateUrl: 'src/app/pages/auditLog/elmahErrorLogs/errorLogMainInfo/errorLogMainInfo.html'
        };
    }
})();