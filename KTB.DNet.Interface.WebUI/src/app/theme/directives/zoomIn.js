/**
 * Animated load block
 */
(function () {
  'use strict';

  angular.module('DNet.theme')
      .directive('zoomIn', zoomIn);

  /** @ngInject */
  zoomIn.$inject = ['$timeout', '$rootScope'];
  function zoomIn($timeout, $rootScope) {
    return {
      restrict: 'A',
      link: function ($scope, elem) {
        var delay = 1000;

        if ($rootScope.$pageFinishedLoading) {
          delay = 100;
        }

        $timeout(function () {
          elem.removeClass('full-invisible');
          elem.addClass('animated zoomIn');
        }, delay);
      }
    };
  }

})();