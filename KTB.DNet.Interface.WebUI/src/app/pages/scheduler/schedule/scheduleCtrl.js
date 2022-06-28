
(function () {
    'use strict';

    angular.module('DNet.pages.scheduler')
        .controller('scheduleCtrl', scheduleCtrl)

    scheduleCtrl.$inject = ['$scope', 'ScheduleService'];
    function scheduleCtrl($scope, ScheduleService) {
        var vm = $scope;
        vm.httpService = ScheduleService;
        vm.schedule = {};
        vm.listOfScheduleTypeOptions = {};
        vm.listOfScheduleDayOptions = {};
        vm.getTableDefinition = _getTableDefinition;
        vm.getColumnDefinition = _getColumnDefinition;
        vm.tableDefinition = vm.getTableDefinition();

        ScheduleService.GetOptions()
            .then(function (response) {
                if(response.Data !== undefined){
                    vm.listOfScheduleTypeOptions = response.Data.ScheduleTypeOptions;
                    vm.listOfScheduleDayOptions = response.Data.ScheduleDayOptions;
                    vm.tableDefinition.columns = vm.getColumnDefinition();
                }
            }, function (error) { });

        function _getTableDefinition() {

            return {
                name: "schedule",
                key: "Id",
                identifier: "Name",
                action: {
                    create: "scheduler.schedule.create",
                    update: "scheduler.schedule.update",
                    delete: true,
                    refresh: true
                },
                columns: vm.getColumnDefinition(),
                tablePageSize: 10 // rows
            }
        }

        function _getColumnDefinition() {
            return [

                    { name: "Name", label: "Name" },
                    { name: "ScheduleType", label: "Schedule Type", datatype: "option", options: vm.listOfScheduleTypeOptions, optionKey: "Value", optionText: "Text" },
                    { name: "ScheduleDay", label: "Schedule Day", datatype: "option", options: vm.listOfScheduleDayOptions, optionKey: "Value", optionText: "Text" },
                    { name: "MonthDay", label: "Month Day" },
                    { name: "ScheduleTime", label: "Time (hh:mm:ss)" },
                    { name: "Interval", label: "Interval" },
                    { name: "DealerCode", label: "Dealer Code" },
                    { name: "Actions", label: "Actions", action: true },
            ]
        }
        
    }

})();