(function () {
    'use strict';

    angular.module('DNet.pages.directives')

        /**
        * The transaction Log table directive
        */

        .directive('transactionLogTable', function () {
            var transactionLogTableCtrl = ['$scope', '$timeout', 'UIHelper', 'ModalHelper', 'TransactionService', function ($scope, $timeout, UIHelper, ModalHelper, TransactionService) {

                var tableName = $scope.transactionLogTableDefinition.name;
                var identifier = $scope.transactionLogTableDefinition.identifier;

                $scope.httpService = TransactionService;
                $scope.getDisplayedValue = _getDisplayedValue;
                $scope.renderData = _renderData;
                $scope.reload = _reload;
                $scope.transactionLogTableDefinition.reload = _reload;
                $scope.onSelectCallback = _onSelectCallback;

                $scope.fetchData = _fetchData;
                $scope.getOptionValue = _getOptionValue;

                $scope.propValue = '';
                $scope.propertyNameList = [
                      { label: 'Select Property Name', value: '' },
                      { label: 'Input', value: 'Input' },
                      { label: 'Output', value: 'Output' },
                      { label: 'Sender IP', value: 'SenderIP' }
                    ];

                var date = new Date();

                $scope.selected = {
                    AppId: undefined,
                    ClientId: undefined,
                    Property: undefined,
                    Dealer: {},
                    Date: {
                        From: new Date(),//new Date(date.getFullYear(), date.getMonth(), 1),
                        To: new Date()
                    }
                };

                $scope.isShowDealer = true;

                TransactionService.GetApplicationList()
                    .then(function (response) {

                        if (response.Data != null) {
                            $scope.listOfApplications = response.Data;
                        }
                    }, function (error) { });

                TransactionService.GetClientList()
                    .then(function (response) {
                        $scope.listOfClients = response.Data;
                    });

                TransactionService.GetDealerList()
                    .then(function (response) {

                        $scope.listOfDealers = response.Data;
                        if (response.Data.length == 1) {
                            $scope.selected.Dealer = response.Data[0];
                            $scope.isShowDealer = false;
                        }
                    });

                function _getDisplayedValue(record, propertyName) {

                    var listOfName = propertyName.split(".");

                    if (listOfName.length > 1) {
                        if (record[listOfName[0]]) {
                            return $scope.getDisplayedValue(record[listOfName[0]], propertyName.replace(listOfName[0] + ".", ""));
                        }

                        return null;
                    }
                    else {
                        return record[propertyName];
                    }
                }

                function _getOptionValue(options, optionKey, optionText, keyValue) {
                    var text = '';
                    if (options && options.length > 0) {
                        options.forEach(function (item, index) {

                            if (item[optionKey] == keyValue) {
                                text = item[optionText];
                            }
                        });
                    }

                    return text;
                }


                function _renderData(tableState, ctrl) {
                    $scope.stCtrl = ctrl;
                    $scope.fetchData(tableState);
                }

                function _reload() {
                    if ($scope.stCtrl) {
                        var tableState = $scope.stCtrl.tableState();
                        $scope.fetchData(tableState);
                        var activeElement = document.activeElement;
                        if (activeElement) {
                            activeElement.blur();
                        }
                    }
                }

                function _fetchData(tableState) {

                    $scope.isLoading = true;
                    var params = UIHelper.convertTableStateToSearchParams(tableState);
                    params.searchParams = {};
                    angular.forEach($scope.transactionLogTableDefinition.columns, function (value, key) {
                        var searchParamInput = document.getElementsByName("search" + value.name);
                        if (searchParamInput.length > 0) {
                            params.searchParams[searchParamInput[0].name] = searchParamInput[0].value;
                        }
                    });
                    params.searchParams["beginDate"] = moment($scope.selected.Date.From, "DD MMMM YYYY").toDate();
                    params.searchParams["endDate"] = moment($scope.selected.Date.To, "DD MMMM YYYY").toDate();
                    if ($scope.selected.Property && $scope.propValue) {
                        params.searchParams[$scope.selected.Property.value] = $scope.propValue;
                    }
                    
                    params.appId = $scope.selected.AppId;
                    params.clientId = $scope.selected.ClientId;
                    params.dealerCode = $scope.selected.Dealer.DealerCode;
                    var fnName = $scope.fnName ? $scope.fnName : "Search"
                    $scope.httpService[fnName](params)
                        .then(function (response) {
                            $scope.transactionLogTableDefinition.dataSource = response.Records;
                            tableState.pagination.numberOfPages = params.Length > 0 ? Math.ceil(response.TotalRecord * 1.0 / params.Length) : 1;

                            $scope.isLoading = false;
                        }, function (error) {
                            $scope.isLoading = false;
                        });

                }

                function _onSelectCallback() {
                    // $scope.reload();
                };

                $scope.propValueOnChange = function _propValueOnChange(e) {
                        // if ($scope.selected.Property) {
                        //     $scope.reload();
                        // }
                }

                //$scope.$watch('propValue', function (e) {
                //    if ($scope.selected.Property.value) {
                //        $scope.reload();
                //    }
                //}, true);
            }];

            return {
                restrict: 'A',
                scope: {
                    'httpService': '=httpService',
                    'fnName': '@fnName',
                    'customPipe': '=customPipe',
                    'transactionLogTableDefinition': '=transactionLogTableDefinition',
                    'offline': '@'

                },
                templateUrl: 'src/app/pages/directives/tables/transactionLogTable.html',
                controller: transactionLogTableCtrl,
                link: function (scope, elem, attrs, form) {
                    if (attrs.customPipe) scope.renderData = scope.customPipe
                }
            };
        })
})();