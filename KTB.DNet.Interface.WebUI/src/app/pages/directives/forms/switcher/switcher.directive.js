(function () {
    'use strict';

    angular.module('DNet.pages.directives')
        .directive('switcher', function () {
            return {
                require: '^form',
                scope: {
                    ngModel: "=",
                    ngDisabled: "@",
                    label: "@",
                    name: "@",
                    textOn: "@",
                    textOff: "@",
                    switcherStyle: "@"
                },
                templateUrl: 'src/app/pages/directives/forms/switcher/switcher.html',
                link: function (scope, elem, attrs, form) {
                    scope.form = form;
                }
            };
        })
})();