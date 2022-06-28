
(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')
        .controller('errorLogDetailCtrl', errorLogDetailCtrl)

    errorLogDetailCtrl.$inject = ['$scope', '$state', '$stateParams', 'ErrorLogService'];
    function errorLogDetailCtrl($scope, $state, $stateParams, ErrorLogService) {
        var vm = $scope;
        vm.httpService = ErrorLogService;

        vm.serverVariableTableDefinition = _getServerVariableTableDefinition();
        vm.back = _back;
        vm.GetValue = _getvalue;
        vm.FormatDate = _formatDate;
        vm.appName = '';
        
        // for view detail
        if ($stateParams.id) {
            vm.appName = $stateParams.app
            vm.httpService.GetErrorDetail($stateParams.id, $stateParams.app)
                .then(function (response) {
                    vm.errorLogDetail = response;
                    if (response !== undefined) {
                        if (response.error != undefined) {
                            if (response.error.serverVariables.item.length > 0) {
                                vm.serverVariableData = vm.errorLogDetail.error.serverVariables.item;
                                vm.serverVariableTableDefinition.dataSource = vm.errorLogDetail.error.serverVariables.item;
                            }
                            if (response.error.cookies != undefined) {
                                if (response.error.cookies.item != undefined) {
                                    vm.cookiesData = vm.errorLogDetail.error.cookies.item;
                                }
                            }
                        }
                    }
                }, function (error) { });
        }

        // server variable table definition
        function _getServerVariableTableDefinition() {
            return {
                name: "Server Variable",
                key: "@name",
                identifier: "@name",
                action: {
                },
                columns: [
                    { name: "@name", label: "Variable Name" },
                    { name: "value.@string", label: "Value", attrStyle: { 'text-align': 'left' } },
                ],
                searchable: false,
                sortable: false,
                tablePageSize: 10 // rows
            };
        }

        function _getvalue(data, key) {
            if (data !== undefined) {
                if (data.length > 0) {
                    return data.filter(function (item) { return (item['@name'] == key); })[0].value['@string'];
                }
            }
        }

        function _formatDate(d) {
            if (d !== undefined) {
                return moment(d).format('llll')
            }
        }

        function _back(application) {
            $state.go("auditLog.elmahlog.list", { app: application });
        }
    }

})();