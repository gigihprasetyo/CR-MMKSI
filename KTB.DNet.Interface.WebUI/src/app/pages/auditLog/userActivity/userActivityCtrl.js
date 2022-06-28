
(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')
        .controller('userActivityCtrl', userActivityCtrl)

    userActivityCtrl.$inject = ['$scope', '$state', 'UserActivityService', 'ModalHelper'];
    function userActivityCtrl($scope, $state, UserActivityService, ModalHelper) {
        var vm = $scope;
        vm.httpService = UserActivityService;
        vm.userActivity = {};
        vm.listOfUserActivityTypeOptions = {};
        vm.getTableDefinition = _getTableDefinition;
        vm.getColumnDefinition = _getColumnDefinition;
        vm.tableDefinition = vm.getTableDefinition();

        UserActivityService.GetOptions()
            .then(function (response) {
                
                if (response.Data != null) {
                    vm.listOfUserActivityTypeOptions = response.Data.ListOfUserActivityTypeOptions;

                    vm.tableDefinition.columns = vm.getColumnDefinition();
                }

            }, function (error) { });
        function _getTableDefinition() {
            return {
                name: "userActivity",
                key: "Id",
                identifier: "Endpoint",
                action: {
                    customActions: [
                        { text: "Detail", type: "primary", action: _view }
                    ]
                },
                columns: vm.getColumnDefinition(),
                tablePageSize: 20 // rows
            }
        }

        function _getColumnDefinition() {
            return [
                    { name: "Id", label: "ID" },
                    { name: "Username", label: "Username" },
                    { name: "Endpoint", label: "Endpoint" },
                    { name: "Activity", label: "Activity", datatype: "option", options: vm.listOfUserActivityTypeOptions, optionKey: "Value", optionText: "Text" },
                    { name: "ActivityTime", label: "Activity Time", datatype: "date", format: "yyyy-MM-dd HH:mm:ss" },
                    { name: "DealerCode", label: "Dealer Code" },
                    { name: "Actions", label: "Actions", action: true }
            ];
        }

        function _view(row) {
            var msg = {};
            msg.title = "User Activity";
            msg.content = row.ActivityDesc
            ModalHelper.showMessage(msg, "info");
        }


    }

})();