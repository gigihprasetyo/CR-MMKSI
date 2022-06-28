/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
    'use strict';

    angular.module('DNet.pages.dashboard')
        .controller('DashboardTransactionListCtrl', DashboardTransactionListCtrl);

    /** @ngInject */
    DashboardTransactionListCtrl.$inject = ['$state', '$scope', 'DashboardService'];
    function DashboardTransactionListCtrl($state, $scope, DashboardService) {
        $scope.httpService = DashboardService;
        $scope.viewDetail = _viewDetail;
        $scope.transaction = {};
        $scope.tableTransactionDefinition = {
            name: "Latest Transactions",
            key: "Id",
            identifier: "Name",
            action: {
               
            },

            columns: [
                { name: "ID", label: "ID", datatype: "action", onClick: _view },
                { name: "Endpoint", label: "API Name" },
                { name: "CreatedBy", label: "Created By" },
                { name: "CreatedTime", label: "Transaction Time" },
                { name: "Status", label: "Status" },
                { name: "IsResolved", label: "Resolved", datatype: "boolean" }
            ],
            tablePageSize: 5, // rows
            hideSearch: true,
            hideFooter:true
        }

        function _viewDetail() {
            $state.go('auditLog.transactionLog.list')
        }

        function _view(row) {
            $state.go("auditLog.transactionLog.view", { "id": row.ID });
        }
    }
})();