(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')
        .directive('latestErrorLog', latestErrorLog);

    /** @ngInject */
    function latestErrorLog() {
        var latestErrorLogCtrl = ['$scope', '$timeout', '$state', 'ErrorLogService',
        function ($scope, $timeout, $state, ErrorLogService) {
            var vm = $scope;;
            vm.LatestErrors = {};
            vm.appName = '';
            vm.severity = -1;            
            vm.view = _view;
            vm.listOfApplicationNames = [];
            vm.ddlAppChange = _ddlAppChange;
            vm.ddlAppSalected = 'Application Name';
            vm.listOfseverity = [
                { label: "Severity", value: -1 },
                { label: "Log", value: 0 },
                { label: "Warning", value: 404 },
                { label: "Fatal", value: 500 }
            ];
            vm.ddlSevChange = _ddlSevChange;
            vm.ddlSevSalected = 'Severity';

            ErrorLogService.GetApplicationLogList()
            .then(function (response) {
                if (response.Data != null) {
                    vm.listOfApplicationNames = response.Data;
                }
            }, function (error) { });

            _renderInfo();

            function _renderInfo() {

                ErrorLogService.GetLatestErrorLog(6, vm.appName, vm.severity).
                then(function (response) {
                    if (response.Data == null || response.Data.length < 1) {
                        vm.LatestErrors = {};
                        $("#errorMsg").html("<div class='alert bg-info'><strong>There's no latest error for " + vm.ddlAppSalected + "and " + vm.ddlSevSalected + "...</strong></div>");
                        return;
                    }

                    vm.LatestErrors = response.Data;
                    $("#errorMsg").html("");

                    $timeout(_renderInfo, 90000); 
                }, function () { });
            }

            function _view(errorId, application) {
                $state.go("auditLog.elmahlog.view", { id: errorId, app: application });
            }

            function _ddlAppChange(item) {
                vm.ddlAppSalected = item.label;
                vm.appName = item.value;
                _renderInfo();
            }

            function _ddlSevChange(item) {
                vm.ddlSevSalected = item.label;
                vm.severity = item.value;
                _renderInfo();
            }
        }];

        return {
            restrict: 'E',
            controller: latestErrorLogCtrl,
            templateUrl: 'src/app/pages/auditLog/elmahErrorLogs/latestErrorLog/latestErrorLog.html'
        };
    }
})();