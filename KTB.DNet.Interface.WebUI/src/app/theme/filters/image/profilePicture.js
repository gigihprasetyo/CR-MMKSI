/**
 * @author v.lugovsky
 * created on 17.12.2015
 */
(function () {
  'use strict';

  angular.module('DNet.theme')
      .filter('profilePicture', profilePicture);

  /** @ngInject */
  profilePicture.$inject = ['layoutPaths'];
  function profilePicture(layoutPaths) {
    return function(input, ext) {
      ext = ext || 'png';
      return layoutPaths.images.profile + input + '.' + ext;
    };
  }

})();
