
(function () {
    'use strict';

    angular.module('DNet.pages.accessControl')
        .controller('applicationCtrl', applicationCtrl)

    applicationCtrl.$inject = ['$scope', 'ApplicationService', 'UIHelper'];
    function applicationCtrl($scope, ApplicationService, UIHelper) {
        var vm = $scope
        vm.httpService = ApplicationService;
        vm.application = {}

        vm.tableDefinition = {
            name: "applications",
            key: "AppId",
            identifier: "Name",
            action: {
                create: "accessControl.application.create",
                update: "accessControl.application.update",
                delete: true
            },
            columns: [
                { name: "AppId", label: "Application ID" },
                { name: "Name", label: "Name" },
                { name: "DeploymentJenkinsJobName", label: "Jenkins Job Name" },
                { name: "Actions", label: "Actions", action: true },
            ],
            tablePageSize: 10 // rows
        }


    }

})();