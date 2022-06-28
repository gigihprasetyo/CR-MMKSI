
(function () {
    'use strict';

    angular.module('DNet.pages.throttler')
        .controller('throttlerCtrl', throttlerCtrl)

    throttlerCtrl.$inject = ['$scope', 'ThrottlerService'];
    function throttlerCtrl($scope, ThrottlerService) {
        var vm = $scope;
        vm.throttler = {};
        vm.httpService = ThrottlerService;

        vm.Export = function () {
            ThrottlerService.Export()
                   .then(function (response) {
                   }, function (error) { });
        }

        vm.tableDefinition = {
            name: "throttler",
            key: "Id",
            identifier: "Endpoint.Name",
            customButtons: [
                {
                    action: vm.Export,
                    text: "Rewrite XML",
                    class: "btn btn-primary"
                }
            ],
            action: {
                create: "throttler.create",
                update: "throttler.update",
                delete: true
            },
            columns: [
                { name: "Endpoint.Name", label: "Name", sortColumnName: "APIEndpointPermission.Name" },
                { name: "Endpoint.URI", label: "URI", sortColumnName: "APIEndpointPermission.URI" },
                { name: "RequestLimit", label: "Request Limit", sortColumnName: "APIThrottle.RequestLimit" },
                { name: "TimeInSeconds", label: "Time (Seconds)", sortColumnName: "APIThrottle.TimeInSeconds" },
                { name: "Enable", label: "Active", datatype: "boolean", sortColumnName: "APIThrottle.Enable" },
                { name: "Actions", label: "Actions", action: true },
            ],
            tablePageSize: 10 // rows
        }

        vm.Export = function () {
            ThrottlerService.Export()
                   .then(function (response) {
                   }, function (error) { });
        }
    }

})();