
(function () {
    'use strict';

    angular.module('DNet.pages.endpoint', [])
        .config(routeConfig);

    /** @ngInject */
    routeConfig.$inject = ['$stateProvider'];
    function routeConfig($stateProvider) {
        $stateProvider
            .state('endpoint', {
                url: '/endpoint',
                permissionCodes: ["WebUI_Permission_Read"],
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                redirect: 'endpoint.list',
                abstract: true,
                title: 'Endpoint',
                sidebarMeta: {
                    icon: 'ion-android-locate',
                    order: 40
                }
            })

            /** 
			 * Endpoint
			 **/
			.state('endpoint.endpoint', {
			    url: '/endpoint',
			    templateUrl: 'src/app/pages/endpoint/endpoint.html',
			    title: 'Endpoints',
			    controller: 'endpointCtrl',
			    breadchrumb: {
                    text: 'List'
			    },
			    sidebarMeta: {
			        order: 100
			    }
			})

            .state('endpoint.list', {
                url: '',
                templateUrl: 'src/app/pages/endpoint/endpoint.html',
                title: 'Endpoints',
                controller: 'endpointCtrl',
				breadchrumb: {
					text: 'List'
				}
            })

            .state('endpoint.create', {
                url: '/create',
                templateUrl: 'src/app/pages/endpoint/endpointForm.html',
                title: 'Create Endpoint Permission',
                controller: 'endpointFormCtrl',
				breadchrumb: {
					text: 'Create'
				}
            })
            .state('endpoint.update', {
                url: '/:id',
                templateUrl: 'src/app/pages/endpoint/endpointForm.html',
                title: 'Update Endpoint Permission',
                controller: 'endpointFormCtrl',
				breadchrumb: {
					text: 'Update'
				}
            })


            
            /** 
			 * Endpoint Group
			 **/
			.state('endpoint.endpointGroup', {
			    url: '/endpointGroup',
			    templateUrl: 'src/app/pages/endpoint/endpointGroup.html',
			    title: 'Endpoint Group',
			    controller: 'endpointGroupCtrl',
			    sidebarMeta: {
			        order: 110,
			    }
			})

        /** 
			 * Endpoint Type
			 **/
			.state('endpoint.endpointType', {
			    url: '/endpointType',
			    templateUrl: 'src/app/pages/endpoint/endpointType.html',
			    title: 'Endpoint Type',
			    controller: 'endpointTypeCtrl',
			    sidebarMeta: {
			        order: 120,
			    }
			})
        /** 
			 * Operation Type
			 **/
			.state('endpoint.operationTpe', {
			    url: '/operationType',
			    templateUrl: 'src/app/pages/endpoint/operationType.html',
			    title: 'Operation Type',
			    controller: 'operationTypeCtrl',
			    sidebarMeta: {
			        order: 130,
			    }
			})
            
    }

})();




