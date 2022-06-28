(function () {
    'use strict';

    angular.module('DNet.pages.directives')
        .directive('textArea', function () {
            return {
                require: '^form',
                restrict: 'E',
                scope: {
                    ngModel: "=",
                    label: "@",
                    name: "@",
                    required: "@",
                    ngDisabled: "@",
                    type: "@"
                },
                templateUrl: 'src/app/pages/directives/forms/textArea/textArea.html',
                link: function (scope, elem, attrs, form) {
                    scope.form = form;
                }
            };
        })
})();