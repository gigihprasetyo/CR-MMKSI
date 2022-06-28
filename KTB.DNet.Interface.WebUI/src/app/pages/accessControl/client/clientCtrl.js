
(function () {
    'use strict';

    angular.module('DNet.pages.accessControl')
        .controller('clientCtrl', clientCtrl)

    clientCtrl.$inject = ['$scope', 'ClientService'];
    function clientCtrl($scope, ClientService) {
        var vm = $scope
        vm.smartTablePageSize = 10;
        vm.httpService = ClientService;
        vm.client = {}

        vm.data = [];
        vm.tableDefinition = {
            name: "client",
            key: "ClientId",
            identifier: "Name",
            action: {
                create: "accessControl.client.create",
                update: "accessControl.client.update",
                delete: true
            },
            columns: [
                { name: "ClientId", label: "Client ID", sortColumnName: "APIClient.ClientId" },
                { name: "Name", label: "Name", sortColumnName: "APIClient.Name" },
                { name: "MsApplication.Name", label: "Application Name", sortColumnName: "MsApplication.Name" },
                { name: "Actions", label: "Actions", action: true },
            ],
            tablePageSize: 10 // rows
        }


    }

})();