
(function () {
    'use strict';

    angular.module('DNet.pages.scheduler')
        .controller('scheduleFormCtrl', scheduleFormCtrl)

    scheduleFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'ScheduleService'];
    function scheduleFormCtrl($scope, $state, $stateParams, ScheduleService) {
        var vm = $scope
        vm.listOfScheduleTypeOptions = []
        vm.listOfScheduleDayOptions = []
        vm.listOfDealerOptions = []
        vm.schedule = {}

        ScheduleService.GetOptions()
            .then(function (response) {
                vm.listOfScheduleTypeOptions = response.Data.ScheduleTypeOptions;
                vm.listOfScheduleDayOptions = response.Data.ScheduleDayOptions;
                vm.listOfDealerOptions = response.Data.DealerOptions;
            }, function (error) { });

        if ($stateParams.id) {
            ScheduleService.GetById($stateParams.id)
                .then(function (response) {
                    vm.schedule = response.Data;
                }, function (error) { });
        }

        vm.Submit = function () {
            if (vm.schedule.Id == null) {
                ScheduleService.Create(vm.schedule)
                    .then(function (response) {
                        $state.go('scheduler.schedule.list')
                    }, function (error) {
                    });
            } else {
                ScheduleService.Update(vm.schedule)
                    .then(function (response) {
                        $state.go('scheduler.schedule.list')
                    }, function (error) {
                    });
            }
            vm.editmode = 0;
        }
    }

})();