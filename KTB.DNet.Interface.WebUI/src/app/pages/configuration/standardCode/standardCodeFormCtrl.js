
(function () {
    'use strict';

    angular.module('DNet.pages.configuration')
        .controller('standardCodeFormCtrl', standardCodeFormCtrl)

    standardCodeFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'StandardCodeService'];
    function standardCodeFormCtrl($scope, $state, $stateParams, StandardCodeService) {
        var vm = $scope
        vm.standardCode = {}
        vm.isEditMode = false;
        
        if ($stateParams.id) {
            vm.isEditMode = true;
            StandardCodeService.GetById($stateParams.id)
            .then(function (response) {
                vm.standardCode = response.Data;
            }, function (error) {
            });
        }

        vm.Submit = function () {
            if (vm.standardCode.ID == null) {
                StandardCodeService.Create(vm.standardCode)
                    .then(function (response) {
                        $state.go('configuration.standardCode.list')
                    }, function (error) {

                    });
            } else {
                StandardCodeService.Update(vm.standardCode)
                    .then(function (response) {
                        $state.go('configuration.standardCode.list')
                    }, function (error) {

                    });
            }
        }
    }

})();