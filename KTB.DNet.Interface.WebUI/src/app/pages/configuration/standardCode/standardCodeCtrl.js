
(function () {
    'use strict';

    angular.module('DNet.pages.configuration')
        .controller('standardCodeCtrl', standardCodeCtrl)

    standardCodeCtrl.$inject = ['$scope', '$rootScope', 'StandardCodeService'];
    function standardCodeCtrl($scope, $rootScope, StandardCodeService) {

        $scope.httpService = StandardCodeService;

        $scope.tableDefinition = {
            name: "standard code",
            key: "ID",
            identifier: "ValueCode",
            action: {
                delete: false,
                customActions: [
                    { text: "Enable", type: "primary", customText: _enableOrDisableButtonText, customType: _enableOrDisableButtonType, action: _enableOrDisableStandardCode }
                ]
            },
            columns: [
                { name: "Category", label: "Category" },
                { name: "ValueId", label: "ValueId" },
                { name: "ValueCode", label: "ValueCode" },
                { name: "ValueDesc", label: "ValueDesc" },
                { name: "Sequence", label: "Sequence" },
                { name: "RowStatus", label: "RowStatus" },
                { name: "Actions", label: "Actions", action: true },
            ],
            tablePageSize: 10 // rows
        }

        var allowCreate = $rootScope.userPermissions != null ? $rootScope.userPermissions.includes("WebUI_StandardCode_Create") : false;
        var allowUpdate = $rootScope.userPermissions != null ? $rootScope.userPermissions.includes("WebUI_StandardCode_Update") : false;

        if (allowCreate) {
            $scope.tableDefinition.action.create = "configuration.standardCode.create";
        }
        if (allowUpdate) {
            $scope.tableDefinition.action.update = "configuration.standardCode.update";
        }

        function _enableOrDisableStandardCode(row) {
            row.RowStatus = row.RowStatus == -1 ? 0 : -1;
            $scope.httpService.Update(row)
                .then(function (response) {

                }, function (error) {

                });
        }

        function _enableOrDisableButtonType(row) {
            if (row.RowStatus == -1) {
                return "success";
            }
            return "danger";
        }

        function _enableOrDisableButtonText(row) {
            if (row.RowStatus == -1) {
                return "Enable";
            }
            return "Disable";
        }
    }

})();