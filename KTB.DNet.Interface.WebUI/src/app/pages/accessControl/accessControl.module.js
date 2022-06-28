
(function () {
	'use strict';

	angular.module('DNet.pages.accessControl', [])
		.config(routeConfig);

	/** @ngInject */
	routeConfig.$inject = ['$stateProvider'];
	function routeConfig($stateProvider) {
		$stateProvider
			.state('accessControl', {
				url: '/accessControl',
				template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
				abstract: true,
				module: true,
				permissionCodes: ["WebUI_MsApplication_Read", "WebUI_Client_Read",
					"WebUI_User_Read", "WebUI_Permission_Read",
					"WebUI_ClientRolePermission_Read", "WebUI_Role_Read"],
				title: 'Access Control',
				sidebarMeta: {
					icon: 'ion-android-contacts',
					order: 10,
				},
			})

			/** 
			 * Application
			 **/
			.state('accessControl.application', {
				url: '/application',
				permissionCodes: ["WebUI_MsApplication_Read"],
				abstract: true,
				template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
				title: 'Application',
				controller: 'applicationCtrl',
				sidebarMeta: {
					order: 100,
				}
			})
			.state('accessControl.application.list', {
				url: '',
				templateUrl: 'src/app/pages/accessControl/application/application.html',
				title: 'Applications',
				breadchrumb: {
					text: 'List',
				}
			})
			.state('accessControl.application.create', {
				url: '/create',
				templateUrl: 'src/app/pages/accessControl/application/applicationForm.html',
				title: 'Create Application',
				breadchrumb: {
					text: 'Create'
				},
				controller: 'applicationFormCtrl'
			})
			.state('accessControl.application.update', {
				url: '/:id',
				templateUrl: 'src/app/pages/accessControl/application/applicationForm.html',
				title: 'Update Application',
				controller: 'applicationFormCtrl',
				breadchrumb: {
					text: 'Update'
				},
			})

			/** 
			 * Client
			 **/
			.state('accessControl.client', {
				url: '/client',
				permissionCodes: ["WebUI_Client_Read"],
				abstract: true,
				template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
				title: 'Client',
				sidebarMeta: {
					order: 101,
				}
			})
			.state('accessControl.client.list', {
				url: '',
				templateUrl: 'src/app/pages/accessControl/client/client.html',
				title: 'Clients',
				controller: 'clientCtrl',
				sidebarMeta: {
					order: 101,
				},
				breadchrumb: {
					text: 'List'
				},
			})
			.state('accessControl.client.create', {
				url: '/create',
				templateUrl: 'src/app/pages/accessControl/client/clientForm.html',
				title: 'Create Client',
				controller: 'clientFormCtrl',
				breadchrumb: {
					text: 'Create'
				},
			})
			.state('accessControl.client.update', {
				url: '/:id',
				templateUrl: 'src/app/pages/accessControl/client/clientForm.html',
				title: 'Update Client',
				controller: 'clientFormCtrl',
				breadchrumb: {
					text: 'Update'
				},
			})

			/** 
			 * Role
			 **/
			.state('accessControl.role', {
				url: '/role',
				permissionCodes: ["WebUI_Role_Read"],
				abstract: true,
				template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
				title: 'Role',
				sidebarMeta: {
					order: 102,
				}
			})
			.state('accessControl.role.list', {
				url: '',
				templateUrl: 'src/app/pages/accessControl/role/role.html',
				title: 'Roles',
				controller: 'roleCtrl',
				sidebarMeta: {
					order: 102,
				},
				breadchrumb: {
					text: 'List'
				},
			})
			.state('accessControl.role.create', {
				url: '/create',
				templateUrl: 'src/app/pages/accessControl/role/roleForm.html',
				title: 'Create Role',
				controller: 'roleFormCtrl',
				breadchrumb: {
					text: 'Create'
				},
			})
			.state('accessControl.role.update', {
				url: '/update/:id',
				templateUrl: 'src/app/pages/accessControl/role/roleForm.html',
				title: 'Update Role',
				controller: 'roleFormCtrl',
				breadchrumb: {
					text: 'Update'
				},
			})

			/** 
			 * User
			 **/
			.state('accessControl.user', {
				url: '/user',
				permissionCodes: ["WebUI_User_Read"],
				abstract: true,
				template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
				title: 'User',
				sidebarMeta: {
					order: 103,
				}
			})
			.state('accessControl.user.list', {
				url: '',
				templateUrl: 'src/app/pages/accessControl/user/user.html',
				title: 'Users',
				controller: 'userCtrl',
				sidebarMeta: {
					order: 103,
				},
				breadchrumb: {
					text: 'List'
				},
			})
			.state('accessControl.user.create', {
				url: '/create',
				templateUrl: 'src/app/pages/accessControl/user/createUserForm.html',
				title: 'Create User',
				controller: 'userFormCtrl',
				breadchrumb: {
					text: 'Create'
				},
			})			
			.state('accessControl.user.upload', {
				url: '/upload',
				templateUrl: 'src/app/pages/accessControl/user/uploadUser.html',
				title: 'Upload User',
				controller: 'userCtrl',
				breadchrumb: {
					text: 'Upload'
				},
			})
			.state('accessControl.user.update', {
				url: '/:id',
				templateUrl: 'src/app/pages/accessControl/user/updateUserForm.html',
				title: 'Update User',
				controller: 'userFormCtrl',
				breadchrumb: {
					text: 'Update'
				},
			})

			/** 
			 * Client Role Permission
			 **/
			.state('accessControl.clientRolePermissions', {
				url: '/clientRolePermission',
				permissionCodes: ["WebUI_ClientRolePermission_Read"],
				templateUrl: 'src/app/pages/accessControl/clientRolePermissions/clientRolePermissions.html',
				title: 'Client Role Permission',
				controller: 'clientRolePermissionsCtrl',
				sidebarMeta: {
					order: 109,
				}
			})

            /** 
			 * Client Role
			 **/
			.state('accessControl.clientUser', {
			    url: '/clientUser',
			    permissionCodes: ["WebUI_Client_Read"],
			    templateUrl: 'src/app/pages/accessControl/clientUser/clientUser.html',
			    title: 'Client User',
			    controller: 'clientUserCtrl',
			    sidebarMeta: {
			        order: 110,
			    }
			})

	}

})();
