angular.module('jq-multi-select', []).
  directive('multiSelect', ['$timeout', function ($timeout) {
    // function refresh(element) {
    //   element.multiSelect('refresh');
    // }

    function init(element, options) {
      var opt = options || {};

      element.multiSelect(opt);
    }

    return {
      require: '?ngModel',
      scope: {
        multiSelect: '=',
        msOptions: '='
      },
      link: function (scope, element, attrs, ngModel) {
        init(element, scope.msOptions);

        // scope.$watch(function () {
        //   return (ngModel && ngModel.$modelValue) || scope.multiSelect;
        // }, function () {
        //   // $timeout(function () {
        //     refresh(element);
        //   // }, 0)

        // });
      }
    };
  }]);