
(function () {
    'use strict';

    angular.module('DNet.pages.endpoint')
        .controller('endpointGroupCtrl', endpointGroupCtrl)

    endpointGroupCtrl.$inject = ['$scope', 'EndpointPermissionService'];
    function endpointGroupCtrl($scope, EndpointPermissionService) {
        var vm = $scope
        vm.entity = {}
        vm.clearForm = _clearForm
        vm.Submit = _submit
        vm.onSelectEndpointGroupCallback = _onSelectEndpointGroupCallback
        
        EndpointPermissionService.GetEndpointGroupOptions()
            .then(function (response) {
                vm.listOfEndpointGroup = response.Data
            }, function (error) { });

        function _onSelectEndpointGroupCallback() {
            populateEndpointOptions(vm.entity.EndpointGroupId);
            EndpointPermissionService.GetEndpointsByEndpointGroup(vm.entity.EndpointGroupId)
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
            vm.listOfEndpointGroup = [];
            vm.listOfEndpoints = [];
        }

        function _submit() {
            EndpointPermissionService.SaveEndpointPermissionGroup(vm.entity)
                .then(function (response) {
                }, function (error) {
                });
            vm.editmode = 0;
        }
    }

})();