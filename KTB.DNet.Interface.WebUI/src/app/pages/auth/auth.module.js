
(function () {
    'use strict';

    angular.module('DNet.pages.auth', [])
        .config(routeConfig);

    /** @ngInject */
    routeConfig.$inject = ['$stateProvider'];
    function routeConfig($stateProvider) {
        $stateProvider
            .state('auth', {
                url: '/auth',
                template: '<ui-view autoscroll="true" autoscroll-body-top></ui-view>',
                abstract: true,
                title: 'Authentication',
                //   sidebarMeta: {
                //       icon: 'ion-ios-cloud-upload',
                //     order: 40,
                //   },
            })
            .state('auth.login', {
                url: '/login',
                templateUrl: 'src/app/pages/auth/login.html',
                title: 'Login',
                // sidebarMeta: {
                //     icon: 'ion-android-home',
                //     order: 0,
                // },
            })
            .state('auth.unauthorized', {
                url: '/unauthorized',
                templateUrl: 'src/app/pages/auth/unauthorized.html',
                title: 'Unauthorized'
            });
    }

})();




