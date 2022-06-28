(function () {
    'use strict';

    angular.module('DNet.services.systemConfig', [])
        .service('SystemConfigService', SystemConfigService);

    SystemConfigService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function SystemConfigService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.systemConfig;
        service.GetOptions = _getOptions;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        /* public methods */
        function _getOptions() {
            var url = this.endpoint.getOptions;
            return RequestHandler.get(url);
        }
    }

})();
