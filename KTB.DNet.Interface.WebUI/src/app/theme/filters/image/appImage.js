/**
 * @author v.lugovsky
 * created on 17.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.theme')
      .filter('appImage', appImage);

  /** @ngInject */
  appImage.$inject = ['layoutPaths'];
  function appImage(layoutPaths) {
    return function(input) {
      return layoutPaths.images.root + input;
    };
  }

})();
