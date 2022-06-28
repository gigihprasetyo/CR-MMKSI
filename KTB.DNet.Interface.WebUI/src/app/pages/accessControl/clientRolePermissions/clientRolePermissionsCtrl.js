
(function () {
    'use strict';

    angular.module('DNet.pages.accessControl')
        .controller('clientRolePermissionsCtrl', clientRolePermissionsCtrl)

    clientRolePermissionsCtrl.$inject = ['$scope', 'ClientRolePermissionService'];
    function clientRolePermissionsCtrl($scope, ClientRolePermissionService) {
        var vm = $scope
        vm.entity = {}
        vm.clearForm = _clearForm
        vm.Submit = _submit
        vm.onSelectClientCallback = _onSelectClientCallback
        vm.onSelectClientRoleCallback = _onSelectClientRoleCallback
        
        ClientRolePermissionService.GetClientOptions()
            .then(function (response) {
                vm.listOfClients = response.Data
            }, function (error) { });

        function _onSelectClientCallback() {
            _clearForm()
            populateClientRoleOptions(vm.entity.ClientId)
        };

        function populateClientRoleOptions(clientId) {
            ClientRolePermissionService.GetRolesByClientId(clientId)
                .then(function (response) {
                    vm.listOfClientRoles = response.Data;
                }, function (error) { });
        }

        function _onSelectClientRoleCallback() {
            populatePermissionOptions(vm.entity.ClientId)

            ClientRolePermissionService.GetSelectedPermissionIdsByClientRoleId(vm.entity.ClientRoleId)
                .then(function (response) {
                    vm.entity.PermissionIds = response.Data;
                }, function (error) { });

        };

        function populatePermissionOptions(clientId) {
            ClientRolePermissionService.GetAllPermissionsByClientId(clientId)
                .then(function (response) {
                    vm.listOfPermissions = response.Data;
                }, function (error) { });
        }

        function _clearForm(){
            vm.entity.PermissionIds = [];
            vm.entity.ClientRoleId = "";
            vm.listOfClientRoles = [];
            vm.listOfPermissions = [];
        }

        function _submit() {
            ClientRolePermissionService.UpdateClientRole(vm.entity)
                .then(function (response) {
                }, function (error) {
                });
            vm.editmode = 0;
        }

    }

})();