
(function () {
    'use strict';

    angular.module('DNet.pages.configuration')
        .controller('systemConfigCtrl', systemConfigCtrl)

    systemConfigCtrl.$inject = ['$scope', '$rootScope', 'SystemConfigService'];
    function systemConfigCtrl($scope, $rootScope, SystemConfigService) {

        $scope.httpService = SystemConfigService;

        $scope.tableDefinition = {
            name: "system config",
            key: "Id",
            identifier: "Name",
            action: {},
            columns: [
                { name: "Name", label: "Name" },
                { name: "ConfigKey", label: "Config Key" },
                { name: "Value", label: "Value" },
                { name: "IsActive", label: "Active", datatype: "boolean", attrStyle: { 'text-align': 'center' } },
                { name: "Actions", label: "Actions", action: true },
            ],
            tablePageSize: 10 // rows
        }

        var allowCreate = $rootScope.userPermissions != null ? $rootScope.userPermissions.includes("WebUI_AppConfig_Create") : false;
        var allowUpdate = $rootScope.userPermissions != null ? $rootScope.userPermissions.includes("WebUI_AppConfig_Update") : false;
        var allowDelete = $rootScope.userPermissions != null ? $rootScope.userPermissions.includes("WebUI_AppConfig_Delete") : false;

        if (allowCreate) {
            $scope.tableDefinition.action.create = "configuration.systemConfig.create";
        }
        if (allowUpdate) {
            $scope.tableDefinition.action.update = "configuration.systemConfig.update";
        }
        if (allowDelete) {
            $scope.tableDefinition.action.delete = true;
        } else {
            $scope.tableDefinition.action.delete = false;
        }
    }

})();