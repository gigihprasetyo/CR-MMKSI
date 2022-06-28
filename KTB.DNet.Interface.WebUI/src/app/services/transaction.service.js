(function () {
    'use strict';

    angular.module('DNet.services.transaction', [])
        .service('TransactionService', TransactionService);

    TransactionService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function TransactionService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.transaction;
        service.GetApplicationList = _getApplicationList;
        service.GetClientList = _getClientList;
        service.GetDealerList = _getDealerList;
        service.GetTopApiList = _getTopApiList;
        service.SearchError = _searchError;
        service.SearchByParam = _searchByParam;
        service.SearchErrorByParam = _searchErrorByParam;
        service.Resend = _resend;
        service.GetTopApiList = _getTopApiList;
        service.Delete = _delete;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        /* public methods */
        function _searchError($entity) {
            var url = this.endpoint.searchError;
            return RequestHandler.post(url, $entity);
        }
        function _resend(data) {
            var url = this.endpoint.resend;
            return RequestHandler.post(url, data);
        }
        function _getApplicationList() {
            var url = this.endpoint.getApplicationList;
            return RequestHandler.get(url);
        }

        function _getClientList() {
            var url = this.endpoint.getClientList;
            return RequestHandler.get(url);
        }

        function _getDealerList() {
            var url = this.endpoint.getDealerList;
            return RequestHandler.get(url);
        }
        function _getTopApiList(data) {
            var url = this.endpoint.getTopApiList;
            return RequestHandler.post(url, data);
        }

        function _searchByParam(data) {
            var url = this.endpoint.searchByParam;
            return RequestHandler.post(url, data);
        }

        function _searchErrorByParam(data) {
            var url = this.endpoint.searchErrorByParam;
            return RequestHandler.post(url, data);
        }

        function _delete(data) {
            var url = this.endpoint.delete;
            return RequestHandler.post(url, data);
        }
    }

})();
