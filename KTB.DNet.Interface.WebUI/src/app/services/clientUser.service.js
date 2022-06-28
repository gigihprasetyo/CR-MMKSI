(function () {
    'use strict';

    angular.module('DNet.services.clientUser', [])
        .service('ClientUserService', ClientUserService);

    ClientUserService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function ClientUserService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.clientUser;
        service.SaveClientUser = _saveClientUser;
        
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        /* public methods */
        function _saveClientUser(data) {
            var url = this.endpoint.saveClientUser;
            return RequestHandler.post(url, data);
        }
    }

})();
