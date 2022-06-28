
(function () {
	'use strict';

	angular.module('DNet.pages.userProfile', [
	])
		.config(routeConfig);

	/** @ngInject */
	routeConfig.$inject = ['$stateProvider'];
	function routeConfig($stateProvider) {
		$stateProvider
			.state('userProfile', {
				url: '/userProfile',
				abstract: true,
				template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
				redirect: 'userProfile.detail',
				title: 'User Profile'
			})
			.state('userProfile.detail', {
				url: '',
				templateUrl: 'src/app/pages/userProfile/updateUserProfileForm.html',
				title: 'User Profiles',
				controller: 'updateUserProfileFormCtrl',
				breadchrumb: {
					text: 'Detail'
				}
			})

	}
})();
