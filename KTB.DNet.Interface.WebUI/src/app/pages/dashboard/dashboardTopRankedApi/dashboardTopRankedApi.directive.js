/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.pages.dashboard')
      .directive('dashboardTopRankedApi', dashboardTopRankedApi);

  /** @ngInject */
  function dashboardTopRankedApi() {
    return {
      restrict: 'E',
      controller: 'DashboardTopRankedApiCtrl',
      templateUrl: 'src/app/pages/dashboard/dashboardTopRankedApi/dashboardTopRankedApi.html'
    };
  }
})();