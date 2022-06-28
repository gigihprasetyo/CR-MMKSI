
(function () {
    'use strict';

    angular.module('DNet.pages.throttler', [])
        .config(routeConfig);

    /** @ngInject */
    routeConfig.$inject = ['$stateProvider'];
    function routeConfig($stateProvider) {
        $stateProvider
            .state('throttler', {
                url: '/throttler',
                permissionCodes: ["WebUI_Throttle_Read"],
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                redirect: 'throttler.list',
                abstract: true,
                title: 'Throttler',
                sidebarMeta: {
                    icon: 'ion-flash-off',
                    order: 60
                }
            })

            .state('throttler.list', {
                url: '',
                templateUrl: 'src/app/pages/throttler/throttler.html',
                title: 'Throttlers',
                controller: 'throttlerCtrl',
                breadchrumb: {
                    text: 'List'
                }
            })

            .state('throttler.create', {
                url: '/create',
                templateUrl: 'src/app/pages/throttler/throttlerForm.html',
                title: 'Create Throttler',
                controller: 'throttlerFormCtrl',
                breadchrumb: {
                    text: 'Create'
                }
            })
            .state('throttler.update', {
                url: '/:id',
                templateUrl: 'src/app/pages/throttler/throttlerForm.html',
                title: 'Update Throttler',
                controller: 'throttlerFormCtrl',
                breadchrumb: {
                    text: 'Update'
                }
            })
    }

})();




