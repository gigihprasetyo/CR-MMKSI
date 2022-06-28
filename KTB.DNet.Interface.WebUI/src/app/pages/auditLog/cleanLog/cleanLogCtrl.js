
(function () {
    'use strict';

    angular.module('DNet.pages.auditLog')
        .controller('cleanLogCtrl', cleanLogCtrl)

    cleanLogCtrl.$inject = ['$scope', '$state', 'TransactionService', 'ErrorLogService'];
    function cleanLogCtrl($scope, $state, TransactionService, ErrorLogService) {
        var vm = $scope;
        vm.currentState = $state.current.name;
        vm.model = {
            From: new Date(),
            To: new Date()
        }

        vm.logType = "Transaction Log";
        vm.logTypeOptions = [
            { Value: "Transaction Log", Text: "Transaction Log" },
            { Value: "Elmah", Text: "Elmah" }
        ];

        vm.deleteLog = _deleteLog;

        function _deleteLog() {

            var httpSevice = null;
            if (vm.logType == "Transaction Log") {
                httpSevice = TransactionService;
            }
            else {
                httpSevice = ErrorLogService;
            }

            var data = {
                "From": moment(vm.model.From, "DD MMMM YYYY").toDate(),
                "To": moment(vm.model.To, "DD MMMM YYYY").toDate()
            };

            httpSevice.Delete(data)
                .then(
                    function (response) { },
                    function (error) { });
        }

    }

})();