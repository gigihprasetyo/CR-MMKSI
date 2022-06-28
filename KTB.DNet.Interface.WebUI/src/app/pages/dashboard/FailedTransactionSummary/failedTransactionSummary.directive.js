/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.pages.dashboard')

      .directive('failedTransactionSummary', function () {
          var failedTransactionSummaryCtrl = ['$state', '$timeout', '$element', '$rootScope', 'layoutPaths', 'baConfig', 'DashboardService', function ($state, $timeout, $element, $rootScope, layoutPaths, baConfig, DashboardService) {
              var layoutColors = baConfig.colors;
              
              _renderChart();

              function _renderChart() {
                if ($state.current.name != "dashboard.view" || !$rootScope.windowFocused) {
                    return;
                }
                DashboardService.GetFailedTransactionSummaryPerDealer().
                then(function (response) {
                    if (response.Data == null || response.Data.length < 1) {
                        $("#failed-transaction-summary").html("There's no failed transaction today....")
                        return;
                    }

                    var id = "failed-transaction-summary";
                    var pieChart = AmCharts.makeChart(id, {
                        titles: [{text: "Daily Failed Transaction Summary", size: 15}],
                        type: 'pie',
                        startDuration: 0,
                        theme: 'blur',
                        addClassNames: true,
                        color: layoutColors.defaultText,
                        //labelTickColor: layoutColors.borderDark,
                      //   legend: {
                      //       position: 'bottom',
                      //   },
                        innerRadius: '30%',
                        dataProvider: response.Data,
                        colorField: 'Color',
                        valueField: 'Total',
                        titleField: 'DealerCode',
                        export: {
                            enabled: true
                        }
                      
                    });
  
                    //pieChart.addListener('init', handleInit);
  
                    pieChart.addListener('rollOverSlice', function (e) {
                        handleRollOver(e);
                    });
  
                    function handleInit() {
                        pieChart.legend.addListener('rollOverItem', handleRollOver);
                    }
  
                    function handleRollOver(e) {
                        var wedge = e.dataItem.wedge.node;
                        wedge.parentNode.appendChild(wedge);
                    }
                    $timeout(_renderChart, 30000); // 30 seconds
                }, function () { });
              }
              
          }];

          return {
              restrict: 'E',
              scope: {},
              templateUrl: 'src/app/pages/dashboard/failedTransactionSummary/failedTransactionSummary.html',
              controller: failedTransactionSummaryCtrl
          };

      });
})();