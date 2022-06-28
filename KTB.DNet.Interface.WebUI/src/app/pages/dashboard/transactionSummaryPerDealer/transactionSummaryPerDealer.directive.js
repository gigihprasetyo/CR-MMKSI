/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.pages.dashboard')

      .directive('transactionSummaryPerDealer', function () {
          var transactionSummaryPerDealerCtrl = ['$state', '$timeout', '$element', '$rootScope', 'layoutPaths', 'baConfig','DashboardService', function ($state, $timeout, $element, $rootScope, layoutPaths, baConfig, DashboardService) {
              var layoutColors = baConfig.colors;
              
              _renderChart();

              function _renderChart() {
                if ($state.current.name != "dashboard.view" || !$rootScope.windowFocused) {
                    return;
                }

                DashboardService.GetTransactionSummaryPerDealer().
                then(function (response) {
                    if (response.Data == null || response.Data.length < 1) {
                      $("#transaction-summary-per-dealer").html("There's no transaction today....")
                      return;
                    }
                    var id = "transaction-summary-per-dealer";
                    var barChart = AmCharts.makeChart(id, {
                        type: 'serial',
                        theme: 'blur',
                        color: layoutColors.defaultText,
                        dataProvider: response.Data,
                        valueAxes: [
                          {
                            axisAlpha: 0,
                            position: 'left',
                            title: 'Number of Transactions',
                            gridAlpha: 0.5,
                            gridColor: layoutColors.border,
                          }
                        ],
                        startDuration: 1,
                        graphs: [
                          {
                            "fillAlphas": 0.9,
                            "fillColors": "#a6c733",
                            "lineAlpha": 0.2,
                            "lineColor": "#a6c733",
                            "title": "Successful",
                            "type": "column",
                            "valueField": "Success",
                            "fixedColumnWidth": 15
                          },
                          {
                            "fillAlphas": 0.9,
                            "fillColors": "#fa5655",
                            "lineAlpha": 0.2,
                            "lineColor": "#fa5655",
                            "title": "Failed",
                            "type": "column",
                            "valueField": "Failed",
                            "fixedColumnWidth": 15
                          }
                        ],
                        chartCursor: {
                          categoryBalloonEnabled: false,
                          cursorAlpha: 0,
                          zoomable: false
                        },
                        categoryField: 'DealerCode',
                        columnSpacing: 0,
                        columnWidth: 0.8,
                        categoryAxis: {
                          gridPosition: 'start',
                          labelRotation: 45,
                          gridAlpha: 0.5,
                          gridColor: layoutColors.border,
                        },
                        export: {
                          enabled: true
                        },
                        creditsPosition: 'top-right',
                        pathToImages: layoutPaths.images.amChart
                      });
                     $timeout(_renderChart, 30000); // 30 seconds
                }, function () { });
              }
              
          }];

          return {
              restrict: 'E',
              scope: {},
              templateUrl: 'src/app/pages/dashboard/transactionSummaryPerDealer/transactionSummaryPerDealer.html',
              controller: transactionSummaryPerDealerCtrl
          };

      });
})();