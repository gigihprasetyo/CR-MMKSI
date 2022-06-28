
/**
 * @author v.lugovsky
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.pages.deployment', ['ui.select', 'ngSanitize'])
    .config(routeConfig);

  /** @ngInject */
  routeConfig.$inject = ['$stateProvider'];
  function routeConfig($stateProvider) {
    $stateProvider
      .state('deployment', {
        url: '/deployment',
        permissionCodes: ["WebUI_JenkinsJob_Deploy"],
        template: '<ui-view autoscroll="true" autoscroll-body-top></ui-view>',
        abstract: true,
        module: true,
        title: 'Deployment',
        sidebarMeta: {
          icon: 'ion-ios-cloud-upload',
          order: 30,
        },
      })
      .state('deployment.deploy', {
          url: '/deploy',
        permissionCodes: ["WebUI_JenkinsJob_Deploy"],
        templateUrl: 'src/app/pages/deployment/deploy/deploy.html',
        title: 'Deploy',
        controller: 'deployCtrl',
         sidebarMeta: {
           order: 301,
         },
      })
      .state('deployment.rollback', {
        url: '/rollback',
        permissionCodes: ["WebUI_JenkinsJob_Deploy"],
        templateUrl: 'src/app/pages/deployment/rollback/rollback.html',
        title: 'Rollback',
        controller: 'rollbackCtrl',
        sidebarMeta: {
          order: 302,
        },
      })

      /** 
	    * application version
	    **/
    .state('deployment.appVersion', {
        url: '/appVersion',
        permissionCodes: ["WebUI_MsAppVersion_Read"],
        abstract: true,
        template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
        title: 'Application Version',
        sidebarMeta: {
            order: 303,
        }
    })
    .state('deployment.appVersion.list', {
        url: '',
        templateUrl: 'src/app/pages/deployment/appVersion/appVersion.html',
        title: 'Application Version',
        controller: 'appVersionCtrl',
        sidebarMeta: {
            order: 303,
        },
        breadchrumb: {
            text: 'List'
        },
    })
    .state('deployment.appVersion.create', {
        url: '/create',
        templateUrl: 'src/app/pages/deployment/appVersion/appVersionForm.html',
        title: 'Application Version',
        controller: 'appVersionFormCtrl',
        breadchrumb: {
            text: 'Create'
        },
    })
    .state('deployment.appVersion.update', {
        url: '/:id',
        templateUrl: 'src/app/pages/deployment/appVersion/appVersionForm.html',
        title: 'Application Version',
        controller: 'appVersionFormCtrl',
        breadchrumb: {
            text: 'Update'
        },
    })
  }
})();
