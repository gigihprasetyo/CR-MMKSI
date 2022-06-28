
(function () {
    'use strict';

    angular.module('DNet.pages.configuration')
        .controller('systemConfigFormCtrl', systemConfigFormCtrl)

    systemConfigFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'SystemConfigService'];
    function systemConfigFormCtrl($scope, $state, $stateParams, SystemConfigService) {
        var vm = $scope
        vm.systemConfig = {}
        vm.isEditMode = false;

        SystemConfigService.GetOptions()
            .then(function (response) {
                vm.dataTypeOptions = response.Data.DataTypeOptions;
                vm.configKeyOptions = response.Data.ConfigKeyOptions;
                GetExistingData();
            }, function (error) { });

        function GetExistingData() {
            if ($stateParams.id) {
                    vm.isEditMode = true;
                    SystemConfigService.GetById($stateParams.id)
                    .then(function (response) {
                        vm.systemConfig = response.Data;
                        SetDisplayedValue();
                    }, function (error) {
                    });
            }
        }

        function SetDisplayedValue() {
            switch(vm.systemConfig.DataType)
            {
                case 2:
                    // if the value is a number
                    vm.systemConfig.DisplayedValue = parseInt(vm.systemConfig.Value);
                    break;
                case 3:
                    // if the value is a boolean
                    vm.systemConfig.DisplayedValue = (vm.systemConfig.Value === "true" || vm.systemConfig.Value === "True")
                    break;
                default:
                    vm.systemConfig.DisplayedValue = vm.systemConfig.Value;
                    break;
            }
        }
        

        vm.Submit = function () {
            vm.systemConfig.Value = vm.systemConfig.DisplayedValue.toString();
            if (vm.systemConfig.Id == null) {
                SystemConfigService.Create(vm.systemConfig)
                    .then(function (response) {
                        $state.go('configuration.systemConfig.list')
                    }, function (error) {

                    });
            } else {
                SystemConfigService.Update(vm.systemConfig)
                    .then(function (response) {
                        $state.go('configuration.systemConfig.list')
                    }, function (error) {

                    });
            }
        }
    }

})();