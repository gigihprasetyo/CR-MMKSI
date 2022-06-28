/**
 * Animated load block
 */
(function () {
    'use strict';

    angular.module('DNet.theme')
        .directive('windowFocus', windowFocus);

    /** @ngInject */
    windowFocus.$inject = ['$window', '$rootScope'];
    function windowFocus($window, $rootScope) {
        // Return the directive configuration object.
        return ({
            link: link,
            restrict: "A"
        });
        // I bind the JavaScript events to the view-model.
        function link(scope, element, attributes) {
            $rootScope.windowFocused = true;
            var onFocus = function(){
                $rootScope.windowFocused = true;
                $rootScope.$apply();
            }
            var onBlur = function(){
                $rootScope.windowFocused = false;
                $rootScope.$apply();
            }
            $window.onfocus = onFocus;
            $window.onblur = onBlur;
        }
    }

})();