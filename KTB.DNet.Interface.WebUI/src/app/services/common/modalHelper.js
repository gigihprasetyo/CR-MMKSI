(function () {
    'use strict';

    angular.module('DNet.services.modalHelper', [])
        .service('ModalHelper', ModalHelper);

    ModalHelper.$inject = ['$rootScope', '$uibModal', '$timeout', 'baProgressModal', 'cfpLoadingBar', 'PendingRequest'];
    function ModalHelper($rootScope, $uibModal, $timeout, baProgressModal, cfpLoadingBar, PendingRequest) {
        var service = {};
        service.messageModal = {
            "success": { "template": "src/app/pages/common/modal/successModal.html" },
            "info": { "template": "src/app/pages/common/modal/infoModal.html" },
            "warning": { "template": "src/app/pages/common/modal/warningModal.html" },
            "error": { "template": "src/app/pages/common/modal/errorModal.html" },
            "confirm": { "template": "src/app/pages/common/modal/confirmModal.html" },
            "json": { "template": "src/app/pages/common/modal/jsonModal.html" },
        };

        service.showProgressBar = showProgressBar;
        service.hideProgressBar = hideProgressBar;
        service.showMessage = showMessage;
        service.showData = showData;

        var ModalCtrl = ['$scope', 'message', function ($scope, message, title) {
            $scope.message = message;
            $scope.title = title;

        }];

        return service;

        function showProgressBar() {
            (function changeValue() {
                var currentProgress = cfpLoadingBar.status()

                if (!PendingRequest.isLoaderFinished()) {
                    if (!$rootScope.progressBarModalOpened) {
                        baProgressModal.setProgress(0)
                        baProgressModal.open();
                    }
                    var nextProgress = Math.round((baProgressModal.getProgress() + cfpLoadingBar.status() * 100) / 2)
                    if (nextProgress < 100 ) {
                        baProgressModal.setProgress(nextProgress);
                        currentProgress = nextProgress;
                    }
                    $timeout(changeValue, 300);
                }

            })();
        }

        function hideProgressBar() {
            if ($rootScope.progressBarModalOpened && PendingRequest.isLoaderFinished() && $rootScope.$pageFinishedLoading) {
                _finishProgress();
            }
        }

        function _finishProgress(){
            var currentProgress = baProgressModal.getProgress();
            if(currentProgress < 100){
                var next = currentProgress + 25;
                if(next < 100){
                    baProgressModal.setProgress(next);
                } else {
                    baProgressModal.setProgress(100);
                }
                $timeout(_finishProgress, 50);
            } else {
                baProgressModal.close();
            }

        }

        function showMessage(message, type, size) {
            return $uibModal.open({
                animation: true,
                templateUrl: this.messageModal[type].template,
                size: size ? size : 'md',
                controller: ModalCtrl,
                resolve: {
                    message: function () {
                        return message;
                    }
                }
            }).result;
        }

        function showData(title, data, size) {
            return $uibModal.open({
                animation: true,
                templateUrl: this.messageModal['json'].template,
                size: size ? size : 'md',
                controller: ModalCtrl,
                resolve: {
                    message: function () {                        
                        return {
                            title: title,
                            data: data
                        }
                    }
                }
            }).result;
        };
    }
})();
