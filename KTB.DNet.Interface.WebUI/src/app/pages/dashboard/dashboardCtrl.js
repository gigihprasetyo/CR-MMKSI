
(function () {
    'use strict';

    angular.module('DNet.pages.dashboard')
        .controller('dashboardCtrl', dashboardCtrl)

    dashboardCtrl.$inject = ['$rootScope', '$scope', 'DashboardService'];
    function dashboardCtrl($rootScope, $scope, DashboardService) {
        var dnetuic = angular.fromJson($rootScope.dnetuic);
        var loggedIn = dnetuic && dnetuic.currentUser;
        $scope.isDMSAdmin = false;

        if(loggedIn)
        {
            $scope.isDMSAdmin = dnetuic.currentUser.isDMSAdmin;
            $scope.dealerCode = $scope.isDMSAdmin ? "" : dnetuic.currentUser.dealerCode;
        }
    }

})();