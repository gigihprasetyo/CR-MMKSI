/**
 * @author v.lugovksy
 * created on 16.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.theme.components')
      .directive('contentTop', contentTop);

  /** @ngInject */
  contentTop.$inject = ['$location','$state'];
  function contentTop($location, $state) {
    return {
      restrict: 'E',
      templateUrl: 'src/app/theme/components/contentTop/contentTop.html',
      link: function($scope) {
        $scope.$watch(function () {
          $scope.paths = $state.$current.path
          $scope.activePageTitle = $state.current.title;
        });
      }
    };
  }

})();