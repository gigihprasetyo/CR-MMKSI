
(function () {
    'use strict';

    angular.module('DNet.pages.scheduler')
        .controller('endpointScheduleCtrl', endpointScheduleCtrl)

    endpointScheduleCtrl.$inject = ['$scope', 'UIHelper', 'EndpointScheduleService', 'EndpointPermissionService', 'ModalHelper'];
    function endpointScheduleCtrl($scope, UIHelper, EndpointScheduleService, EndpointPermissionService, ModalHelper) {
        var vm = $scope;
        vm.endpointPermission = {};
        vm.endpointScheduleService = EndpointScheduleService;
        vm.customRenderData = _renderData
        vm.endpointScheduleTableState = null
        vm.AssignSchedule = _assignSchedule
        vm.entity = {};
        vm.entity.EndpointPermissionId = null;
        vm.entity.ListOfSelectedScheduleId = [];
        vm.listOfEndpointTypeOptions = {};
        vm.listOfOperationTypeOptions = {};
        vm.listOfScheduleTypeOptions = {};
        vm.listOfScheduleDayOptions = {};

        vm.getTableDefinition = _getTableDefinition;
        vm.getEndpointScheduleTableDefinition = _getEndpointScheduleTableDefinition;

        vm.tableDefinition = vm.getTableDefinition();
        vm.endpointScheduleTableDefinition = vm.getEndpointScheduleTableDefinition();

        EndpointScheduleService.GetOptions()
            .then(function (response) {
                vm.listOfEndpointTypeOptions = response.Data.ListOfTransactionTypeOptions;
                vm.listOfOperationTypeOptions = response.Data.ListOfOperationTypeOptions;
                vm.listOfScheduleTypeOptions = response.Data.ListOfScheduleTypeOptions;
                vm.listOfScheduleDayOptions = response.Data.ListOfScheduleDayOptions;
                vm.tableDefinition = vm.getTableDefinition();
                vm.endpointScheduleTableDefinition = vm.getEndpointScheduleTableDefinition();

            }, function (error) { });

        vm.onSelectRow = function (row, tableState) {
            vm.entity.EndpointPermissionId = row.Id
            _refreshAll()
        }

        function _getTableDefinition() {
            return {
                name: "Assigned Endpoint Permission",
                key: "Id",
                identifier: "Name",
                action: {},
                columns: [
                    { name: "Name", label: "Name" },
                    { name: "PermissionCode", label: "PermissionCode" },
                    { name: "URI", label: "URI" },
                    { name: "EndpointType", label: "Type", datatype: "option", options: vm.listOfEndpointTypeOptions, optionKey: "Value", optionText: "Text" },
                    { name: "OperationType", label: "Operation", datatype: "option", options: vm.listOfOperationTypeOptions, optionKey: "Value", optionText: "Text" },
                    { name: "Description", label: "Description" },
                    { name: "Action", label: "" }
                ],
                tablePageSize: 10
            }
        }

        function _getEndpointScheduleTableDefinition() {
            return {
                name: "Endpoint Schedule",
                key: "Id",
                identifier: "Schedule.Name",
                action: {
                    delete: { customDelete: _deleteEndpointSchedule }
                },
                columns: [
                    { name: "Schedule.Name", label: "Name", sortColumnName:"APISchedule.Name" },
                    { name: "Schedule.ScheduleType", label: "Type", datatype: "option", options: vm.listOfScheduleTypeOptions, optionKey: "Value", optionText: "Text", sortColumnName:"APISchedule.ScheduleType" },
                    { name: "Schedule.ScheduleDay", label: "ScheduleDay", datatype: "option", options: vm.listOfScheduleDayOptions, optionKey: "Value", optionText: "Text", sortColumnName:"APISchedule.ScheduleDay" },
                    { name: "Schedule.MonthDay", label: "MonthDay", sortColumnName:"APISchedule.MonthDay" },
                    { name: "Schedule.ScheduleTime", label: "Time", sortColumnName:"APISchedule.ScheduleTime" },
                    { name: "Schedule.Interval", label: "Interval", sortColumnName:"APISchedule.Interval" },
                    { name: "Schedule.DealerCode", label: "DealerCode", sortColumnName:"APISchedule.DealerCode" },
                    { name: "Actions", label: "Actions", action: true },
                ],
                tablePageSize: 10
            }
        }

        function _renderData(tableState) {
            vm.endpointScheduleTableState = tableState;
            if (vm.entity.EndpointPermissionId) {
                // $scope.isLoading = true;
                var params = UIHelper.convertTableStateToSearchParams(tableState);
                EndpointScheduleService.Search(params, vm.entity.EndpointPermissionId)
                    .then(function (response) {
                        vm.endpointScheduleTableDefinition.dataSource = response.Records
                        tableState.pagination.numberOfPages = response.numberOfPages;
                        vm.displaySchedule = true;
                    }, function (error) { });
            }
        }

        function _getUnassignedSchedule() {
            EndpointScheduleService.GetUnassignedSchedule(vm.entity.EndpointPermissionId)
                .then(function (response) {
                    vm.listOfUnassignedSchedules = response.Data;
                }, function (error) { });
        }

        function _assignSchedule() {
            EndpointScheduleService.AddBulkEndpointSchedule(vm.entity)
                .then(function (response) {
                    //ModalHelper.showMessage(response.Message, "success");
                    _refreshAll()
                }, function (error) { });
        }

        function _refreshAll() {
            _renderData(vm.endpointScheduleTableState);
            _getUnassignedSchedule(vm.entity.EndpointPermissionId);
            vm.entity.ListOfSelectedScheduleId = []
        }

        function _deleteEndpointSchedule(row) {
            EndpointScheduleService.Delete(row.Id)
                .then(function (response) {
                    // ModalHelper.showMessage(response.Message, "success");
                    _refreshAll()
                }, function (error) { });
        }

    }

})();