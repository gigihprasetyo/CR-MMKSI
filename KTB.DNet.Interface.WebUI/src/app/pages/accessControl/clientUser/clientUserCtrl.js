
(function () {
    'use strict';

    angular.module('DNet.pages.accessControl')
        .controller('clientUserCtrl', clientUserCtrl)

    clientUserCtrl.$inject = ['$scope', 'ClientRolePermissionService', 'ClientUserService', 'UserService'];
    function clientUserCtrl($scope, ClientRolePermissionService, ClientUserService, UserService) {
        var vm = $scope
        vm.entity = {}
        vm.clearForm = _clearForm
        vm.Submit = _submit
        vm.onSelectClientCallback = _onSelectClientCallback
        
        ClientRolePermissionService.GetClientOptions()
            .then(function (response) {
                vm.listOfClients = response.Data
            }, function (error) { });

        function _onSelectClientCallback() {
            console.log(vm.entity);
            populateUserOptions(vm.entity.ClientId);
            UserService.GetUsersByClientId(vm.entity.ClientId)
                .then(function (response) {
                    console.log(response);
                    vm.entity.UserIds = response.Data;
                }, function (error) { });
        };

        function populateUserOptions(clientId) {
            UserService.GetUnassignedUsers(clientId)
                .then(function (response) {
                    console.log(response);
                    vm.listOfUsers = response.Data;
                }, function (error) { });
        };

        function _clearForm(){
            vm.entity.UserIds = [];
            vm.entity.ClientId = "";
            vm.listOfClients = [];
            vm.listOfUsers = [];
        }

        function _submit() {
            ClientUserService.SaveClientUser(vm.entity)
                .then(function (response) {
                }, function (error) {
                });
            vm.editmode = 0;
        }

    }

})();