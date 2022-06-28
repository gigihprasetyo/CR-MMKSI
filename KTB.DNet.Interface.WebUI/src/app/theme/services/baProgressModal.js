/**
 * @author n.poltoratsky
 * created on 27.06.2016
 */
(function () {
    'use strict';

    angular.module('DNet.theme')
        .factory('baProgressModal', baProgressModal);

    /** @ngInject */
    baProgressModal.$inject = ['$rootScope', '$uibModal'];
    function baProgressModal($rootScope, $uibModal) {
        var methods = {};
        var progress = 0;
        var max = 100;
        $rootScope.progressBarModalOpened = false;

        return {
            setProgress: function (value) {
                if (value > max) {
                    throw Error('Progress can\'t be greater than max');
                }
                progress = value;
            },
            getProgress: function () {
                return progress;
            },
            open: function() {
                if (!$rootScope.progressBarModalOpened) {
                    $rootScope.progressBarModalOpened = true;
                    methods = $uibModal.open({
                        animation: true,
                        templateUrl: 'src/app/pages/common/modal/progressBarModal.html',
                        windowClass: "progress-bar-modal",
                        size: 'sm',
                        keyboard: false,
                        backdrop: 'static'
                    });
                } 
            },
            close: function() {
                if ($rootScope.progressBarModalOpened) {
                    methods.close();
                    $rootScope.progressBarModalOpened = false;
                } else {
                    console.log("Couldn't close modal. Progress modal is not active.")
                    // throw Error('Progress modal is not active');
                }

            }
        };
    }

})();