
(function () {
    'use strict';

    angular.module('DNet.pages.userProfile')
        .controller('updateUserProfileFormCtrl', updateUserProfileFormCtrl)

        updateUserProfileFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'UserService'];
    function updateUserProfileFormCtrl($scope, $state, $stateParams, UserService) {
        $scope.user = {};
        $scope.updateUserInfo = _updateUserInfo;
        
        UserService.getUserProfile()
            .then(function (response) {
                $scope.user = response.Data;
            }, function (error) { });
        

        
        function _updateUserInfo() {
            UserService.updateUserInfo($scope.user)
                .then(function (response) {
                    $state.go('userProfile.detail')
                }, function (error) {  });
        }

    }

})();