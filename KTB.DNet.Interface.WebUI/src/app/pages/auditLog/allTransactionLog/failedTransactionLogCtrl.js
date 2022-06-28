
(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')
        .controller('failedTransactionLogCtrl', failedTransactionLogCtrl)

    failedTransactionLogCtrl.$inject = ['$rootScope','$scope', '$state', '$timeout', 'TransactionService'];
    function failedTransactionLogCtrl($rootScope, $scope, $state, $timeout, TransactionService) {
        //$timeout(_autoRefresh, 3600000);
        var vm = $scope;
        vm.errorCount = 10;
        vm.httpService = TransactionService;
        vm.transaction = {};
        vm.currentState = $state.current.name;

        vm.tableDefinition = {
            name: "transaction",
            key: "Id",
            identifier: "Name",
            action: {
                customActions: [
                    { text: "view", type: "primary", customType: _customButtonType, action: _view, state: vm.currentState.replace("list", "view")}
                ],
            },
            columns: [
                { name: "Id", label: "ID" },
                { name: "Endpoint", label: "[Server]- Endpoint" },
                { name: "ErrorMessage", label: "Error Message" },
                { name: "CreatedTime", label: "Created Time", datatype: "date", format: "dd MMMM yyyy HH:mm:ss" },
                { name: "Actions", label: "Actions", action: true },
            ],
            tablePageSize: 20 // rows
        }

        // auto refresh error transaction log
        function _autoRefresh() {
            if ($state.current.name == "auditLog.failedTransactionLog.list" && $scope.tableDefinition.hasOwnProperty("reload")) {
                $scope.tableDefinition.reload();
            }

            //$timeout(_autoRefresh, 3600000);
        }

        function _view(row) {
            $state.go(vm.currentState.replace("list", "view"), { "id": row.Id });
        }

        
        function _customButtonType(row) {
            if (row.IsResolved) {

                return 'success';
            }
            else {
                return 'danger';
            }
        }
    }

})();