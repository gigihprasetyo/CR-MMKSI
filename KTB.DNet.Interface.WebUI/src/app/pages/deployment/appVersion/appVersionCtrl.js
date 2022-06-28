
(function () {
    'use strict';

    angular.module('DNet.pages.deployment')
        .controller('appVersionCtrl', appVersionCtrl);

    /** @ngInject */
    appVersionCtrl.$inject = ['$scope', 'AppVersionService', 'UIHelper'];
    function appVersionCtrl($scope, AppVersionService, UIHelper) {
        var vm = $scope
        vm.httpService = AppVersionService;
        vm.appVersion = {}

        vm.tableDefinition = {
            name: "application version",
            key: "VersionId",
            identifier: "Version",
            action: {
                create: "deployment.appVersion.create",
                update: "deployment.appVersion.update",
                delete: true
            },
            columns: [
                { name: "MsApplication.Name", label: "Application Name", sortColumnName: "MsApplication.Name" },
                { name: "Version", label: "Version", sortColumnName: "MsAppVersion.Version" },
                { name: "Description", label: "Description", sortColumnName: "MsAppVersion.Description" },
                { name: "IsCurrentDeployment", label: "Current Deployment", sortColumnName: "MsAppVersion.IsCurrentDeployment", datatype: "boolean" },
                { name: "Actions", label: "Actions", action: true }
            ],
            tablePageSize: 10 // rows
        }


    }

})();