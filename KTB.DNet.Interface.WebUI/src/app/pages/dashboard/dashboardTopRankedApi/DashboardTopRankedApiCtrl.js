/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
    'use strict';

    angular.module('DNet.pages.dashboard')
        .controller('DashboardTopRankedApiCtrl', DashboardTopRankedApiCtrl);

    /** @ngInject */
    DashboardTopRankedApiCtrl.$inject = ['$state', '$scope', 'DashboardService'];
    function DashboardTopRankedApiCtrl($state, $scope, DashboardService) {
        $scope.httpService = DashboardService;        
        $scope.transaction = {};
        $scope.viewAPIDetail = _viewAPIDetail;
        $scope.tableTopRankedAPIDefinition = {
            name: "Top Ranked API",
            key: "Id",
            identifier: "Name",
            action: {               
            },

            columns: [
                { name: "Endpoint", label: "Interface" },
                { name: "Total", label: "Total" }
            ],
            tablePageSize: 5, // rows
            hideSearch: true,
            hideFooter: true
        }

        function _viewAPIDetail() {
            $state.go('auditLog.topApi.list')
        }
    }
})();