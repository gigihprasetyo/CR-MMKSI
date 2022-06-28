(function () {
    'use strict';

    angular.module('DNet.pages.directives')

        /**
        * The general table directive
        */

        .directive('generalTable', function () {
            var generalTableCtrl = ['$scope', '$timeout', 'UIHelper', 'ModalHelper', function ($scope, $timeout, UIHelper, ModalHelper) {

                var tableName = $scope.tableDefinition.name;
                var identifier = $scope.tableDefinition.identifier;

                $scope.getDisplayedValue = _getDisplayedValue;
                $scope.delete = _delete;
                $scope.defaultDelete = _defaultDelete;
                $scope.renderData = _renderData;
                $scope.tableDefinition.reload = _reload;
                $scope.fetchData = _fetchData;
                $scope.getOptionValue = _getOptionValue;

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

                function _delete(row) {

                    // var dataName = tableName + " (" + identifier + " : " + $scope.getDisplayedValue(row, identifier) + ")?";
                    var dataName = tableName + ' "' + $scope.getDisplayedValue(row, identifier) + '"?';

                    ModalHelper
                        .showMessage(
                            "Are you sure you want to delete " + dataName,
                            "confirm")
                        .then(
                            function () {
                                if ($scope.tableDefinition.action.delete.customDelete) {
                                    $scope.tableDefinition.action.delete.customDelete(row);
                                }
                                else {
                                    $scope.defaultDelete(row, dataName);
                                }
                            }, function () {
                                // canceled.
                            }
                        );
                }

                function _defaultDelete(row, dataName) {

                    $scope.httpService.Delete(row[$scope.tableDefinition.key])
                        .then(function (response) {
                            if (response.Success) {
                                _reload();
                            }

                        }, function (response) {
                            if (response) {
                            }
                        });
                };

                function _renderData(tableState, ctrl) {
                    $scope.stCtrl = ctrl;
                    $scope.fetchData(tableState);
                }

                function _reload() {
                    var tableState = $scope.stCtrl.tableState();
                    $scope.fetchData(tableState);
                }

                function _fetchData(tableState) {

                    $scope.isLoading = true;
                    var params = UIHelper.convertTableStateToSearchParams(tableState);
                    var fnName = $scope.fnName ? $scope.fnName : "Search"
                    $scope.httpService[fnName](params)
                        .then(function (response) {
                            $scope.tableDefinition.dataSource = response.Records;
                            tableState.pagination.numberOfPages = params.Length > 0 ? Math.ceil(response.TotalRecord * 1.0 / params.Length) : 1;

                            $scope.isLoading = false;
                        }, function (error) {
                            $scope.isLoading = false;
                        });

                }
            }];

            return {
                restrict: 'A',
                scope: {
                    'httpService': '=httpService',
                    'fnName': '@fnName',
                    'customPipe': '=customPipe',
                    'tableDefinition': '=tableDefinition',
                    'onSelectRow': '=onSelectRow',
                    'offline': '@'

                },
                templateUrl: 'src/app/pages/directives/tables/generalTable.html',
                controller: generalTableCtrl,
                link: function (scope, elem, attrs, form) {
                    if (attrs.customPipe) scope.renderData = scope.customPipe
                }
            };

        })
        .directive('onselectrow', function () {
            return {
                require: '^stTable',
                restrict: 'A',
                template: '',
                scope: {
                    'onselectrow': '=onselectrow',
                    'row': '=rowData',
                },
                link: function (scope, element, attr, ctrl) {
                    if (typeof scope.onselectrow === "function") element.addClass('selectable');

                    element.bind('click', function (evt) {
                        if (typeof scope.onselectrow !== "function") return
                        scope.onselectrow(scope.row, ctrl.tableState());
                        scope.$apply(function () {
                            ctrl.select(scope.row, 'single');
                        });
                    });

                    scope.$watch('row.isSelected', function (newValue, oldValue) {
                        if (newValue === true) {
                            element.addClass('active');
                        } else {
                            element.removeClass('active');
                        }
                    });
                }
            };
        })
        .directive('customPagination', ['stConfig', function (stConfig) {
            return {
                restrict: 'EA',
                require: '^stTable',
                scope: {
                    stItemsByPage: '=?',
                    stDisplayedPages: '=?',
                    stPageChange: '&'
                },
                templateUrl: 'src/app/pages/directives/tables/customPagination.html',
                link: function (scope, element, attrs, ctrl) {

                    scope.stItemsByPage = scope.stItemsByPage ? +(scope.stItemsByPage) : stConfig.pagination.itemsByPage;
                    scope.stDisplayedPages = scope.stDisplayedPages ? +(scope.stDisplayedPages) : stConfig.pagination.displayedPages;

                    scope.currentPage = 1;
                    scope.pages = [];
                    scope.numberOfPages = [
                        5, 10, 25, 50, 100
                    ]

                    var rowPerPage = 10;

                    scope.changeNumberOfRow = function(stItemsByPage){
                        rowPerPage = stItemsByPage;
                        ctrl.slice(0, stItemsByPage);
                    };

                    function redraw() {
                        var paginationState = ctrl.tableState().pagination;
                        paginationState.number = rowPerPage;
                        var start = 1;
                        var end;
                        var i;
                        var prevPage = scope.currentPage;
                        scope.totalItemCount = paginationState.totalItemCount;
                        scope.currentPage = Math.floor(paginationState.start / paginationState.number) + 1;
                        start = Math.max(start, scope.currentPage - Math.abs(Math.floor(scope.stDisplayedPages / 2)));
                        end = start + scope.stDisplayedPages;

                        if (end > paginationState.numberOfPages) {
                            end = paginationState.numberOfPages + 1;
                            start = Math.max(1, end - scope.stDisplayedPages);
                        }

                        scope.start = start;
                        scope.end = end;
                        scope.totalPages = paginationState.numberOfPages;
                        scope.pages = [];
                        scope.numPages = paginationState.numberOfPages;

                        for (i = start; i < end; i++) {
                            scope.pages.push(i);
                        }

                        if (prevPage !== scope.currentPage) {
                            scope.stPageChange({ newPage: scope.currentPage });
                        }

                    }

                    //table state --> view
                    scope.$watch(function () {
                        return ctrl.tableState().pagination;
                    }, redraw, true);

                    scope.$watch('stDisplayedPages', redraw);

                    //view -> table state
                    scope.selectPage = function (page) {
                        if (page > 0 && page <= scope.numPages) {
                            ctrl.slice((page - 1) * rowPerPage, rowPerPage);
                        }
                    };

                    if (!ctrl.tableState().pagination.number) {
                        ctrl.slice(0, rowPerPage);
                    }
                }
            };
        }]);


})();