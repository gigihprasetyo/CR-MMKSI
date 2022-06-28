
(function () {
    'use strict';

    angular.module('DNet.pages.endpoint')
        .controller('endpointCtrl', endpointCtrl)

    endpointCtrl.$inject = ['$scope', 'EndpointPermissionService'];
    function endpointCtrl($scope, EndpointPermissionService) {
        var vm = $scope;
        vm.endpointPermission = {};
        vm.httpService = EndpointPermissionService;
        vm.listOfEndpointTypeOptions = [];
        vm.listOfEndpointGroupOptions = [];
        vm.listOfOperationTypeOptions = [];
        vm.getTableDefinition = _getTableDefinition;
        vm.getColumnDefinition = _getColumnDefinition;
        vm.tableDefinition = vm.getTableDefinition();

        EndpointPermissionService.GetOptions()
            .then(function (response) {
                vm.listOfEndpointTypeOptions = response.Data.ListOfEndpointTypeOptions;
                vm.listOfOperationTypeOptions = response.Data.ListOfOperationTypeOptions;
                vm.listOfEndpointGroupOptions = response.Data.ListOfEndpointGroupOptions;
                vm.tableDefinition.columns = vm.getColumnDefinition();

            }, function (error) { });


        function _getTableDefinition() {
            return {
                name: "endpoint permission",
                key: "Id",
                identifier: "Name",
                action: {
                    create: "endpoint.create",
                    update: "endpoint.update",
                    delete: true,
                    refresh: true
                },
                columns: vm.getColumnDefinition(),
                tablePageSize: 10 // rows
            }
        }

        function _getColumnDefinition() {
            return [
                { name: "Name", label: "Name" },
                { name: "URI", label: "URI" },
                { name: "EndpointGroup", label: "Endpoint Group", datatype: "option", options: vm.listOfEndpointGroupOptions, optionKey: "Value", optionText: "Text" },
                { name: "EndpointType", label: "Type", datatype: "option", options: vm.listOfEndpointTypeOptions, optionKey: "Value", optionText: "Text" },
                { name: "OperationType", label: "Operation", datatype: "option", options: vm.listOfOperationTypeOptions, optionKey: "Value", optionText: "Text" },
                { name: "IsScheduled", label: "Scheduled", datatype: "boolean" , attrStyle: {'text-align':'center'}},
                { name: "Actions", label: "Actions", action: true }
            ]
        }
    }

})();