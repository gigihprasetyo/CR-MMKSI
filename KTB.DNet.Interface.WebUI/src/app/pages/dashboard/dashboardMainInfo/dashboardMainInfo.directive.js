/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.pages.dashboard')
      .directive('dashboardMainInfo', dashboardMainInfo);

  /** @ngInject */
  function dashboardMainInfo() {
    return {
      restrict: 'E',
      controller: 'DashboardMainInfoCtrl',
      templateUrl: 'src/app/pages/dashboard/dashboardMainInfo/dashboardMainInfo.html'
    };
  }
})();