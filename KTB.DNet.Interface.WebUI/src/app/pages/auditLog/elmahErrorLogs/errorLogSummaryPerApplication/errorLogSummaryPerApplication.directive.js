(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')

        .directive('errorSummaryPerApplication', function () {
            var errorSummaryPerApplicationCtrl = ['$scope', '$state', '$timeout', '$element', '$rootScope', 'layoutPaths', 'baConfig', 'ErrorLogService', function ($scope, $state, $timeout, $element, $rootScope, layoutPaths, baConfig, ErrorLogService) {
                var layoutColors = baConfig.colors;

                _renderChart();

                function _renderChart() {

                    ErrorLogService.GetErrorLogSummaryPerApplication().
                    then(function (response) {
                        if (response.Data == null || response.Data.length < 1) {
                            $("#chart-errorlog").html("There's no error today....")
                            return;
                        }
                        var id = "chart-errorlog";
                        var barChart = AmCharts.makeChart(id, {
                            type: 'serial',
                            theme: 'blur',
                            color: layoutColors.defaultText,
                            dataProvider: response.Data,
                            valueAxes: [
                              {
                                  axisAlpha: 0,
                                  position: 'left',
                                  title: 'Number of Log',
                                  gridAlpha: 0.5,
                                  gridColor: layoutColors.border,
                              }
                            ],
                            startDuration: 1,
                            graphs: [
                              {
                                  "fillAlphas": 0.9,
                                  "fillColors": layoutColors.primary,
                                  "lineAlpha": 0.2,
                                  "lineColor": layoutColors.primary,
                                  "title": "Log",
                                  "type": "column",
                                  "valueField": "Log",
                                  "fixedColumnWidth": 15
                              },
                              {
                                  "fillAlphas": 0.9,
                                  "fillColors": layoutColors.warning,
                                  "lineAlpha": 0.2,
                                  "lineColor": layoutColors.warning,
                                  "title": "Warning",
                                  "type": "column",
                                  "valueField": "Warning",
                                  "fixedColumnWidth": 15
                              },
                              {
                                  "fillAlphas": 0.9,
                                  "fillColors": layoutColors.danger,
                                  "lineAlpha": 0.2,
                                  "lineColor": layoutColors.danger,
                                  "title": "Fatal",
                                  "type": "column",
                                  "valueField": "Fatal",
                                  "fixedColumnWidth": 15
                              }
                            ],
                            chartCursor: {
                                categoryBalloonEnabled: false,
                                cursorAlpha: 0,
                                zoomable: false
                            },
                            legend: {
                                useGraphSettings: true,
                                position: 'top',
                                color: layoutColors.defaultText
                            },
                            categoryField: 'Application',
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
                        $timeout(_renderChart, 900000); 
                    }, function () { });
                }

            }];

            return {
                restrict: 'E',
                scope: {
                    'applicationName': '@application',
                },
                templateUrl: 'src/app/pages/auditLog/elmahErrorLogs/errorLogSummaryPerApplication/errorLogSummaryPerApplication.html',
                controller: errorSummaryPerApplicationCtrl,
            };

        });
})();