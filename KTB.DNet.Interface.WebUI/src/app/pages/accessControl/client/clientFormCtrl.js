
(function () {
    'use strict';

    angular.module('DNet.pages.accessControl')
        .controller('clientFormCtrl', clientFormCtrl)

    clientFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'ClientService'];
    function clientFormCtrl($scope, $state, $stateParams, ClientService) {
        var vm = $scope
        vm.client = {}
        vm.listOfApplication = []
        vm.listOfPermission = []
        vm.listOfRole = []

        ClientService.GetAppOptions()
            .then(function (response) {
                vm.listOfApplication = response.Data
            }, function (error) { });

        vm.onSelectCallback = function () {
            populateOptions(vm.client.AppId)
        };

        if ($stateParams.id) {
            ClientService.GetById($stateParams.id)
                .then(function (response) {
                    vm.client = response.Data;
                    populateOptions(vm.client.AppId)
                }, function (error) { });
        }

        function populateOptions(appId) {
            ClientService.GetOptions(appId)
                .then(function (response) {
                    vm.listOfPermission = response.Data.ListOfPermission;
                    vm.listOfRole = response.Data.ListOfRole;
                }, function (error) { });
        }

        vm.Submit = function () {
            if (vm.client.ClientId == null) {
                ClientService.Create(vm.client)
                    .then(function (response) {
                        $state.go('accessControl.client.list')
                    }, function (error) {
                    });
            } else {
                ClientService.Update(vm.client)
                    .then(function (response) {
                        $state.go('accessControl.client.list')
                    }, function (error) {
                    });
            }
            vm.editmode = 0;
        }
    }

})();