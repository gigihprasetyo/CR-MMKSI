
(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')
        .controller('errorLogListCtrl', errorLogListCtrl)

    errorLogListCtrl.$inject = ['$scope', '$state', 'UIHelper', '$stateParams', 'ErrorLogService'];
    function errorLogListCtrl($scope, $state, UIHelper, $stateParams, ErrorLogService) {
        var vm = $scope;
        vm.httpService = ErrorLogService;
        vm.customRenderData = _renderData;
        vm.tableState = null;
        vm.backToDashboard = _backToDashboard;
        vm.appName = '';
        vm.selected = { App: undefined };
        vm.listOfApplicationNames = [];
        vm.onSelectCallback = _onSelectCallback;
        vm.errorLogTableDefinition = _getErrorLogTableDefinition();
        vm.view = _view;

        if($stateParams.app) {
            vm.appName = $stateParams.app;
        }

        vm.httpService.GetApplicationLogList()
            .then(function (response) {
                if (response.Data != null) {
                    vm.listOfApplicationNames = response.Data;

                    var i = $.grep(vm.listOfApplicationNames, function (e) { return e.label === vm.appName; });
                    vm.selected.App = i[0];
                }
            }, function (error) { });

        function _getErrorLogTableDefinition() {
            return {
                name: "Error Log",
                key: "ErrorId",
                identifier: "ErrorLog",
                action: {
                },
                clickRow: true,
                hideSearch: true,
                fieldSearch: true,
                columns: [
                    { name: "Sequence", label: "#", datatype: "action", onClick: _view },
                    { name: "TimeLocal", label: "Time", datatype: "date", format: "dd MMM yy HH:mm:ss", onClick: _view },
                    { name: "Verb", label: "URL", attrClass: "tag label label-default" },
                    { name: "URL", label: "", searchable: true },
                    { name: "StatusCode", label: "Code", datatype: "string" },
                    { name: "Host", label: "Host", searchable: true },
                    { name: "Type", label: "Type", searchable: true },
                    { name: "Message", label: "Message", searchable: true, attrStyle: { 'text-align': 'left' } },
                ],
                tablePageSize: 20
            };
        }

        function _renderData(tableState) {
            vm.tableState = tableState;

            // $scope.isLoading = true;
            var params = UIHelper.convertTableStateToSearchParams(tableState);
            params.searchParams = {};
            angular.forEach($scope.errorLogTableDefinition.columns, function (value, key) {
                var searchParamInput = document.getElementsByName("search" + value.name);
                if (searchParamInput.length > 0) {
                    params.searchParams[searchParamInput[0].name] = searchParamInput[0].value;
                }
            });
            vm.httpService.Search(params, vm.appName)
                .then(function (response) {
                    vm.errorLogTableDefinition.dataSource = response.Records

                    tableState.pagination.numberOfPages = params.Length > 0 ? Math.ceil(response.TotalRecord * 1.0 / params.Length) : 1;

                    $scope.isLoading = false;
                }, function (error) {
                    $scope.isLoading = false;
                });
        }

        function _onSelectCallback(item, model) {
            if (vm.appName !== model.value) {
                vm.appName = model.value;
                vm.customRenderData(vm.tableState);
            }
        };
        
        function _backToDashboard() {
            $state.go("auditLog.elmahlog.dashboard");
        }

        function _view(row) {
            $state.go("auditLog.elmahlog.view", { id: row.ErrorId, app: row.Application });
        }       
    }

})();