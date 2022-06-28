/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.pages.dashboard')
      .directive('dashboardErrorList', dashboardErrorList);

  /** @ngInject */
  function dashboardErrorList() {
    return {
      restrict: 'E',
      controller: 'DashboardErrorListCtrl',
      templateUrl: 'src/app/pages/dashboard/dashboardErrorList/dashboardErrorList.html'
    };
  }
})();