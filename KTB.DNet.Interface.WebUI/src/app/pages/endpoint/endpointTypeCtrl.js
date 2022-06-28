
(function () {
    'use strict';

    angular.module('DNet.pages.endpoint')
        .controller('endpointTypeCtrl', endpointTypeCtrl)

    endpointTypeCtrl.$inject = ['$scope', 'EndpointPermissionService'];
    function endpointTypeCtrl($scope, EndpointPermissionService) {
        var vm = $scope
        vm.entity = {}
        vm.clearForm = _clearForm
        vm.Submit = _submit
        vm.onSelectEndpointTypeCallback = _onSelectEndpointTypeCallback
        
        EndpointPermissionService.GetOptions()
            .then(function (response) {
                vm.listOfEndpointType = response.Data.ListOfEndpointTypeOptions;
            }, function (error) { });

        function _onSelectEndpointTypeCallback() {
            console.log(vm.entity.EndpointTypeId);
            populateEndpointOptions();
            EndpointPermissionService.GetEndpointsByEndpointType(vm.entity.EndpointTypeId)
                .then(function (response) {
                    vm.entity.EndpointIds = response.Data;
                }, function (error) { });
        };

        function populateEndpointOptions() {
            EndpointPermissionService.GetAllPermission()
                .then(function (response) {
                    console.log(response);
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
            EndpointPermissionService.SaveEndpointType(vm.entity)
                .then(function (response) {
                }, function (error) {
                });
            vm.editmode = 0;
        }
    }

})();