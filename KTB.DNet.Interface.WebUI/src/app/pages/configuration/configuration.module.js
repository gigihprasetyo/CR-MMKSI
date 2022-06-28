
(function () {
	'use strict';

	angular.module('DNet.pages.configuration', [])
		.config(routeConfig);

	/** @ngInject */
	routeConfig.$inject = ['$stateProvider'];
	function routeConfig($stateProvider) {
	    $stateProvider
            .state('configuration', {
                url: '/configuration',
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                abstract: true,
                module: true,
                permissionCodes: ["WebUI_AppConfig_Read", "WebUI_StandardCode_Read", "WebUI_StandardCodeChar_Read"],
                title: 'Configuration',
                sidebarMeta: {
                    icon: 'ion-android-settings',
                    order: 70,
                },
            })

			/** 
			 * system config
			 **/
			.state('configuration.systemConfig', {
			    url: '/systemConfig',
			    permissionCodes: ["WebUI_AppConfig_Read"],
			    abstract: true,
			    template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
			    title: 'System Config',
			    controller: 'systemConfigCtrl',
			    sidebarMeta: {
			        order: 71,
			    }
			})
			.state('configuration.systemConfig.list', {
			    url: '',
			    templateUrl: 'src/app/pages/configuration/systemConfig/systemConfig.html',
			    title: 'System Config',
			    breadchrumb: {
			        text: 'List',
			    }
			})
			.state('configuration.systemConfig.create', {
			    url: '/create',
			    templateUrl: 'src/app/pages/configuration/systemConfig/systemConfigForm.html',
			    title: 'Create System Config',
			    breadchrumb: {
			        text: 'Create'
			    },
			    controller: 'systemConfigFormCtrl'
			})
			.state('configuration.systemConfig.update', {
			    url: '/:id',
			    templateUrl: 'src/app/pages/configuration/systemConfig/systemConfigForm.html',
			    title: 'Update System Config',
			    controller: 'systemConfigFormCtrl',
			    breadchrumb: {
			        text: 'Update'
			    },
			})

            /** 
			 * standard code
			 **/
			.state('configuration.standardCode', {
			    url: '/standardCode',
			    permissionCodes: ["WebUI_StandardCode_Read"],
			    abstract: true,
			    template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
			    title: 'Standard Code',
			    controller: 'standardCodeCtrl',
			    sidebarMeta: {
			        order: 72,
			    }
			})
			.state('configuration.standardCode.list', {
			    url: '',
			    templateUrl: 'src/app/pages/configuration/standardCode/standardCode.html',
			    title: 'Standard Code',
			    breadchrumb: {
			        text: 'List',
			    }
			})
			.state('configuration.standardCode.create', {
			    url: '/create',
			    templateUrl: 'src/app/pages/configuration/standardCode/standardCodeForm.html',
			    title: 'Create Standard Code',
			    breadchrumb: {
			        text: 'Create'
			    },
			    controller: 'standardCodeFormCtrl'
			})
			.state('configuration.standardCode.update', {
			    url: '/:id',
			    templateUrl: 'src/app/pages/configuration/standardCode/standardCodeForm.html',
			    title: 'Update Standard Code',
			    controller: 'standardCodeFormCtrl',
			    breadchrumb: {
			        text: 'Update'
			    },
			})

            /** 
			 * standard code char
			 **/
			.state('configuration.standardCodeChar', {
			    url: '/standardCodeChar',
			    permissionCodes: ["WebUI_StandardCodeChar_Read"],
			    abstract: true,
			    template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
			    title: 'Standard Code Char',
			    controller: 'standardCodeCharCtrl',
			    sidebarMeta: {
			        order: 73,
			    }
			})
			.state('configuration.standardCodeChar.list', {
			    url: '',
			    templateUrl: 'src/app/pages/configuration/standardCodeChar/standardCodeChar.html',
			    title: 'Standard Code Char',
			    breadchrumb: {
			        text: 'List',
			    }
			})
			.state('configuration.standardCodeChar.create', {
			    url: '/create',
			    templateUrl: 'src/app/pages/configuration/standardCodeChar/standardCodeCharForm.html',
			    title: 'Create Standard Code Char',
			    breadchrumb: {
			        text: 'Create'
			    },
			    controller: 'standardCodeCharFormCtrl'
			})
			.state('configuration.standardCodeChar.update', {
			    url: '/:id',
			    templateUrl: 'src/app/pages/configuration/standardCodeChar/standardCodeCharForm.html',
			    title: 'Update Standard Code Char',
			    controller: 'standardCodeCharFormCtrl',
			    breadchrumb: {
			        text: 'Update'
			    },
			})

	}

})();
