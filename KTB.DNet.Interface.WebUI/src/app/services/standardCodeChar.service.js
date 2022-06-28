(function () {
    'use strict';

    angular.module('DNet.services.standardCodeChar', [])
        .service('StandardCodeCharService', StandardCodeCharService);

    StandardCodeCharService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function StandardCodeCharService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.standardCodeChar;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

    }

})();
