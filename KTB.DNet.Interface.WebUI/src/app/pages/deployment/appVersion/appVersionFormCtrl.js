
(function () {
    'use strict';

    angular.module('DNet.pages.deployment')
        .controller('appVersionFormCtrl', appVersionFormCtrl)

    appVersionFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'AppVersionService'];
    function appVersionFormCtrl($scope, $state, $stateParams, AppVersionService) {
        $scope.appVersion = {}
        $scope.getOptions = _getOptions;

        $scope.getOptions();

        if ($stateParams.id) {
            AppVersionService.GetById($stateParams.id)
                .then(function (response) {
                    $scope.appVersion = response.Data;
                }, function (error) { });
        }

        function _getOptions() {
            AppVersionService.GetOptions()
                .then(function (response) {
                    $scope.appOptions = response.Data.AppOptions;
                }, function (error) { });
        }

        $scope.Submit = function () {
            if ($scope.appVersion.VersionId == null) {
                AppVersionService.Create($scope.appVersion)
                    .then(function (response) {
                        $state.go('deployment.appVersion.list')
                    }, function (error) {
                    });
            } else {
                AppVersionService.Update($scope.appVersion)
                    .then(function (response) {
                        $state.go('deployment.appVersion.list')
                    }, function (error) {
                    });
            }
            $scope.editmode = false;
        }
    }

})();