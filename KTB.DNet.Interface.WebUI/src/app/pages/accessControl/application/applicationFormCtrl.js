
(function () {
    'use strict';

    angular.module('DNet.pages.accessControl')
        .controller('applicationFormCtrl', applicationFormCtrl)

    applicationFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'ApplicationService'];
    function applicationFormCtrl($scope, $state, $stateParams, ApplicationService) {
        var vm = $scope
        vm.listOfPermission = []
        vm.application = {}

        ApplicationService.GetPermissionOptions()
            .then(function (response) {
                vm.listOfPermission = response.Data
            }, function (error) { });

        ApplicationService.GetJenkinsJobOptions()
            .then(function (response) {
                vm.listOfJenkinsJobs = response.Data
            }, function (error) { });

        if ($stateParams.id) {
            ApplicationService.GetById($stateParams.id)
                .then(function (response) {
                    vm.application = response.Data;
                }, function (error) { });
        }

        vm.Submit = function () {
            if (vm.application.AppId == null) {
                ApplicationService.Create(vm.application)
                    .then(function (response) {
                        $state.go('accessControl.application.list')
                    }, function (error) {
                    });
            } else {
                ApplicationService.Update(vm.application)
                    .then(function (response) {
                        $state.go('accessControl.application.list')
                    }, function (error) {
                    });
            }
            vm.editmode = 0;
        }
    }

})();