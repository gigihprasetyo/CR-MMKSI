/**
 * @author v.lugovsky
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.pages', [
    'ui.router',
    'ngCookies',
    'angular-loading-bar',
    'jq-multi-select',
    'moment-picker',
    'DNet.pages.auth',
    'DNet.pages.endpoint',
    'DNet.pages.dashboard',
    'DNet.pages.deployment',
    'DNet.pages.accessControl',
    'DNet.pages.scheduler',
    'DNet.pages.directives',
    'DNet.pages.configuration',
    'DNet.pages.auditLog',
    'DNet.pages.throttler',
    'DNet.pages.userProfile'
  ])
    .config(routeConfig)
    .config(['momentPickerProvider', function (momentPickerProvider) {
      momentPickerProvider.options({
        startView: 'month',
      });
    }])
    .run(run);


  run.$inject = ['config', '$transitions', '$rootScope', '$location', '$cookies', '$http', '$state', 'toastrConfig', 'AuthenticationService', 'ArrayHelper', 'PendingRequest', 'ModalHelper'];
  function run(config, $transitions, $rootScope, $location, $cookieStore, $http, $state, toastrConfig, AuthenticationService, ArrayHelper, PendingRequest, ModalHelper) {
    $rootScope.appVersion = config.appVersion;
    // keep user logged in after page refresh
    $rootScope.dnetuic = AuthenticationService.GetDNETUICFromCookies();
    var dnetuic = angular.fromJson($rootScope.dnetuic);
    if (dnetuic && dnetuic.currentUser) {
      $http.defaults.headers.common['Authorization'] = 'Bearer ' + dnetuic.currentUser.token;
      if ($location.path() == '/auth/login') {
        $location.path('/dashboard')
      }
    }

    $transitions.onSuccess({}, function (transition) {
      PendingRequest.cancelAll(transition.to().name, 'GET');
    });

    $rootScope.$on('$locationChangeStart', function (event, next, current) {
      // redirect to login page if not logged in and trying to access a restricted page
      var dnetuic = angular.fromJson($rootScope.dnetuic);
      var loggedIn = dnetuic && dnetuic.currentUser;
      if (!loggedIn) {
        $rootScope.isLogin = true;
        if ($location.path() != '/auth/login') {
          // $state.go('auth.login')
          $location.path('/auth/login')
        }
      }
      else {
        if (!$rootScope.userPermissions ||
          $rootScope.userPermissions == null ||
          $rootScope.userPermissions == undefined) {
          AuthenticationService.GetUserPermissions(dnetuic.currentUser.userId, dnetuic.currentUser.clientId)
            .then(function (response) {
              $rootScope.userPermissions = ArrayHelper.getArrayOfProperty(response.Data, "PermissionCode");
              $rootScope.$broadcast('refresh-sidebar');
            }, function (response) {
            }).finally(function () { });
        } else {
          sessionStorage.setItem('visited', true);
        }
      }
    });

    $rootScope.logout = function () {
      AuthenticationService.ClearCredentials();
      $state.go('auth.login')
    }

    $rootScope.updateUserProfile = function () {
      $state.go('userProfile.detail')
    }

    $rootScope.$watch('$pageFinishedLoading', function (newValue, oldValue) {
      if ($rootScope.$pageFinishedLoading === undefined) {
        ModalHelper.showProgressBar();
      } else if ($rootScope.$pageFinishedLoading == true) {
        ModalHelper.hideProgressBar();
      }
    });
  }


  routeConfig.$inject = ['$urlRouterProvider', 'baSidebarServiceProvider', 'cfpLoadingBarProvider'];
  function routeConfig($urlRouterProvider, baSidebarServiceProvider, cfpLoadingBarProvider) {
    cfpLoadingBarProvider.latencyThreshold = 0;
    cfpLoadingBarProvider.includeSpinner = false;
    $urlRouterProvider.otherwise('/dashboard');

    // baSidebarServiceProvider.addStaticItem({
    //   title: 'Pages',
    //   icon: 'ion-document',
    //   subMenu: [{
    //     title: 'Sign In',
    //     fixedHref: 'Home/Login',
    //     blank: false
    //   }]
    // });
  }

})();
