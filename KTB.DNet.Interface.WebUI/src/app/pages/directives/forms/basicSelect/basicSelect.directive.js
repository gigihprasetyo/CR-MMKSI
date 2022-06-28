(function () {
    'use strict';

    angular.module('DNet.pages.directives')
        .directive("basicSelect", function () {
            return {
                require: '^form',
                restrict: 'E',
                scope: {
                    ngModel: "=",
                    ngDisabled: "@",
                    label: "@",
                    name: "@",
                    required: "@",
                    options: "="
                },
                templateUrl: 'src/app/pages/directives/forms/basicSelect/basicSelect.html',
                link: function (scope, elem, attrs, form) {
                    scope.form = form;
                }
            };
        })
})();