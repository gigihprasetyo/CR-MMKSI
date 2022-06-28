(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')
        .controller('DashboardPieChartCtrl', DashboardPieChartCtrl);

    /** @ngInject */
    function DashboardPieChartCtrl($scope, $state, $timeout, baConfig, baUtil, ErrorLogService) {
        $scope.charts = [];
        $scope.logList = _errorLogList;
        loadPieCharts();
       

        function loadPieCharts() {
            var pieColor = baUtil.hexToRGB(baConfig.colors.defaultText, 0.2);
            ErrorLogService.GetErrorLogMainInfo().
                then(function (response) {
                    if (response.Data == null || response.Data.length < 1) {
                        $("#pie-chart-item").html("There's no error....")
                        return;
                    }

                    $scope.charts = response.Data;

                    $timeout(function () {
                        $('.chart').each(function () {
                            var chart = $(this);
                            chart.easyPieChart({
                                easing: 'easeOutBounce',
                                barColor: '#ef1e25',
                                size: 84,
                                scaleLength: 0,
                                animation: 2000,
                                lineWidth: 9,
                                lineCap: 'round',
                            });
                        });
                    }, 1000);
                });


            $timeout(function () {

                loadPieCharts();
            }, 100000);
        }

        function _errorLogList(chart) {
            $state.go("auditLog.elmahlog.list", { app: chart.description });
        };
    }
})();