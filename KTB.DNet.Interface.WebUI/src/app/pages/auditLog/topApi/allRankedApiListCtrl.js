
(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')
        .controller('allRankedApiListCtrl', allRankedApiListCtrl)

    allRankedApiListCtrl.$inject = ['$scope', '$state', 'TransactionService'];
    function allRankedApiListCtrl($scope, $state, TransactionService) {
        var vm = $scope;
        vm.httpService = TransactionService;
        vm.transaction = {};
        vm.currentState = $state.current.name;

        vm.tableDefinition = {
            name: "Top Ranked API List",
            key: "Id",
            identifier: "Name",
            action: {

            },
            columns: [
                { name: "Endpoint", label: "Interface" },
                { name: "Total", label: "Total" },

            ],
            tablePageSize: 10, // rows
        }
    }

})();

