
(function () {
    'use strict';

    angular.module('DNet.pages.configuration')
        .controller('standardCodeCharFormCtrl', standardCodeCharFormCtrl)

    standardCodeCharFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'StandardCodeCharService'];
    function standardCodeCharFormCtrl($scope, $state, $stateParams, StandardCodeCharService) {
        var vm = $scope
        vm.standardCodeChar = {}
        vm.isEditMode = false;
        
        if ($stateParams.id) {
            vm.isEditMode = true;
            StandardCodeCharService.GetById($stateParams.id)
            .then(function (response) {
                vm.standardCodeChar = response.Data;
            }, function (error) {
            });
        }

        vm.Submit = function () {
            if (vm.standardCodeChar.ID == null) {
                StandardCodeCharService.Create(vm.standardCodeChar)
                    .then(function (response) {
                        $state.go('configuration.standardCodeChar.list')
                    }, function (error) {

                    });
            } else {
                StandardCodeCharService.Update(vm.standardCodeChar)
                    .then(function (response) {
                        $state.go('configuration.standardCodeChar.list')
                    }, function (error) {

                    });
            }
        }
    }

})();