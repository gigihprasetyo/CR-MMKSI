
(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')
        .controller('transactionLogDetailCtrl', transactionLogDetailCtrl)

    transactionLogDetailCtrl.$inject = ['$scope', '$rootScope', '$state', '$stateParams', 'TransactionService', 'ModalHelper'];
    function transactionLogDetailCtrl($scope, $rootScope, $state, $stateParams, TransactionService, ModalHelper) {
        var vm = $scope;
        vm.userPermissions = $rootScope.userPermissions;
        vm.allowToResolve = $rootScope.userPermissions != null ? $rootScope.userPermissions.includes("WebUI_FailedTransactionLog_Resend") : false;

        vm.httpService = TransactionService;
        vm.transactionLogDetail = {};
        vm.back = _back;
        vm.resolve = _resolve;
        vm.showToken = _showToken;
        vm.showInput = _showInput;
        vm.resendTableDefinition = _getResendTableDefinition();
        vm.currentState = $state.current.name;
        vm.title = $state.current.title;

        if ($stateParams.id) {
            TransactionService.GetById($stateParams.id)
                .then(function (response) {
                    vm.transactionLogDetail = response.Data;

                    try {
                        vm.jsonObjectInput = JSON.parse(vm.transactionLogDetail.Input);
                        vm.jsonObjectInput = typeof vm.jsonObjectInput === "string" ? JSON.parse(vm.jsonObjectInput) : vm.jsonObjectInput;
                        vm.transactionLogDetail.Input = JSON.stringify(vm.jsonObjectInput, null, 4);
                    } catch (e) { }

                    try {
                        vm.jsonObjectOutput = JSON.parse(vm.transactionLogDetail.Output);
                        vm.jsonObjectOutput = typeof vm.jsonObjectOutput === "string" ? JSON.parse(vm.jsonObjectOutput) : vm.jsonObjectOutput;
                        vm.transactionLogDetail.Output = JSON.stringify(vm.jsonObjectOutput, null, 4);
                    } catch (e) { }

                    vm.resendTableDefinition.dataSource = response.Data.ResendLog;
                }, function (error) { });
        }

        function _getResendTableDefinition() {
            return {
                name: "resend transaction log",
                key: "Id",
                identifier: "Endpoint",
                action: {},
                columns: [
                    { name: "Id", label: "ID" },
                    { name: "Endpoint", label: "Endpoint" },
                    { name: "StatusStr", label: "Status" },
                    { name: "CreatedBy", label: "Created By" },
                    { name: "CreatedTime", label: "Created Time", datatype: "date", format: "yyyy-MM-dd HH:mm:ss" },
                    { name: "UpdatedBy", label: "Updated By" },
                    { name: "UpdatedTime", label: "Updated Time", datatype: "date", format: "yyyy-MM-dd HH:mm:ss" }
                ],
                searchable: false,
                tablePageSize: 10 // rows
            };
        }

        vm.Submit = function () {
            if ($stateParams.id) {
                TransactionService.Resend(vm.transactionLogDetail)
                    .then(function (response) {
                        //$state.go(vm.currentState.replace("resolve", "list"));
                    }, function (error) {
                        //$state.go(vm.currentState.replace("resolve", "list"));
                    });
            }
        }

        function _back() {
            window.history.back();
        }

        function _resolve(row) {
            $state.go(vm.currentState.replace("view", "resolve"), { "id": $stateParams.id });
        }

        function _showToken() {
            var msg = {};
            msg.title = "Token"
            msg.content = vm.transactionLogDetail.Token;
            ModalHelper.showMessage(msg, "info");
        }

        function _showInput() {
            var msg = vm.jsonObjectInput;
            if (msg) {
                ModalHelper.showData("Input Data", msg);
            } else {
                msg = {}
                msg.title = "Input Data"
                msg.content = vm.transactionLogDetail.Input;
                ModalHelper.showMessage(msg, "info");
            }

        }

    }

})();