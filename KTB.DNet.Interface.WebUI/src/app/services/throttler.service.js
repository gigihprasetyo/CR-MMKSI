(function () {
    'use strict';

    angular.module('DNet.services.throttler', [])
        .service('ThrottlerService', ThrottlerService);

    ThrottlerService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function ThrottlerService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.throttler;
        service.GetOptions = _getOptions;
        service.Export = _export;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        /* public methods */
        function _getOptions() {
            var url = this.endpoint.getOptions;
            return RequestHandler.get(url);
        }

        function _export() {
            var url = this.endpoint.export;
            return RequestHandler.post(url);
        }

    }

})();
