(function () {
    'use strict';

    angular.module('DNet.services.client', [])
        .service('ClientService', ClientService);

    ClientService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function ClientService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.client;
        service.GetAppOptions = _getAppOptions;
        service.GetOptions = _getOptions;
        service.GetListById = _getListById;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        /* public methods */
        function _getAppOptions() {
            var url = this.endpoint.getAppOptions;
            return RequestHandler.get(url);
        }

        function _getOptions(appId) {
            var url = this.endpoint.getOptions + appId;
            return RequestHandler.get(url);
        }

        function _getListById(data) {
            var url = this.endpoint.getListById;
            return RequestHandler.post(url, data);
        }

    }
    
})();
