(function () {
    'use strict';

    angular.module('DNet.services.standardCode', [])
        .service('StandardCodeService', StandardCodeService);

    StandardCodeService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function StandardCodeService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.standardCode;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

    }

})();
