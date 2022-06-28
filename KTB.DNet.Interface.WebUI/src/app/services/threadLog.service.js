(function () {
    'use strict';

    angular.module('DNet.services.threadLog', [])
        .service('ThreadLogService', ThreadLogService);

    ThreadLogService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function ThreadLogService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.threadLog;
        service.GetDetailTransactionLog = _getDetailTransactionLog;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        /* public methods */
        function _getDetailTransactionLog(threadLogId) {
            var url = this.endpoint.getDetailTransactionLog + "/" + threadLogId;
            return RequestHandler.get(url);
        }

    }

})();
