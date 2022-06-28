
(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')
        .controller('threadLogCtrl', threadLogCtrl)

    threadLogCtrl.$inject = ['$scope', '$state', '$stateParams', 'ThreadLogService'];
    function threadLogCtrl($scope, $state, $stateParams, ThreadLogService) {

        $scope.httpService = ThreadLogService;
        $scope.currentState = $state.current.name;
        $scope.resendTableDefinition = _getResendTableDefinition();
        $scope.threadLogTableDefinition = _getThreadLogTableDefinition();
        $scope.back = _back;
        if ($stateParams.logId) {
            ThreadLogService.GetDetailTransactionLog($stateParams.logId)
                .then(function (response) {
                    $scope.transactionLogDetail = response.Data;
                    $scope.resendTableDefinition.dataSource =
                        $scope.transactionLogDetail.ResendLog == null ?
                            [] : $scope.transactionLogDetail.ResendLog;

                }, function (error) { });
        }

        function _back() {
            $state.go("auditLog.transactionRuntime.list");
        }

        function _getThreadLogTableDefinition() {
            return {
                name: "thread log",
                key: "TransactionLogId",
                identifier: "MethodName",
                action: {
                    customActions: [
                        { text: "Detail", type: "primary", action: _showTransactionLogDetail, state: $scope.currentState.replace("list", "view")}
                    ]
                },
                columns: [
                    { name: "TransactionLogId", label: "Transaction Log ID" },
                    { name: "MethodName", label: "Method Name" },
                    { name: "StartedTime", label: "Started Time", datatype: "date", format: "yyyy-MM-dd HH:mm:ss" },
                    { name: "FinishedTime", label: "Finished Time", datatype: "date", format: "yyyy-MM-dd HH:mm:ss" },
                    { name: "ExecutionTime", label: "Execution Time (ms)" },
                    { name: "Actions", label: "Actions", action: true },
                ],
                tablePageSize: 20 // rows
            };
        }

        function _getResendTableDefinition() {
            return {
                name: "resend transaction log",
                key: "Id",
                identifier: "MethodName",
                action: {},
                columns: [
                    { name: "LogId", label: "ID" },
                    { name: "Endpoint", label: "Endpoint" },
                    { name: "Status", label: "Status" },
                    { name: "CreatedBy", label: "Created By" },
                    { name: "CreatedTime", label: "Created Time", datatype: "date", format: "yyyy-MM-dd HH:mm:ss" },
                    { name: "UpdatedBy", label: "Updated By" },
                    { name: "UpdatedTime", label: "Updated Time", datatype: "date", format: "yyyy-MM-dd HH:mm:ss" },
                    { name: "Actions", label: "Actions", action: true },
                ],
                searchable: false,
                tablePageSize: 10 // rows
            };
        }

        function _showTransactionLogDetail(row) {
            $state.go($scope.currentState.replace("list", "view"), { "id": row.TransactionLogId });
        }
    }

})();