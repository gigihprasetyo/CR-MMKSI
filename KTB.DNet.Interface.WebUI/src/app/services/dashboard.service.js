(function () {
    'use strict';

    angular.module('DNet.services.dashboard', [])
        .service('DashboardService', DashboardService);

    DashboardService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function DashboardService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.dashboard;
        service.GetRankedAPIList = _getRankedAPIList;
        service.GetInfoBoxes = _getInfoBoxes;
        service.GetLatestTransactionList = _getLatestTransactionList;
        service.GetLatestErrorList = _getLatestErrorList;
        service.GetFailedTransactionSummaryPerDealer = _getFailedTransactionSummaryPerDealer;
        service.GetTransactionSummary = _getTransactionSummary;
        service.GetTransactionSummaryPerDealer = _getTransactionSummaryPerDealer;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        function _getRankedAPIList() {
            var url = this.endpoint.getRankedAPIList;
            return RequestHandler.get(url);
        }

        function _getInfoBoxes() {
            var url = this.endpoint.getInfoBoxes;
            return RequestHandler.get(url);
        }

        function _getLatestTransactionList() {
            var url = this.endpoint.getLatestTransactionList;
            return RequestHandler.get(url);
        }

        function _getLatestErrorList() {
            var url = this.endpoint.getLatestErrorList;
            return RequestHandler.get(url);
        }

        function _getFailedTransactionSummaryPerDealer() {
            var url = this.endpoint.getFailedTransactionSummaryPerDealer;
            return RequestHandler.get(url);
        }

        function _getTransactionSummary() {
            var url = this.endpoint.getTransactionSummary;
            return RequestHandler.get(url);
        }

        function _getTransactionSummaryPerDealer() {
            var url = this.endpoint.getTransactionSummaryPerDealer;
            return RequestHandler.get(url);
        }
    }

})();
