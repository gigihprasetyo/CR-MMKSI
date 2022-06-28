
(function () {
    'use strict';

    angular.module('DNet.pages.accessControl')
        .controller('roleCtrl', roleCtrl);

    /** @ngInject */
    roleCtrl.$inject = ['$scope', 'RoleService'];
    function roleCtrl($scope, RoleService) {
        var vm = $scope
        vm.smartTablePageSize = 10;
        vm.httpService = RoleService;
        vm.role = {}

        vm.tableDefinition = {
            name: "role",
            key: "Id",
            identifier: "Name",
            action: {
                create: "accessControl.role.create",
                update: "accessControl.role.update",
                delete: true
            },
            columns: [
                { name: "Name", label: "Name" },
                { name: "Level", label: "Level" },
                { name: "Actions", label: "Actions", action: true },
            ],
            tablePageSize: 10 // rows
        }
    }

})();