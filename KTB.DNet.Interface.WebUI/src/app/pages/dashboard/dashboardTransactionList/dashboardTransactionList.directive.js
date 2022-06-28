/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.pages.dashboard')
      .directive('dashboardTransactionList', dashboardTransactionList);

  /** @ngInject */
  function dashboardTransactionList() {
    return {
      restrict: 'E',
      controller: 'DashboardTransactionListCtrl',
      templateUrl: 'src/app/pages/dashboard/dashboardTransactionList/dashboardTransactionList.html'
    };
  }
})();