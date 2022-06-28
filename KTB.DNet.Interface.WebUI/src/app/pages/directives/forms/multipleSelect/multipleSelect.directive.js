(function () {
    'use strict';

    angular.module('DNet.pages.directives')
        .directive('multipleSelect', function () {
            var selectCtrl = ['$rootScope', '$scope', function ($rootScope, $scope) {
                $rootScope.$watch('hasPendingRequest', function (newValue, oldValue) {
                    $scope.hasPendingRequest = $rootScope.hasPendingRequest;
                });
            }];
            return {
                require: '^form',
                restrict: 'E',
                templateUrl: 'src/app/pages/directives/forms/multipleSelect/multipleSelect.html',
                controller: selectCtrl,
                scope: {
                    ngModel: "=",
                    required: "@",
                    ngDisabled: "@",
                    label: "@",
                    name: "@",
                    options: "=options",
                    value: "@",
                    text: "@",
                },
                // replace: true,
                link: function (scope, elem, attrs, form) {
                    scope.form = form;
                }
            };
        })
})();