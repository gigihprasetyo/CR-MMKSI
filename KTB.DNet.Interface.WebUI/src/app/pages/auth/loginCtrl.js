(function () {
    'use strict';

    angular.module('DNet.pages.auth')
        .controller('LoginCtrl', LoginCtrl);

    LoginCtrl.$inject = ['$rootScope', '$scope', '$state', '$uibModal', 'AuthenticationService', 'ModalHelper', 'ArrayHelper'];
    function LoginCtrl($rootScope, $scope, $state, $uibModal, AuthenticationService, ModalHelper, ArrayHelper) {
        var vm = $scope;
        vm.loginDetail = {};
        vm.userData = {};
        vm.login = _login;
        vm.enterApp = _enterApp;
        vm.back = _back;
        vm.selectClientMode = false;
        vm.listClients = []
        vm.selectedClient = {};

        function _back() {
            vm.selectClientMode = false;
        }

        if($rootScope.tokenExpired){
            ModalHelper.showMessage("Anda tidak memiliki otorisasi untuk mengakses interface ini atau token telah expired.", "error")
        }

        function _login() {
            AuthenticationService.Login(vm.loginDetail.username, vm.loginDetail.password)
                .then(function (result) {
                    vm.selectClientMode = true;
                    vm.userData = result.Data.User
                    vm.listClients = result.Data.Clients
                    // clientModal(result.Data, vm.loginDetail)
                }, function (response) {
                    alert(response.Message);
                }).finally(function () { });
        };

        function _enterApp() {
            ModalHelper.showProgressBar();
            if (vm.selectedClient.selected === undefined) {
                alert('Please select client.');
                return;
            }

            var entity = {
                userId: vm.userData.Id,
                username: vm.loginDetail.username,
                password: vm.loginDetail.password,
                clientId: vm.selectedClient.selected.ClientId,
                dealerCode: vm.userData.DealerCode,
                isDMSAdmin : vm.userData.IsDMSAdmin
            }

            AuthenticationService.Token(entity)
                .then(function (result) {
                    AuthenticationService.SetCredentials(entity, result.data.access_token);
                    $rootScope.isLogin = false;
                    var dnetuic = angular.fromJson($rootScope.dnetuic);
                    AuthenticationService.GetUserPermissions(dnetuic.currentUser.userId, dnetuic.currentUser.clientId)
                        .then(function (response) {
                            $rootScope.userPermissions = ArrayHelper.getArrayOfProperty(response.Data, "PermissionCode");
                            // $uibModalInstance.close()
                            $rootScope.$broadcast('refresh-sidebar');
                            $state.go('dashboard.view');
                        }, function (response) {
                        }).finally(function () { });
                }, function (result) {
                }).finally(function () { });
        };
    }

})();

