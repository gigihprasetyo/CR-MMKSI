/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
    'use strict';

    angular.module('DNet.pages.dashboard')
        .controller('DashboardMainInfoCtrl', DashboardMainInfoCtrl);

    /** @ngInject */
	DashboardMainInfoCtrl.$inject = ['$scope', 'DashboardService'];
    function DashboardMainInfoCtrl($scope, DashboardService) {
        var vm = $scope;
        vm.httpService = DashboardService;
        vm.infoboxes = [];
        vm.setInfoboxes = _setInfoboxes;
        vm.dashboardInfo = {};
        vm.numberOfBoxes = 5;

        DashboardService.GetInfoBoxes()
            .then(function (response) {
                vm.dashboardInfo = response.Data;
                vm.infoboxes = vm.setInfoboxes(vm.dashboardInfo.IsDMSAdmin);
                if (!vm.dashboardInfo.IsDMSAdmin) {
                    vm.numberOfBoxes = 4;
                }
            }, function (error) { });


        function _setInfoboxes(userType) {
            var infoboxes = [{
                description: 'Users',
                stats: vm.dashboardInfo.UserCount,
                icon: 'ion-person',
                show: true
            }, {
                description: 'Clients',
                stats: vm.dashboardInfo.ClientCount,
                icon: 'ion-briefcase',
                show: userType
            }, {
                description: 'Roles',
                stats: vm.dashboardInfo.RoleCount,
                icon: 'ion-ios-people',
                show: true
            }, {
                description: 'Permissions',
                stats: vm.dashboardInfo.PermissionCount,
                icon: 'ion-key',
                show: true
            }, {
                description: 'Dealers',
                stats: vm.dashboardInfo.DealerCount,
                icon: 'ion-android-car',
                show: true
            }
            ]

            if (!userType) {
                infoboxes.splice(1, 1);
            }

            return infoboxes;
            ;
        }
    }
})();