/**
 * @author v.lugovsky
 * created on 17.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.theme')
      .filter('kameleonImg', kameleonImg);

  /** @ngInject */
  kameleonImg.$inject = ['layoutPaths'];
  function kameleonImg(layoutPaths) {
    return function(input) {
      return layoutPaths.images.root + 'theme/icon/kameleon/' + input + '.svg';
    };
  }

})();
