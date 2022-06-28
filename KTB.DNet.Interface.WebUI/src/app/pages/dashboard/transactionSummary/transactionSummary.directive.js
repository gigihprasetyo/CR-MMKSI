/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.pages.dashboard')

      .directive('transactionSummary', function () {
          var transactionSummaryCtrl = ['$state', '$timeout', '$scope', '$rootScope', '$element', 'layoutPaths', 'baConfig', 'DashboardService', function ($state, $timeout, $scope, $rootScope, $element, layoutPaths, baConfig, DashboardService) {
              var layoutColors = baConfig.colors;
              
              _renderChart();

              function _renderChart(){
                if ($state.current.name != "dashboard.view" || !$rootScope.windowFocused ) {
                    return;
                }  
                
                DashboardService.GetTransactionSummary().
                then(function (response) {
                    if (response.Data == null ||
                        (
                        response.Data[0].Total < 1 &&
                        response.Data[1].Total < 1 &&
                        response.Data[2].Total < 1
                        )) {
                        $("#transaction-summary").html("There's no transaction today....")
                        return;
                    }

                    var id = "transaction-summary";
                    var pieChart = AmCharts.makeChart(id, {
                        titles: [{text: "Daily Transaction Summary", size: 15}],
                        type: 'pie',
                        startDuration: 0,
                        theme: 'blur',
                        addClassNames: true,
                        color: layoutColors.defaultText,
                        //labelTickColor: layoutColors.borderDark,
                        //legend: {
                        //    position: 'bottom',
                        //},
                        defs: {
                            filter: [
                              {
                                id: 'shadow',
                                width: '200%',
                                height: '200%',
                                feOffset: {
                                  result: 'offOut',
                                  in: 'SourceAlpha',
                                  dx: 0,
                                  dy: 0
                                },
                                feGaussianBlur: {
                                  result: 'blurOut',
                                  in: 'offOut',
                                  stdDeviation: 5
                                },
                                feBlend: {
                                  in: 'SourceGraphic',
                                  in2: 'blurOut',
                                  mode: 'normal'
                                }
                              }
                            ]
                          },
                        innerRadius: '30%',
                        dataProvider: response.Data,
                        colorField: 'Color',
                        valueField: 'Total',
                        titleField: 'TransactionType',
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
              templateUrl: 'src/app/pages/dashboard/transactionSummary/transactionSummary.html',
              controller: transactionSummaryCtrl
          };

      });
})();