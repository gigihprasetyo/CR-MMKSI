
(function () {
    'use strict';

    angular.module('DNet.pages.endpoint')
        .controller('operationTypeCtrl', operationTypeCtrl)

    operationTypeCtrl.$inject = ['$scope', 'EndpointPermissionService'];
    function operationTypeCtrl($scope, EndpointPermissionService) {
        var vm = $scope
        vm.entity = {}
        vm.clearForm = _clearForm
        vm.Submit = _submit
        vm.onSelectOperationTypeCallback = _onSelectOperationTypeCallback
        
        EndpointPermissionService.GetOptions()
            .then(function (response) {
                vm.listOfOperationType = response.Data.ListOfOperationTypeOptions;
            }, function (error) { });

        function _onSelectOperationTypeCallback() {
            populateEndpointOptions();
            EndpointPermissionService.GetEndpointsByOperationType(vm.entity.OperationTypeId)
                .then(function (response) {
                    vm.entity.EndpointIds = response.Data;
                }, function (error) { });
        };

        function populateEndpointOptions() {
            EndpointPermissionService.GetAllPermission()
                .then(function (response) {
                    vm.listOfEndpoints = response.Data;
                }, function (error) { });
        };

        function _clearForm(){
            vm.entity.EndpointIds = [];
            vm.entity.EndpointId = "";
            vm.listOfEndpointType = [];
            vm.listOfEndpoints = [];
        }

        function _submit() {
            EndpointPermissionService.SaveOperationType(vm.entity)
                .then(function (response) {
                }, function (error) {
                });
            vm.editmode = 0;
        }
    }

})();