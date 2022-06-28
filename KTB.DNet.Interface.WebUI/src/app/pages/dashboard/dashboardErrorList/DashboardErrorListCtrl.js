/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
    'use strict';

    angular.module('DNet.pages.dashboard')
        .controller('DashboardErrorListCtrl', DashboardErrorListCtrl);

    /** @ngInject */
    DashboardErrorListCtrl.$inject = ['$state', '$scope', '$timeout', '$rootScope', 'DashboardService'];
    function DashboardErrorListCtrl($state, $scope, $timeout, $rootScope, DashboardService) {
       // $timeout(_autoRefresh, 3600000);

        $scope.httpService = DashboardService;
        $scope.transaction = {};
        $scope.viewErrorDetail = _viewErrorDetail;
        $scope.tableErrorDefinition = {
            name: "Latest Errors",
            key: "Id",
            identifier: "Name",
            action: {
            },

            columns: [
                { name: "ID", label: "ID", datatype: "action", onClick: _view },
                { name: "Endpoint", label: "API Name" },
                { name: "IsResolved", label: "Resolved", datatype: "boolean" },
                { name: "ErrorTime", label: "Error Time", datatype: "date", format: "yyyy-MM-dd HH:mm:ss" }
            ],
            tablePageSize: 5, // rows
            hideSearch: true,
            hideFooter:true
        }

        // auto refresh error transaction log
        function _autoRefresh() {
            if ($state.current.name != "dashboard.view" || !$rootScope.windowFocused ) {
                return;
            }    
            $scope.tableErrorDefinition.reload();
           // $timeout(_autoRefresh, 3600000); // 1 Hour
                
        }

        function _viewErrorDetail() {
            $state.go('auditLog.failedTransactionLog.list')
        }

        function _view(row) {
            $state.go("auditLog.failedTransactionLog.view", { "id": row.ID });
        }
    }
})();