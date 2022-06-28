(function () {
    'use strict';

    angular.module('DNet.pages.directives')
        /* Input Text / Password */
        .directive('inputText', function () {
            return {
                require: '^form',
                restrict: 'E',
                scope: {
                    ngModel: "=",
                    label: "@",
                    name: "@",
                    required: "@",
                    readonly: "@",
                    ngDisabled: "=",
                    type: "@"
                },
                templateUrl: 'src/app/pages/directives/forms/inputText/inputText.html',
                link: function (scope, elem, attrs, form) {
                    scope.form = form;
                }
            };
        })
})();