
(function () {
    'use strict';

    angular.module('DNet.pages.accessControl')
        .controller('userCtrl', userCtrl);

    /** @ngInject */
    userCtrl.$inject = ['$scope', 'UserService', 'Upload', '$timeout', '$window'];
    function userCtrl($scope, UserService, Upload, $timeout, $window) {

        $scope.user = {};
        $scope.httpService = UserService;
        $scope.Result = {};

        $scope.tableDefinition = {
            name: "user",
            key: "Id",
            identifier: "UserName",
            action: {
                create: "accessControl.user.create",
                update: "accessControl.user.update",
                upload: "accessControl.user.upload",
                delete: true
            },
            columns: [
                { name: "UserName", label: "User Name" },
                { name: "Email", label: "Email" },
                { name: "FullName", label: "Name" },
                { name: "IsActive", label: "Active", datatype: "boolean" },
                { name: "Actions", label: "Actions", action: true }
            ],
            tablePageSize: 10 // rows
        };

        $scope.UploadFiles = function (files) {
            $scope.SelectedFiles = files;
            if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
                UserService.uploadUser(files)
                    .then(function (response) {
                        $timeout(function () {
                            $scope.Result = response.data;
                            
                            if (!response.data.Success) {
                                alert('failed to upload user:' + response.data.Data.UserName);
                            }
                            else {
                                alert('user upload success');
                            }
                        });
                    }, function (response) {
                        if (response.status > 0) {
                            var errorMsg = response.status + ': ' + response.data;
                            alert(errorMsg);
                        }
                    }, function (evt) {
                        var element = angular.element(document.querySelector('#dvProgress'));
                        $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                        element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                    });
            }
        };

        $scope.getUploadTemplate = function() {
            UserService.getUploadTemplate();
        }

    }

})();