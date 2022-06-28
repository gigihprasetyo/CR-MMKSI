/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.theme.components')
      .directive('msgCenter', msgCenter);

  /** @ngInject */
  msgCenter.$inject = ['baNotification'];
  function msgCenter(baNotification) {
    return {
      restrict: 'E',
      templateUrl: 'src/app/theme/components/msgCenter/msgCenter.html',
      // controller: 'MsgCenterCtrl',
      link:function($scope, element, attrs) {
        $scope.baNotification = baNotification;
        $scope.quantity = 100;
        $scope.$watch(function () {
            return baNotification.getNotification();
        }, reloadNotification);

        function reloadNotification() {
          $scope.notifications = baNotification.getNotification();
        }

        $scope.successFilterNotif = function(object) {
          return object.success === true;
        };
        
        $scope.errorFilterNotif = function(object) {
          return object.success === false;
        };

        $scope.clearAll = function(){
          baNotification.clearAll();
        }
        $scope.showAll = function(){
          $scope.quantity = undefined;
        }
    }
    };
  }

})();