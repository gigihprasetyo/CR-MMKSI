
(function () {
    'use strict';

    angular.module('DNet.pages.throttler')
        .controller('throttlerFormCtrl', throttlerFormCtrl)

    throttlerFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'ThrottlerService'];
    function throttlerFormCtrl($scope, $state, $stateParams, ThrottlerService) {

        $scope.endpointOptions = [];
        $scope.selectedEndpoint = {};
        $scope.throttle = {};



        if ($stateParams.id) {
            ThrottlerService.GetById($stateParams.id)
                .then(function (response) {
                    $scope.throttle = response.Data;
                    $scope.selectedEndpoint = $scope.throttle.Endpoint;
                    $scope.endpointOptions.push($scope.throttle.Endpoint);
                }, function (error) { });
        }
        else {
            ThrottlerService.GetOptions()
                .then(function (response) {
                    $scope.endpointOptions = response.Data.EndpointOptions;
                }, function (error) { });
        }

        $scope.Submit = function () {
            $scope.throttle.EndpointId = $scope.selectedEndpoint.Id;

            if ($stateParams.id) {
                ThrottlerService.Update($scope.throttle)
                    .then(function (response) {
                    }, function (error) { });
            } else {
                ThrottlerService.Create($scope.throttle)
                    .then(function (response) {
                        $state.go('throttler.list')
                    }, function (error) { });
            }
        }
    }

})();