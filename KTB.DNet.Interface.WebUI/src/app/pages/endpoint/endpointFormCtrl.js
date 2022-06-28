
(function () {
    'use strict';

    angular.module('DNet.pages.endpoint')
        .controller('endpointFormCtrl', endpointFormCtrl)

    endpointFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'EndpointPermissionService'];
    function endpointFormCtrl($scope, $state, $stateParams, EndpointPermissionService) {
        var vm = $scope
        vm.transactionTypeOptions = [];
        vm.operationTypeOptions = [];
        vm.endpointGroupOptions = [];
        vm.permissionCodeOptions = [];
        vm.endpointPermission = {};
        vm.usePermissionCodeOption = false;
        vm.isEditMode = false;
        

        EndpointPermissionService.GetOptions()
            .then(function (response) {
                vm.transactionTypeOptions = response.Data.ListOfEndpointTypeOptions;
                vm.operationTypeOptions = response.Data.ListOfOperationTypeOptions;
                vm.endpointGroupOptions = response.Data.ListOfEndpointGroupOptions
                vm.permissionCodeOptions = response.Data.ListOfPermissionCodeOptions;
                if (vm.permissionCodeOptions != null && vm.permissionCodeOptions.length > 0)
                {
                    vm.usePermissionCodeOption = true;
                }
                
            }, function (error) { });

        if ($stateParams.id) {
            vm.isEditMode = true;
            EndpointPermissionService.GetById($stateParams.id)
                .then(function (response) {
                    vm.endpointPermission = response.Data;
                }, function (error) { });
        }

        vm.Submit = function () {
            if ($stateParams.id) {
                EndpointPermissionService.Update(vm.endpointPermission)
                    .then(function (response) {
                        $state.go('endpoint.list')
                    }, function (error) { });
            } else {
                EndpointPermissionService.Create(vm.endpointPermission)
                    .then(function (response) {
                        $state.go('endpoint.list')
                    }, function (error) { });
            }
        }
    }

})();