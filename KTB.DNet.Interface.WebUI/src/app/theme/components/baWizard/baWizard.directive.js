(function() {
  'use strict';

  angular.module('DNet.theme.components')
    .directive('baWizard', baWizard);

  /** @ngInject */
  function baWizard() {
    return {
      restrict: 'E',
      transclude: true,
      templateUrl: 'src/app/theme/components/baWizard/baWizard.html',
      controllerAs: '$baWizardController',
      controller: 'baWizardCtrl',
      scope: {
        tabMode: "="
      },
      link: function (scope, elem, attrs, form) {
          
      }
    }
  }
})();
