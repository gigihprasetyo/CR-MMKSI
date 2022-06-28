/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.theme.components')
    .controller('BaSidebarCtrl', BaSidebarCtrl);

  /** @ngInject */
  BaSidebarCtrl.$inject = ['$rootScope', '$state', '$scope', 'baSidebarService'];
  function BaSidebarCtrl($rootScope, $state, $scope, baSidebarService) {

    baSidebarService.getMenuItems().then(function (response) {
      $scope.menuItems = response;
      $scope.defaultSidebarState = $scope.menuItems[0].stateRef;
    }, function (error) { });


    $scope.hoverItem = function ($event) {
      $scope.showHoverElem = true;
      $scope.hoverElemHeight = $event.currentTarget.clientHeight;
      var menuTopValue = 66;
      $scope.hoverElemTop = $event.currentTarget.getBoundingClientRect().top - menuTopValue;
    };

    $scope.$on('$stateChangeSuccess', function () {
      if (baSidebarService.canSidebarBeHidden()) {
        baSidebarService.setMenuCollapsed(true);
      }
    });

    $scope.$on('refresh-sidebar', function (event, args) {
      baSidebarService.getMenuItems().then(function (response) {
        $scope.menuItems = response;
        $scope.defaultSidebarState = $scope.menuItems[0].stateRef;
      }, function (error) { });
    });
  }
})();