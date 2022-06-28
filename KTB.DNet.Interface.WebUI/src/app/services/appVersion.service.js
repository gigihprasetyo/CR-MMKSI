(function () {
    'use strict';

    angular.module('DNet.services.appVersion', [])
        .service('AppVersionService', AppVersionService);

    AppVersionService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function AppVersionService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.appVersion;
        service.GetOptions = _getOptions;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        function _getOptions() {
            var url = this.endpoint.getOptions;
            return RequestHandler.get(url);
        }
    }

})();
