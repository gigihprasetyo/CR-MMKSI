(function () {
    'use strict';

    angular.module('DNet.pages.directives')
        .directive('standardSelect', function () {
            var selectCtrl = ['$rootScope', '$scope', function ($rootScope, $scope) {

                $scope.getDisplayedValue = _getDisplayedValue;

                $rootScope.$watch('hasPendingRequest', function (newValue, oldValue) {
                    $scope.hasPendingRequest = $rootScope.hasPendingRequest;
                });
                
                function _getDisplayedValue(option, propertyName) {

                    if (option) {
                        var listOfName = propertyName.split(".");

                        if (listOfName.length > 1) {
                            if (option[listOfName[0]]) {
                                return $scope.getDisplayedValue(option[listOfName[0]], propertyName.replace(listOfName[0] + ".", ""));
                            }

                            return null;
                        }
                        else {
                            return option[propertyName];
                        }
                    }

                    return null;

                }
            }];

            return {
                require: '^form',
                restrict: 'E',
                templateUrl: 'src/app/pages/directives/forms/standardSelect/standardSelect.html',
                controller: selectCtrl,
                scope: {
                    ngModel: "=",
                    required: "@",
                    ngDisabled: "=",
                    label: "@",
                    name: "@",
                    options: "=",
                    value: "@",
                    text: "@",
                    onSelectCallback: "&",
                    searchEnabled: "="
                },
                replace: true,
                link: function (scope, elem, attrs, form) {
                    scope.form = form;
                }
            };
        })
})();